using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlantView : BattleUnitView
{
    public BattlePlantData plantData;

    public void Init(BattlePlantData plantData)
    {
        this.plantData = plantData;
        this.unitData = plantData;

        CommonInit();

        srUnit.sprite = Resources.Load("Sprite/Plant/" + plantData.GetItem().pixelUrl, typeof(Sprite)) as Sprite;
        MoveToPos();
        isInit = true;

    }
}
