using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneName
{
    Menu,
    Test
}


#region BattleRelated
public enum LevelPhase
{
    Peace,
    Battle
}


public enum InteractState
{
    Normal,
    Move,
    Target,
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

public enum MagicType
{
    Sun,
    Moon,
    Star
}
#endregion



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
