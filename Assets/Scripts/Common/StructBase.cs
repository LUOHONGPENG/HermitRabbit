using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct UnitInfo
{
    public BattleUnitType type;
    public int keyID;

    public UnitInfo(BattleUnitType type, int keyID)
    {
        this.type = type;
        this.keyID = keyID;
    }
}


public struct SkillBattleInfo
{
    //Basic
    public int ID;
    public string name;
    public BattleUnitType skillSubjectType;
    public int costAP;
    public int costMOV;
    public int costHP;
    public int costMemory;
    //Range
    public SkillRegionType regionType;
    public bool isRangeSelf;
    public int range;
    public int radius;
    //Target
    public bool needExtraTarget;
    public bool isTargetFoe;
    public bool isTargetCharacter;
    public bool isTargetPlant;
    //EffectType
    public SkillEffectType foeEffect;
    public SkillEffectType characterEffect;
    public SkillEffectType plantEffect;
    //Damage
    public SkillDamageType damageType;
    public SkillDamageDeltaStd damageDeltaStd;
    public float damageDeltaFloat;
    public int damageModifier;
    public float damageExtraBonus;
    public ActiveSkillType activeSkillType;
    //Special
    public List<BuffExcelItem> listBuffEffect;
    public List<int> listBuffDelta;
    //Special
    public List<SkillSpecialExcelItem> listSpecialEffect;
    public List<int> listSpecialDelta;
    public SkillTileEffectType tileEffectType;

    public SkillBattleInfo(SkillExcelItem item,BattleUnitData unitData)
    {
        this.ID = item.id;

        this.name = item.GetDescItem().GetName();
        this.skillSubjectType = item.skillSubjectType;
        this.costAP = item.RealCostAP;
        this.costMOV = item.costMOV;
        this.costHP = item.RealCostHP;
        this.costMemory = item.costMemory;
        //Range
        this.regionType = item.regionType;
        this.isRangeSelf = item.isRangeSelf;
        this.range = item.RealRange;
        this.radius = item.RealRadius;
        //Target
        this.needExtraTarget = item.needExtraTarget;
        this.isTargetFoe = item.isTargetFoe;
        this.isTargetCharacter = item.isTargetCharacter;
        this.isTargetPlant = item.isTargetPlant;
        //EffectType
        this.foeEffect = item.foeEffect;
        this.characterEffect = item.characterEffect;
        this.plantEffect = item.plantEffect;
        //Damage
        this.damageType = item.RealDamageType;
        this.damageDeltaStd = item.RealDamageStd;
        this.damageDeltaFloat = item.damageDeltaFloat;
        this.damageModifier = item.RealDamageModifier;
        this.damageExtraBonus = 0;
        this.activeSkillType = item.activeSkillType;
        //Buff
        this.listBuffEffect = item.listBuffUse;
        this.listBuffDelta = new List<int>(item.listBuffDelta);
        //SpecialEffect
        this.listSpecialEffect = item.listSpecialEffectUse;
        this.listSpecialDelta = new List<int>(item.listSpecialDelta);
        this.tileEffectType = item.RealTileEffectType;

        //UnitDataBuff
        if (unitData != null)
        {
            if (unitData.tileBuffChangeDmgReal)
            {
                damageType = SkillDamageType.Real;
            }

            if (unitData.tileBuffMagicDamageAdd)
            {
                if(damageType == SkillDamageType.Magic)
                {
                    this.costHP++;
                    damageExtraBonus = 0.5f;
                }
            }
        }

    }
}

public class FoeFindTargetInfo
{
    public FoeFindTargetType findTargetType;

    public int keyID = -1;
    public BattleUnitType unitType;
    public Vector2Int posID;

    public int GCost = 99;
    private float HPRate = 1f;
    private int sourceHate = 0;

    public int realDistance = 0;

    public FoeFindTargetInfo(BattleFoeData foeData, BattleUnitData unit)
    {
        this.findTargetType = foeData.findTargetType;
        this.keyID = unit.keyID;
        this.unitType = unit.battleUnitType;
        this.posID = unit.posID;
        this.sourceHate = unit.curHate;
        this.HPRate = unit.HPrate;
        this.realDistance = PublicTool.CalculateGlobalDis(foeData.posID, unit.posID);
    }

