using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffTinyUIItem : MonoBehaviour
{
    public Image imgIcon;
    public Text codeBuffLevel;

    public void Init(Buff buffInfo)
    {
        codeBuffLevel.text = buffInfo.GetLevel().ToString();
    }
}
