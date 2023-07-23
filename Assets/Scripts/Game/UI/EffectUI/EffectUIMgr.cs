using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUIMgr : MonoBehaviour
{
    public Transform tfEffectText;
    public GameObject pfBattleText;
    public GameObject pfWarningText;


    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("EffectWarningText", EffectWarningTextEvent);
        EventCenter.Instance.AddEventListener("EffectBattleText", EffectBattleTextEvent);

    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("EffectWarningText", EffectWarningTextEvent);
        EventCenter.Instance.RemoveEventListener("EffectBattleText", EffectBattleTextEvent);

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


}
