using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneName
{
    Menu,
    Test
}


#region Basic Status
public enum GamePhase
{
    Peace,
    Battle
}


public enum InteractState
{
    Normal,
    Move,
    Skill,
    WaitAction
}

public enum InteractTargetType
{
    None,
    Map,
    Character
}

public enum MapIndicatorType
{
    Hide,
    Normal,
    AttackRadius,
    AttackCover,
    Blue
}

public enum BattleUnitType
{
    Character,
    Plant,
    Foe
}


public enum BattlePhase
{
    CharacterPhase,
    FoePhase
}

public enum BarResourceType
{
    Health,
    Skill,
    Move
}
#endregion

#region SkillEnum

public enum SkillRegionType
{
    Circle,
    Square,
    Cross
}

public enum SkillElementType
{
    None,
    Sun,
    Moon,
    Star
}

public enum SkillDamageType
{
    None,
    Physical,
    Magic,
    Real
}

public enum SkillEffectType
{
    None,
    Damage,
    Debuff,
    DaDebuff,
    Heal,
    Buff,
    Special
}

#endregion

public enum EffectUITextType
{
    Damage,
    Warning
}

public enum BattleTextType
{
    Damage,
    Heal,
    Debuff,
    Buff
}

#region Data Struct

public struct UnitInfo
{
    public BattleUnitType type;
    public int keyID;

    public UnitInfo(BattleUnitType type,int keyID)
    {
        this.type = type;
        this.keyID = keyID;
    }
}


public struct SkillBattleInfo
{
    public int costAP;

    public SkillRegionType regionType;
    public int range;
    public int radius;
    public bool isTargetFoe;

    public SkillBattleInfo(CharacterSkillExcelItem item)
    {
        this.costAP = item.costAP;

        this.regionType = item.regionType;
        this.range = item.range;
        this.radius = item.radius;
        this.isTargetFoe = item.isTargetFoe;
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

public struct BattleTextInfo
{
    public BattleTextType type;
    public string info;

    public BattleTextInfo(BattleTextType type, string info)
    {
        this.type = type;
        this.info = info;
    }
}
#endregion