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

        srUnit.sprite = Resources.Load(foeData.GetItem().pixelUrl, typeof(Sprite)) as Sprite;

        List<Vector2Int> listPos = PublicTool.GetGameData().listTempEmptyPos;
        int ran = Random.Range(0, listPos.Count);
        foeData.posID = listPos[ran];
        MoveToPos(foeData.posID,false);
    }


}
