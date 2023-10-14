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
    public float damageDeltaFloat;
    public SkillDamageDeltaStd damageDeltaStd;
    public bool isNormalAttack;
    //Special
    public List<SkillSpecialExcelItem> listSpecialEffect;

    public SkillBattleInfo(SkillExcelItem item)
    {
        this.ID = item.id;
        this.name = item.name;
        this.skillSubjectType = item.skillSubjectType;
        this.costAP = item.costAP;
        //Range
        this.regionType = item.regionType;
        this.isRangeSelf = item.isRangeSelf;
        this.range = item.range;
        this.radius = item.radius;
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
        this.damageType = item.damageType;
        this.damageDeltaFloat = item.damageDeltaFloat;
        this.damageDeltaStd = item.damageDeltaStd;
        this.isNormalAttack = item.isNormalAttack;
        //
        this.listSpecialEffect = item.listSpecialEffectUse;

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
    public int characterID;
    public Vector2 mousePos;

    public UITipInfo(UITipType uiTipType, int ID,int characterID,Vector2 mousePos)
    {
        this.uiTipType = uiTipType;
        this.ID = ID;
        this.characterID = characterID;
        this.mousePos = mousePos;
    }
}

