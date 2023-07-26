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

        //srUnit.sprite = Resources.Load(plantData.GetItem().pixelUrl, typeof(Sprite)) as Sprite;

        //Need to modify it
        List<Vector2Int> listPos = PublicTool.GetGameData().listTempEmptyPos;
        int ran = Random.Range(0, listPos.Count);
        plantData.posID = listPos[ran];
        MoveToPos(plantData.posID);
    }
}
