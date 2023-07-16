using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUIMgr : MonoBehaviour
{
    public Transform tfDamageText;
    public GameObject pfDamageText;

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
                InitDamageText(info.argNum, info.pos);
                break;
        }


    }

    public void InitDamageText(float damage, Vector3 pos)
    {
        GameObject objDamage = GameObject.Instantiate(pfDamageText, tfDamageText);
        EffectDamageTextItem efDamage = objDamage.GetComponent<EffectDamageTextItem>();
        efDamage.Init(damage, pos);
    }
}
