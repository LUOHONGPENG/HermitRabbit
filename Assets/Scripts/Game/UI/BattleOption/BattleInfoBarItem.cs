using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInfoBarItem : MonoBehaviour
{
    public Image imgFill;
    public Text codeInfo;
    public List<Color> listColor = new List<Color>();
    public List<Color> listColorText = new List<Color>();

    private BarResourceType resourceType;

    public void Init(BarResourceType barResourceType)
    {
        resourceType = barResourceType;
        switch (resourceType)
        {
            case BarResourceType.Health:
                imgFill.color = listColor[0];
                codeInfo.color = listColorText[0];
                break;
            case BarResourceType.Skill:
                imgFill.color = listColor[1];
                codeInfo.color = listColorText[1];
                break;
            case BarResourceType.Move:
                imgFill.color = listColor[2];
                codeInfo.color = listColorText[2];
                break;
        }
    }

    public void UpdateData(float cur, float max)
    {
        imgFill.fillAmount = (float)cur / (float)max;
        codeInfo.text = string.Format("{0}/{1}", (int)cur, (int)max);
    }
}
