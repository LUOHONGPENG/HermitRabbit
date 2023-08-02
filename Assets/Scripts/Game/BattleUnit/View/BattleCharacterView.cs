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
        this.unitData = characterData;
        srUnit.sprite = Resources.Load(characterData.GetItem().pixelUrl, typeof(Sprite)) as Sprite;
        MoveToPos();
    }

}
