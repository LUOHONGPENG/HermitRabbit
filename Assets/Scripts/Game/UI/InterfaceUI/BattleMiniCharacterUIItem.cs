using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMiniCharacterUIItem : MonoBehaviour
{
    public Button btnBg;

    public Image imgFillHP;
    public Image imgFillAP;
    public Image imgFillMove;

    private BattleCharacterData characterData;

    public void Init(BattleCharacterData characterData)
    {
        this.characterData = characterData;

        btnBg.onClick.RemoveAllListeners();
        btnBg.onClick.AddListener(delegate ()
        {
            if (PublicTool.GetGameData().gamePhase == GamePhase.Battle && BattleMgr.Instance.battleTurnPhase == BattlePhase.CharacterPhase && InputMgr.Instance.GetInteractState() != InteractState.WaitAction)
            {
                EventCenter.Instance.EventTrigger("InputChooseCharacter", characterData.typeID);
            }
        });

        RefreshUI();
    }

    public void RefreshUI()
    {
        imgFillHP.fillAmount = characterData.HPrate;
        imgFillAP.fillAmount = characterData.curAP * 1f / characterData.curMaxAP;
        imgFillMove.fillAmount = characterData.curMOV * 1f / characterData.curMaxMOV;
    }
}
