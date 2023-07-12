using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneName
{
    Menu,
    Test
}


#region BattleRelated

public enum BattleUnitType
{
    Character,
    Plant,
    Foe
}

public enum BattleTurnPhase
{
    Character,
    Plant,
    Foe
}

public enum InteractState
{
    Normal,
    Move,
    Target,
    WaitAction
}

public enum MapIndicatorType
{
    Hide,
    Normal,
    Red,
    Blue
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