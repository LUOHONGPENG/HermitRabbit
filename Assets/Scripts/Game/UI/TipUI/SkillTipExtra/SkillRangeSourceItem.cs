using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillRangeSourceItem : MonoBehaviour
{
    public Image imgIcon;

    public List<Sprite> listSp;

    public void Init(BattleUnitType type)
    {
        switch (type)
        {
            case BattleUnitType.Character:
                imgIcon.sprite = listSp[0];
                break;
            case BattleUnitType.Foe:
                imgIcon.sprite = listSp[1];
                break;
            case BattleUnitType.Plant:
                imgIcon.sprite = listSp[2];
                break;
        }

    }
}
