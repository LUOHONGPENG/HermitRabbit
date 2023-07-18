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
    Character,
    Plant,
    Foe
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

#endregion

public enum EffectUITextType
{
    Damage,
    Warning
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

public struct InteractInfo
{
    public InteractState state;
    public int data_0;

    public InteractInfo(InteractState state,int data_0 = -1)
    {
        this.state = state;
        this.data_0 = data_0;
    }
}

public struct SkillMapInfo
{
    public SkillRegionType regionType;
    public int range;
    public int radius;
    public bool isTargetFoe;

    public SkillMapInfo(CharacterSkillExcelItem item)
    {
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
#endregion