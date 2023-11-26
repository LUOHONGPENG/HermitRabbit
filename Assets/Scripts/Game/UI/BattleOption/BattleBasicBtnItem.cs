using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBasicBtnItem : BattleBtnBase
{
    public enum BattleBasicBtnType
    {
        Move,
        Attack
    }

    public Image imgBtn;
    public List<Sprite> listSpBtn = new List<Sprite>();

    public void Init(BattleBasicBtnType type)
    {
/*        switch (type)
        {
            case BattleBasicBtnType.Move:
                imgBtn.sprite = listSpBtn[0];
                break;
            case BattleBasicBtnType.Attack:
                imgBtn.sprite = listSpBtn[1];
                break;
        }*/
    }
}
