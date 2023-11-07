using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleCharacterView : BattleUnitView
{
    public BattleCharacterData characterData;
    public EffectSpinBallMgr effectSpinBallMgr;


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

        //Effect
        effectSpinBallMgr.Init();

        isInit = true;

    }

    public override void ChangeAniState(UnitAniState state)
    {
        base.ChangeAniState(state);

        //Ball
        if (state == UnitAniState.Ready)
        {
            if(GetTypeID() == 1001)
            {
                effectSpinBallMgr.ShowBall(PublicTool.GetGameData().GetCurSkillBattleInfo().ID);
            }
        }
        else
        {
            effectSpinBallMgr.HideBall();
        }

    }


}
