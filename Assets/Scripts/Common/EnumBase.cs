using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneName
{
    Menu,
    Test
}

public enum ExcelName
{
    CharacterSkill,
    CharacterData
}

public enum MagicType
{
    Sun,
    Moon,
    Star
}

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
    Choose,
    Move,
    Target,
    Wait
}