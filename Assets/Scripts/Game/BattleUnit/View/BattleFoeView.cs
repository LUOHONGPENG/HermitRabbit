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

        srUnit.sprite = Resources.Load("Sprite/Foe/"+foeData.GetItem().pixelUrl, typeof(Sprite)) as Sprite;


        MoveToPos();
    }


}
