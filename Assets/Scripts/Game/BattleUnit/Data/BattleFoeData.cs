using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BattleFoeData : BattleUnitData
{
    private FoeExcelItem item;

    public int exp;

    public FoeFindTargetType findTargetType;

    public FoeFocusType focusType;

    public List<Vector2Int> listValidAttackRange = new List<Vector2Int>();

    public BattleFoeData(int typeID,int keyID)
    {
        //Basic Setting
        this.typeID = typeID;
        this.keyID = keyID;
        this.battleUnitType = BattleUnitType.Foe;
        item = ExcelDataMgr.Instance.foeExcelData.GetExcelItem(typeID);
        curHP = item.HP;
        maxHP = item.HP;
        curMOV = item.MOV;
        maxMOV = item.MOV;

        findTargetType = item.findTargetType;
        focusType = item.focusType;
    }

    public FoeExcelItem GetItem()
    {
        return item;
    }

    public int GetSkillID()
    {
        return item.skillID;
    }

    public int GetSkillTouchRange()
    {
        SkillExcelItem skillItem = PublicTool.GetSkillItem(GetSkillID());
        if (skillItem != null && skillItem.regionType == SkillRegionType.Circle)
        {
            return skillItem.RealRange + skillItem.RealRadius;
        }
        else
        {
            return 0;
        }

    }

    #region Override

    public override void ResetNewTurnBefore()
    {
        TurnBuffDecreaseBefore();

        curMOV = regenMOV;
    }

    public override void ResetNewTurnAfter()
    {
        TurnBuffDecreaseAfter();

        if (curMOV > curMaxMOV)
        {
            curMOV = curMaxMOV;
        }
    }


    public override void InvokeDead()
    {
        EventCenter.Instance.EventTrigger("BattleFoeDead", exp);
    }

    #endregion

    #region Basic Attribute
    public override int curATK
    {
        get
        {
            int temp = item.ATK + buffATK;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }

    public override int curDEF
    {
        get
        {
            int temp = item.DEF + buffDEF;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }

    public override int curRES
    {
        get
        {
            int temp = item.RES + buffRES;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }


    #endregion


    public override void RefreshTouchRange()
    {
        Dictionary<Vector2Int, FindPathNode> dicPathNode = new Dictionary<Vector2Int, FindPathNode>();
        List<MapTileData> listMap = PublicTool.GetGameData().listMapTile;
        for (int i = 0; i < listMap.Count; i++)
        {
            dicPathNode.Add(listMap[i].posID, new FindPathNode(listMap[i].posID));
        }

        //Clear Valid Node
        listValidTouchRange.Clear();



        //Get Block Pos
        List<Vector2Int> listBlock = new List<Vector2Int>();
        for (int i = 0; i < PublicTool.GetGameData().listMapCurStonePos.Count; i++)
        {
            listBlock.Add(PublicTool.GetGameData().listMapCurStonePos[i]);
        }
        for (int i = 0; i < PublicTool.GetGameData().listTempPlantPos.Count; i++)
        {
            listBlock.Add(PublicTool.GetGameData().listTempPlantPos[i]);
        }
        for (int i = 0; i < PublicTool.GetGameData().listTempCharacterPos.Count; i++)
        {
            listBlock.Add(PublicTool.GetGameData().listTempCharacterPos[i]);
        }


        //Prepare the container for search
        Queue<FindPathNode> ququeOpen = new Queue<FindPathNode>();
        Dictionary<Vector2Int, FindPathNode> dicClose = new Dictionary<Vector2Int, FindPathNode>();
        dicClose.Clear();

        //Start searching
        FindPathNode startNode = PublicTool.GetFindPathNode(dicPathNode, posID);
        ququeOpen.Enqueue(startNode);
        while (ququeOpen.Count > 0)
        {
            FindPathNode tarNode = ququeOpen.Dequeue();
            if (tarNode.parentNode != null)
            {
                FindPathNode parentNode = tarNode.parentNode;
                tarNode.path = new List<Vector2Int>(parentNode.path);
                tarNode.gCost = parentNode.gCost + 1;
            }
            else
            {
                tarNode.path = new List<Vector2Int>();
                tarNode.gCost = 0;
            }

            //No matter whether it is valid
            dicClose.Add(tarNode.pos, tarNode);

            if (tarNode.gCost > curMOV)
            {
                continue;
            }
            tarNode.path.Add(tarNode.pos);
            //dicValidMoveNode.Add(tarNode.pos, tarNode);
            listValidTouchRange.Add(tarNode.pos);

            

            List<FindPathNode> listNearNode = PublicTool.GetNearFindPathNode(dicPathNode, tarNode.pos);
            for (int i = 0; i < listNearNode.Count; i++)
            {
                FindPathNode nextNode = listNearNode[i];
                if (dicClose.ContainsKey(nextNode.pos))
                {
                    continue;
                }
                if (ququeOpen.Contains(nextNode))
                {
                    continue;
                }
                if (listBlock.Contains(nextNode.pos))
                {
                    continue;
                }
                nextNode.parentNode = tarNode;

                ququeOpen.Enqueue(nextNode);
            }
        }

        foreach (Vector2Int tarPos in PublicTool.GetGameData().listTempAllPos)
        {
            if (listValidTouchRange.Contains(tarPos))
            {
                listValidTouchRange.Remove(tarPos);
            }
        }

        //Range
        listValidAttackRange.Clear();
        if (GetSkillID() > 0)
        {
            SkillExcelItem skillItem = PublicTool.GetSkillItem(GetSkillID());
            int skillTouchRange = skillItem.RealRadius + skillItem.RealRange;

            List<Vector2Int> listTemp = PublicTool.GetTargetCircleRange(posID, skillTouchRange);
            for (int j = 0; j < listTemp.Count; j++)
            {
                Vector2Int validAttack = listTemp[j];
                if (!listValidAttackRange.Contains(validAttack))
                {
                    listValidAttackRange.Add(validAttack);
                }
            }

            for (int i = 0; i < listValidTouchRange.Count; i++)
            {
                Vector2Int validMove = listValidTouchRange[i];
                listTemp = PublicTool.GetTargetCircleRange(validMove, skillTouchRange);
                for(int j = 0; j < listTemp.Count; j++)
                {
                    Vector2Int validAttack = listTemp[j];
                    if (!listValidAttackRange.Contains(validAttack))
                    {
                        listValidAttackRange.Add(validAttack);
                    }
                }
            }

            listValidAttackRange.Remove(posID);
            //listValidTouchRange = PublicTool.GetTargetCircleRange(posID, skillTouchRange);
        }


    }

    public int TraitID
    {
        get
        {
            FoeExcelItem item = GetItem();
            if (item != null)
            {
                return item.foeTraitID;
            }
            else
            {
                return -1;
            }

        }
    }
}