    public int TotalHate
    {
        get
        {
            int temp = sourceHate;
            switch (findTargetType)
            {
                case FoeFindTargetType.Normal:
                    temp += Mathf.RoundToInt(1f * CalculateHateHP());
                    temp += Mathf.RoundToInt(1f * CalculateHateGCostNear());
                    break;
                case FoeFindTargetType.Farthest:
                    temp += Mathf.RoundToInt(2f * CalculateHateDis());
                    break;
                case FoeFindTargetType.HPLowest:
                    temp += Mathf.RoundToInt(2f * CalculateHateHP());
                    break;
            }

            //Standard HATE
            if(temp < 0)
            {
                temp = 0;
            }


            if(HPRate <= 0)
            {
                temp = -1;
            }

            return temp;
        }
    }

    public int CalculateHateHP()
    {
        //HPrate = 0 -> 1000
        //HPrate = 1 -> 0
        return (int)(1 - HPRate) * 1000;
    }

    public int CalculateHateGCostNear()
    {
        //GCost = 0 -> 1000
        //GCost = 20 -> 0
        if(GCost > 20)
        {
            return 0;
        }
        else
        {
            return Mathf.RoundToInt((20f - GCost)/20f * 1000f);
        }
    }

    public int CalculateHateGCostFar()
    {
        //GCost = 0 -> 0
        //GCost = 20 -> 1000
        if (GCost > 20)
        {
            return 1000;
        }
        else
        {
            return Mathf.RoundToInt(GCost * 1000f / 20f);
        }
    }

    public int CalculateHateDis()
    {
        //GCost = 0 -> 0
        //GCost = 20 -> 1000
        if (realDistance > 20)
        {
            return 1000;
        }
        else
        {
            return Mathf.RoundToInt(GCost * 1000f / 20f);
        }
    }
}


public struct PlantSkillRequestInfo
{
    public int keyID;
    public int skillID;
    public UnitInfo tarUnit;

    public PlantSkillRequestInfo(int keyID,int skillID, UnitInfo tarUnit)
    {
        this.keyID = keyID;
        this.skillID = skillID;
        this.tarUnit = tarUnit;
    }
}

public struct EffectUITextInfo
{
    public EffectUITextType type;
    public Vector2Int posID;
    public int argNum;
    public string argString;
    public EffectUITextInfo(EffectUITextType type, Vector2Int posID, int argNum, string argString = "")
    {
        this.type = type;
        this.posID = posID;
        this.argNum = argNum;
        this.argString = argString;
    }
}

/// <summary>
/// The struct for storing the info of skill effect text such as Damage and Heal
/// </summary>
public struct EffectBattleTextInfo
{
    public BattleTextType type;
    public string content;
    public Vector2Int posID;

    public EffectBattleTextInfo(BattleTextType type, string content, Vector2Int posID)
    {
        this.type = type;
        this.content = content;
        this.posID = posID;
    }
}

public struct EffectViewInfo
{
    public EffectViewType type;
    public Vector2Int tarPos;

    public EffectViewInfo(EffectViewType type, Vector2Int tarPos)
    {
        this.type = type;
        this.tarPos = tarPos;
    }
}

public struct ChangeCameraInfo
{
    public CameraType cameraType;
    public CameraPosType posType;
    public Vector2Int posSubject;
    public Vector2Int posTarget;
    public Vector2Int posExtraTarget;

    public ChangeCameraInfo(CameraType type, CameraPosType posType, Vector2Int posSubject, Vector2Int posTarget,Vector2Int posExtraTarget)
    {
        this.cameraType = type;
        this.posType = posType;
        this.posSubject = posSubject;
        this.posTarget = posTarget;
        this.posExtraTarget = posExtraTarget;
    }
}

/// <summary>
/// The struct for storing the info of Warning Text
/// </summary>
public struct EffectWarningTextInfo
{
    public string content;
    public Vector2Int posID;

    public EffectWarningTextInfo(string content,Vector2Int posID)
    {
        this.content = content;
        this.posID = posID;
    }
}

public struct UITipInfo
{
    public UITipType uiTipType;
    public int ID;
    public int unitID;
    public Vector2 mousePos;

    public UITipInfo(UITipType uiTipType, int ID,int unitID, Vector2 mousePos)
    {
        this.uiTipType = uiTipType;
        this.ID = ID;
        this.unitID = unitID;
        this.mousePos = mousePos;
    }
}

