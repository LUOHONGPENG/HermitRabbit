using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        CharacterExcelItem excelItem = characterData.GetItem();

        CommonInit();


        if (excelItem.aniUrl.Length > 1)
        {
            aniUnit.runtimeAnimatorController = Resources.Load("Ani/Character/" + excelItem.aniUrl) as RuntimeAnimatorController;
            ChangeAniState(UnitAniState.Idle);

        }
        else
        {
            srUnit.sprite = Resources.Load("Sprite/Character/" + excelItem.pixelUrl, typeof(Sprite)) as Sprite;
        }

        MoveToPos();
        isInit = true;

    }

    

}
