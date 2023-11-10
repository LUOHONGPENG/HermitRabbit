using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneName
{
    Init,
    Menu,
    Test,
    Game
}

public enum SaveSlotName
{
    Auto,
    Slot1,
    Slot2,
    End,
    BeforeBattle
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
    None,
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
    Line,
    Water,
    BurnUnit
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
    CONST,
    ATK,
    ATKMOV,
    MAXHP,
    COSTHP,
    DEF,
    RES,
    BURN
}

public enum PlantTriggerType
{
    CharacterNormalAttack,
    TurnStart
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
    TurnHalf,
    TurnRemove
}

public enum BuffCountDownType
{
    BeforeReset,
    AfterReset
}

public enum SpecialEffectTimeType
{
    BeforeDamage,
    AfterDamage
}

#endregion


#region Foe

public enum FoeGenerateType
{
    RowRange,
    FixedPos
}

public enum FoeFindTargetType
{
    Normal,
    Farthest
}

public enum FoeFocusType
{
    Foe,
    Friend
}
#endregion 

#region Skill Perform
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


public enum EffectViewType
{
    FireBall,
    FireBallBoom,
    Gravity,
    Thunder,
    ThunderHit,
    EnergyFire
    
}

public enum EffectViewPosType
{
    Subject,
    TargetPos,
    AllTarget,
    AllTile,
    AllBurn
}

public enum SkillPerformInfoType
{
    SubjectAni,
    EffectView,
    PlaySound
}

#endregion

public enum UITipType
{
    SkillNode,
    SkillButton
}


#region Talk

public enum TalkGroup
{
    Day1
}

public enum TalkStep
{
    BattleStart,
    Text,
    End
}

#endregion

#region Camera

public enum CameraType
{
    NormalCamera,
    SkillPerformCamera
}

public enum CameraPosType
{
    CharacterLeft
}
#endregion
