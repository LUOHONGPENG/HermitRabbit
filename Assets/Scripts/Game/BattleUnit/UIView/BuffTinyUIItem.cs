using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffTinyUIItem : MonoBehaviour
{
    public Image imgIcon;
    public Text codeBuffLevel;

    private int buffID = -1;

    public int GetBuffID()
    {
        return buffID;
    }


    public void Init(Buff buffInfo)
    {
        buffID = buffInfo.id;

        codeBuffLevel.text = buffInfo.GetLevel().ToString();
        if(buffInfo.effectType == SkillEffectType.Harm)
        {
            imgIcon.color = Color.red;
        }
        else if(buffInfo.effectType == SkillEffectType.Help)
        {
            imgIcon.color = Color.blue;
        }
    }
}
