using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private List<Vector2Int> listSkillRadiusEffectPos = new List<Vector2Int>();
    private List<Vector2Int> listSkillBurnEffectPos = new List<Vector2Int>();

    private IEnumerator IE_InvokeSkillPerform()
    {
        EventCenter.Instance.EventTrigger("HideUnitUI", null);
        List<SkillPerformInfo> listPerform = PublicTool.GetSkillPerformInfo(skillBattleInfo.ID);
        if (listPerform != null)
        {
            for(int i = 0; i < listPerform.Count; i++)
            {
                SkillPerformInfo performInfo = listPerform[i];
                switch (performInfo.infoType)
                {
                    case SkillPerformInfoType.SubjectAni:
                        StartCoroutine(IE_SubjectAniPerform(performInfo.unitAniState, performInfo.startTime));
                        break;
                    case SkillPerformInfoType.EffectView:
                        StartCoroutine(IE_SkillEffectView(performInfo.effectViewType, performInfo.effectPosType, performInfo.startTime));
                        break;
                    case SkillPerformInfoType.PlaySound:
                        StartCoroutine(IE_SkillEffectPlaySound(performInfo.soundType, performInfo.startTime));
                        break;
                }

            }
        }
        float waitTime = PublicTool.GetSkillPerformTotalTime(skillBattleInfo.ID);
        yield return new WaitForSeconds(waitTime);
        EventCenter.Instance.EventTrigger("ShowUnitUI", null);
    }

    private IEnumerator IE_SubjectAniPerform(UnitAniState state,float startTime)
    {
        if (skillSubject.battleUnitType == BattleUnitType.Character)
        {
            yield return new WaitForSeconds(startTime);
            BattleCharacterView characterView = unitViewMgr.GetCharacterView(skillSubject.keyID);
            characterView.ChangeAniState(state);
        }
        yield break;
    }

    private IEnumerator IE_SkillEffectView(EffectViewType viewType,EffectViewPosType posType, float startTime)
    {
        yield return new WaitForSeconds(startTime);

        if (posType == EffectViewPosType.Subject)
        {
            EventCenter.Instance.EventTrigger("EffectViewGenerate", new EffectViewInfo(viewType, skillSubject.posID));
        }
        else if (posType == EffectViewPosType.TargetPos)
        {
            EventCenter.Instance.EventTrigger("EffectViewGenerate", new EffectViewInfo(viewType, skillTargetPos));
        }
        else if (posType == EffectViewPosType.AllTile)
        {
            foreach (Vector2Int pos in listSkillRadiusEffectPos)
            {
                EventCenter.Instance.EventTrigger("EffectViewGenerate", new EffectViewInfo(viewType, pos));
            }
        }
        else if(posType == EffectViewPosType.AllBurn)
        {
            foreach (Vector2Int pos in listSkillBurnEffectPos)
            {
                EventCenter.Instance.EventTrigger("EffectViewGenerate", new EffectViewInfo(viewType, pos));
            }
        }
    }

    private IEnumerator IE_SkillEffectPlaySound(SoundType soundType, float startTime)
    {
        yield return new WaitForSeconds(startTime);
        PublicTool.PlaySound(soundType);
    }
}
