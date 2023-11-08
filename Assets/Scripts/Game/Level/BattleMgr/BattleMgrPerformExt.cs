using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private IEnumerator IE_InvokeSkillPerform()
    {
        InvokeSkillEffect();
        if (skillSubject.battleUnitType == BattleUnitType.Character)
        {
            yield return StartCoroutine(IE_CharacterSkillPerform());
        }
        yield break;
    }

    private IEnumerator IE_CharacterSkillPerform()
    {
        BattleCharacterView characterView = unitViewMgr.GetCharacterView(skillSubject.keyID);
        characterView.ChangeAniState(UnitAniState.Focus);
        yield return new WaitForSeconds(0.5f);
        characterView.ChangeAniState(UnitAniState.Attack);
    }

    private void InvokeSkillEffect()
    {
        EventCenter.Instance.EventTrigger("EffectViewGenerate", new EffectViewInfo(EffectViewType.FireBall, skillTargetPos));
    }
}
