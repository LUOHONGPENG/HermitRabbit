using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterView : BattleUnitView
{
    public BattleCharacterData characterData;

    public int GetTypeID()
    {
        return characterData.GetTypeID();
    }

    public void Init(BattleCharacterData characterData)
    {
        this.characterData = characterData;
        srUnit.sprite = Resources.Load(characterData.GetItem().pixelUrl, typeof(Sprite)) as Sprite;
        ResetPosition();
    }

    public void ResetPosition()
    {
        Vector3 tilePos = PublicTool.ConvertPosFromID(characterData.posID);
        this.transform.localPosition = new Vector3(tilePos.x, 0.35f, tilePos.z);
    }
}
