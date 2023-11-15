using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFoeView : BattleUnitView
{
    public BattleFoeData foeData;
    

    public void Init(BattleFoeData foeData)
    {
        this.foeData = foeData;
        this.unitData = foeData;

        CommonInit();

        FoeExcelItem excelItem = foeData.GetItem();

        if (excelItem.aniUrl.Length > 1)
        {
            aniUnit.runtimeAnimatorController = Resources.Load("Ani/Foe/" + excelItem.aniUrl) as RuntimeAnimatorController;
            ChangeAniState(UnitAniState.Idle);
        }
        else
        {
            srUnit.sprite = Resources.Load("Sprite/Foe/" + foeData.GetItem().pixelUrl, typeof(Sprite)) as Sprite;
        }

        MoveToPos();

        isInit = true;
    }


}
