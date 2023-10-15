using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class BattleCharacterData : BattleUnitData
{
    //BattleData
    public int ATK;
    public int DEF;
    public int RES;

    private CharacterExcelItem item;

    public BattleCharacterData(int typeID)
    {
        //Basic Setting
        this.typeID = typeID;
        this.keyID = typeID;
        this.battleUnitType = BattleUnitType.Character;
        item = ExcelDataMgr.Instance.characterExcelData.GetExcelItem(typeID);
        //Pos Setting
        posID = new Vector2Int(item.startPos[0], item.startPos[1]);
        //Attribute Setting
        ATK = item.ATK;
        DEF = item.DEF;
        RES = item.RES;
        //3 Attribute Setting
        curHP = item.HP;
        maxHP = item.HP;
        curAP = item.AP;
        maxAP = item.AP;
        curMOV = item.MOV;
        maxMOV = item.MOV;
        //Grow Setting
        EXP = 0;
        SPSpent = 0;
        InitSkillNode();
    }

    public CharacterExcelItem GetItem()
    {
        return item;
    }

    public override int curATK
    {
        get
        {
            int tempATK = 0;
            //Basic ATK
            tempATK += ATK;
            //
            if (CheckUnlockSkillNode(1021))
            {
                tempATK += 1;
            }
            return tempATK;
        }
    }

    public override int curDEF
    {
        get
        {
            return DEF;
        }
    }

    public override int curRES
    {
        get
        {
            return RES;
        }
    }

    public override void ResetNewTurn()
    {
        curAP = maxAP;
        curMOV = maxMOV;
    }
}


public partial class BattleCharacterData
{
    //LevelData
    public int EXP = 0;
    public int SPSpent = 0;
    public List<int> listUnlockSkillNode = new List<int>();

    public void InitSkillNode()
    {
        listUnlockSkillNode.Clear();
        List<SkillNodeExcelItem> listNode = ExcelDataMgr.Instance.skillNodeExcelData.GetSkillNodeList(typeID);
        for(int i = 0; i < listNode.Count; i++)
        {
            if (listNode[i].isInitUnlock)
            {
                AcquireSkillNode(listNode[i].id);
            }
        }
    }

    public void AcquireSkillNode(int nodeID)
    {
        if (!CheckUnlockSkillNode(nodeID))
        {
            listUnlockSkillNode.Add(nodeID);
        }
    }

    public bool CheckUnlockSkillNode(int nodeID)
    {
        if (listUnlockSkillNode.Contains(nodeID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int Level
    {
        get
        {
            return ExcelDataMgr.Instance.characterExpExcelData.GetLevelFromExp(EXP);
        }
    }

    public int SPLeft
    {
        get
        {
            int TotalSp = ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level).SP;
            return TotalSp - SPSpent;
        }
    }

    public int curEXP
    {
        get
        {
            return EXP - ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level).EXP;
        }
    }

    public int requiredEXP
    {
        get
        {
            return ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level+1).EXP - ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level).EXP;
        }
    }

    public bool CheckWhetherMaxLevel()
    {
        if(ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level+1) == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

