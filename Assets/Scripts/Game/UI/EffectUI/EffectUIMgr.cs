using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUIMgr : MonoBehaviour
{
    public Transform tfEffectText;
    public GameObject pfDamageText;
    public GameObject pfWarningText;


    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("EffectUIText", EffectUITextEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("EffectUIText", EffectUITextEvent);
    }

    private void EffectUITextEvent(object arg0)
    {
        EffectUITextInfo info = (EffectUITextInfo)arg0;
        switch (info.type)
        {
            case EffectUITextType.Damage:
                InitDamageText(info.argString, info.posID);
                break;
            case EffectUITextType.Warning:
                InitWarningText(info.argString, info.posID);
                break;
        }


    }

    public void InitDamageText(string info, Vector2Int posID)
    {
        GameObject objDamage = GameObject.Instantiate(pfDamageText, tfEffectText);
        EffectDamageTextItem efDamage = objDamage.GetComponent<EffectDamageTextItem>();
        efDamage.Init(info, posID);
    }

    public void InitWarningText(string content, Vector2Int posID)
    {
        GameObject objWarning = GameObject.Instantiate(pfWarningText, tfEffectText);
        EffectWarningTextItem efWarning = objWarning.GetComponent<EffectWarningTextItem>();
        efWarning.Init(content, posID);
    }
}
