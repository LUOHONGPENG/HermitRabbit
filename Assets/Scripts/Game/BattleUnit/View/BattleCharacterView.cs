using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterView : BattleUnitViewBase
{
    public BattleCharacterData characterData;

    public void Init(BattleCharacterData characterData)
    {
        this.characterData = characterData;
    }
}
