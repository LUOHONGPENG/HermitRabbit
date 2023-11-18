using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlantData : BattleUnitData
{
    private PlantExcelItem item;

    public BattlePlantData(int typeID,int keyID)
    {
        //Basic Setting
        this.typeID = typeID;
        this.keyID = keyID;
        this.battleUnitType = BattleUnitType.Plant;

        item = ExcelDataMgr.Instance.plantExcelData.GetExcelItem(typeID);
        curHP = item.HP;
        maxHP = item.HP;
    }

    public PlantExcelItem GetItem()
    {
        return item;
    }

    public int GetSkillID()
    {
        return item.skillID;
    }

    public int GetEssence()
    {
        return (int)item.essence;
    }


    #region Basic Attribute
    public override int curATK
    {
        get
        {
            int tempATK = 0;
            tempATK += item.ATK;
            tempATK += buffATK;
            tempATK += PublicTool.GetPlantNumInThisRow(posID.y, 2001);
            if (tempATK < 0)
            {
                tempATK = 0;
            }
            return tempATK;
        }
    }

    public override int curDEF
    {
        get
        {
            int tempDEF = 0;
            tempDEF += item.DEF;
            tempDEF += buffDEF;
            tempDEF += PublicTool.GetPlantNumInThisColumn(posID.x, 2002);
            if (tempDEF < 0)
            {
                tempDEF = 0;
            }
            return tempDEF;
        }
    }

    public override int curRES
    {
        get
        {
            int tempRES = 0;
            tempRES += item.RES;
            tempRES += buffRES;
            if (tempRES < 0)
            {
                tempRES = 0;
            }
            return tempRES;
        }
    }
    #endregion

    public override void ResetNewTurnBefore()
    {
        curAP = curMaxAP;
    }

    public override void ResetNewTurnAfter()
    {
        TurnBuffDecrease();
        if (curAP > curMaxAP)
        {
            curAP = curMaxAP;
        }
    }


    public override void ResetBattleEnd()
    {
        ClearAllBuff();

        curHP = curMaxHP;
        curAP = curMaxAP;

    }

    public override void RefreshTouchRange()
    {
        listValidTouchRange.Clear();

        if (GetSkillID() > 0)
        {
            SkillExcelItem skillItem = PublicTool.GetSkillItem(GetSkillID());
            int skillTouchRange = skillItem.RealRadius + skillItem.RealRange;
            listValidTouchRange = PublicTool.GetTargetCircleRange(posID,skillTouchRange);
        }
        else
        {
            if(typeID == 2001)
            {
                for(int i = 0; i < GameGlobal.mapClipSize * GameGlobal.mapClipNumX; i++)
                {
                    listValidTouchRange.Add(new Vector2Int(i,posID.y));
                }
            }
            else if(typeID == 2002)
            {
                for (int i = 0; i < GameGlobal.mapClipSize * GameGlobal.mapClipNumY + GameGlobal.mapRowFriend + GameGlobal.mapRowFoe; i++)
                {
                    listValidTouchRange.Add(new Vector2Int(posID.x,i));
                }
            }
        }
    }

}
