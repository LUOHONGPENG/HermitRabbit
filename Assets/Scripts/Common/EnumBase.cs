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
    PeaceNormal,
    BattleNormal,
    CharacterMove,
    CharacterSkill,
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
    Harm,
    Help
}

public enum SkillDamageDeltaStd
{
    None,
    ATK,
    MAXHP
}

public enum PlantTriggerType
{
    CharacterNormalAttack
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

