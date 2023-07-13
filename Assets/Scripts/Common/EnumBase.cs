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

public struct ChangeInteractStruct
{
    public InteractState state;
    public int data0;

    public ChangeInteractStruct(InteractState state,int data0)
    {
        this.state = state;
        this.data0 = data0;
    }
}