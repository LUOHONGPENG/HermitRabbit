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

public enum LanguageType
{
    EN,
    CN
}

public enum SaveSlotName
{
    Auto,
    Slot1,
    Slot2,
    Slot3,
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

public enum ResourceType
{
    Essence,
    Memory
}

#endregion

#region Map

public enum MapTileType
{
    Normal,
    Water,
    Grass,
    Stone,
    Flower,
    Magic,
    Duel,
    Guard,
    Stealth,
    Hope,
    End
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
    Epic,
    Legendary
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

#region Skill

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

public enum SkillTag
{
    PDmg,
    MDmg,
    TDmg,
    HurtAll,
    HurtCharacter
}

public enum SkillEffectType
{
    None,
    Harm,
    Help
}

public enum SkillTileEffectType
{
    None,
    Burn,
    BurnUnitOnly,
    Water,
    WaterPlus,
    Break
}

public enum SkillDamageDeltaStd
{
    None,
    CONST,
    ATK,
    ATKMOV,
    ATKDISD1,
    ATKDISD2,
    ATKDISD3,
    MAXHP,
    COSTHP,
    CURHP,
    DEF,
    RES,
    BURN
}

public enum PlantTriggerType
{
    CharacterNormalAttack,
    ActionEndDead,
    TurnStart,
    FirstTurn,
    SecondTurn,
    Passive
}

public enum SkillNodeType
{
    None,
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
    Farthest,
    HPLowest
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
    EnergyFire,
    Fourtain,
    Cut
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
    PlaySound,
    ChangeCamera
}

#endregion

public enum UITipType
{
    SkillNode,
    SkillButton,
    PlantPreview,
    BuffIcon,
    TileIcon
}


#region Talk

public enum TalkGroup
{
    None,
    Day1,
    Day2,
    Day3,
    Day7

}

public enum TalkStep
{
    BattleStart,
    Text,
    End
}

#endregion

#region Tutorial

public enum TutorialGroup
{
    None,
    Battle,
    Skill,
    Map,
    Plant,
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
    None,
    CharacterLeftLow,
    CharacterLeftHigh,
    MiddleLeft,
    FocusSubject,
    FocusTarget,
    FocusTargetExtra
}
#endregion
