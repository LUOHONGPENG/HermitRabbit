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
        RefreshUI();
    }

    public void RefreshUI()
    {
        imgFillHP.fillAmount = characterData.curHP / characterData.maxHP;
        imgFillAP.fillAmount = characterData.curAP / characterData.maxAP;
        imgFillMove.fillAmount = characterData.curMOV / characterData.maxMOV;
    }
}
