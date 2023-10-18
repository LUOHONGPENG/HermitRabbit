using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneName
{
    Menu,
    Test
}


#region System Basic Status
public enum GamePhase
{
    Peace,
    Battle
}


public enum InteractState
{
    PeaceNormal,
    PeacePlant,
    PeaceMap,
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

#endregion

#region Map

public enum MapTileType
{
    Normal,
    Water,
    Grass,
    Stone
}

public enum MapIndicatorType
{
    Hide,
    Normal,
    AttackRadius,
    AttackCover,
    Blue
}

public enum Rarity
{
    Common,
    UnCommon,
    Rare,
    Epic
}

#endregion

#region Battle Basic
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
    Line
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
    Const,
    ATK,
    ATKMOV,
    MAXHP,
    DEF,
    RES
}

public enum PlantTriggerType
{
    CharacterNormalAttack
}

public enum SkillNodeType
{
    Active,
    Passive,
    Numerical
}

public enum ActiveSkillType
{
    NormalAttack,
    DamageSkill,
    SupportSkill,
    UltimateSkill,
    PlantSkill,
    MonsterSkill
}

public enum BuffCounterType
{
    Fixed,
    TurnDecrease,
    TurnRemove
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
    Buff,
    Special
}

public enum UITipType
{
    SkillNode,
    SkillButton
}
