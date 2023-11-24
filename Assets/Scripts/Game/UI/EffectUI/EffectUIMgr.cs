using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EffectUIMgr : MonoBehaviour
{
    public Transform tfEffectText;
    public GameObject pfBattleText;
    public GameObject pfWarningText;

    public CanvasGroup groupSkillName;
    public TextMeshProUGUI txSkillName;
    Sequence seq;

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("EffectWarningText", EffectWarningTextEvent);
        EventCenter.Instance.AddEventListener("EffectBattleText", EffectBattleTextEvent);
        EventCenter.Instance.AddEventListener("EffectSkillName", EffectSkillNameEvent);

    }



    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("EffectWarningText", EffectWarningTextEvent);
        EventCenter.Instance.RemoveEventListener("EffectBattleText", EffectBattleTextEvent);
        EventCenter.Instance.RemoveEventListener("EffectSkillName", EffectSkillNameEvent);

    }



    #region Warning
    private void EffectWarningTextEvent(object arg0)
    {
        EffectWarningTextInfo info = (EffectWarningTextInfo)arg0;
        InitWarningText(info);
    }

    public void InitWarningText(EffectWarningTextInfo info)
    {
        GameObject objWarning = GameObject.Instantiate(pfWarningText, tfEffectText);
        EffectWarningTextItem efWarning = objWarning.GetComponent<EffectWarningTextItem>();
        efWarning.Init(info);
    }

    #endregion


    private void EffectBattleTextEvent(object arg0)
    {
        EffectBattleTextInfo info = (EffectBattleTextInfo)arg0;
        InitBattleText(info);
    }

    public void InitBattleText(EffectBattleTextInfo info)
    {
        GameObject objBattle = GameObject.Instantiate(pfBattleText, tfEffectText);
        EffectBattleTextItem efBattle = objBattle.GetComponent<EffectBattleTextItem>();
        efBattle.Init(info);
    }

    private void EffectSkillNameEvent(object arg0)
    {
        string strName = (string)arg0;

        txSkillName.text = strName;

        if (seq != null)
        {
            seq.Kill();
        }
        seq = DOTween.Sequence();
        seq.Append(groupSkillName.DOFade(1, 0.2F));
        seq.AppendInterval(1f);
        seq.Append(groupSkillName.DOFade(0, 0.4F));
        seq.Play();
    }


}
