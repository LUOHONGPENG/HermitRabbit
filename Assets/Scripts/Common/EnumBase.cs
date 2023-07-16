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
    Red,
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

    public SkillMapInfo(SkillRegionType regionType,int range,int radius)
    {
        this.regionType = regionType;
        this.range = range;
        this.radius = radius;
    }

}
#endregion