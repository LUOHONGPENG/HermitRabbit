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
            tempATK += buffATK;
            if (PublicTool.CheckWhetherCharacterUnlockSkill(1001,1021))
            {
                tempATK += 1;
            }
            if (PublicTool.CheckWhetherCharacterUnlockSkill(1002,2021))
            {
                tempATK += 1;
            }
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
            tempDEF += DEF;
            tempDEF += buffDEF;
            if (PublicTool.CheckWhetherCharacterUnlockSkill(1002, 2031))
            {
                tempDEF += 1;
            }
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
            tempRES += RES;
            tempRES += buffRES;
            if (PublicTool.CheckWhetherCharacterUnlockSkill(1001, 2041))
            {
                tempRES += 1;
            }
            if (tempRES < 0)
            {
                tempRES = 0;
            }
            return tempRES;
        }
    }

    public override float curMaxHP
    {
        get
        {
            float temp = item.HP;
            if(typeID == 1001)
            {
                temp += ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level).HP1; 
            }
            else if(typeID == 1002)
            {
                temp += ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level).HP2;
            }
            return temp;
        }
    }



    public override int curMaxMOV
    {
        get
        {
            int temp = maxMOV + buffMaxMOV;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }

    public override int curMaxAP
    {
        get
        {
            int temp = maxAP + buffMaxAP;
            if (temp < 0)
            {
                temp = 0;
            }
            return temp;
        }
    }

    public override int curCounter
    {
        get
        {
            int temp = buffCounter;
            if (keyID == 1002 && PublicTool.CheckWhetherCharacterUnlockSkill(1002, 2291))
            {
                temp += 2;
            }
            return temp;
        }
    }

    public override void ResetNewTurn()
    {
        //Buff will affect AP and MOV
        TurnBuffDecrease();

        curAP = regenAP;
        curMOV = regenMOV;
    }

    public override void ResetBattleEnd()
    {
        ClearAllBuff();

        curHP = curMaxHP;
        curAP = regenAP;
        curMOV = regenMOV;

        this.posID = new Vector2Int(item.startPos[0], item.startPos[1]);

    }

}

/// <summary>
/// About the data that need to be saved
/// </summary>
public partial class BattleCharacterData
{
    //LevelData
    public int EXP = 0;
    public int SPSpent = 0;
    public List<int> listUnlockSkillNode = new List<int>();

    public void InitSkillNode()
    {
        ResetSkillNode();
    }

    public void ResetSkillNode()
    {
        listUnlockSkillNode.Clear();
        List<SkillNodeExcelItem> listNode = ExcelDataMgr.Instance.skillNodeExcelData.GetSkillNodeList(typeID);
        for (int i = 0; i < listNode.Count; i++)
        {
            if (listNode[i].isInitUnlock)
            {
                AcquireSkillNode(listNode[i].id);
            }
        }
        SPSpent = 0;
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
            return ExcelDataMgr.Instance.characterExpExcelData.GetRequiredExp(Level);
            //return ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level+1).EXP - ExcelDataMgr.Instance.characterExpExcelData.GetExpItem(Level).EXP;
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

