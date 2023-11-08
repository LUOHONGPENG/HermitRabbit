using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    private IEnumerator IE_InvokeSkillPerform()
    {
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
                        StartCoroutine(IE_SkillEffectView(performInfo.effectViewType, performInfo.startTime));
                        break;
                    case SkillPerformInfoType.PlaySound:
                        StartCoroutine(IE_SkillEffectPlaySound(performInfo.soundType, performInfo.startTime));
                        break;
                }

            }
        }
        float waitTime = PublicTool.GetSkillPerformTotalTime(skillBattleInfo.ID);
        yield return new WaitForSeconds(waitTime);
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

    private IEnumerator IE_SkillEffectView(EffectViewType viewType,float startTime)
    {
        yield return new WaitForSeconds(startTime);

        SkillEffectViewExcelItem effectViewExcelItem = PublicTool.GetSkillEffectViewExcelItem(viewType);
        if (effectViewExcelItem != null)
        {
            if(effectViewExcelItem.effectViewPosType == EffectViewPosType.TargetPos)
            {
                EventCenter.Instance.EventTrigger("EffectViewGenerate", new EffectViewInfo(viewType, skillTargetPos));
            }
            else if(effectViewExcelItem.effectViewPosType == EffectViewPosType.AllTile)
            {
                foreach(Vector2Int pos in listSkillRadiusPos)
                {
                    EventCenter.Instance.EventTrigger("EffectViewGenerate", new EffectViewInfo(viewType, pos));
                }
            }
        }
    }

    private IEnumerator IE_SkillEffectPlaySound(SoundType soundType, float startTime)
    {
        yield return new WaitForSeconds(startTime);
        PublicTool.PlaySound(soundType);
    }
}
