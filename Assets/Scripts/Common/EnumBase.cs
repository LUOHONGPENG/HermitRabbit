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

public enum BattleTurnState
{
    Character,
    Plant,
    Foe
}

public enum BattleInteractState
{
    Choose,
    Move,
    Target,
    Wait
}