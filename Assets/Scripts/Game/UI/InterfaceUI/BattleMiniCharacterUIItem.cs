using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleMiniCharacterUIItem : MonoBehaviour
{
    public Button btnBg;

    public Text codeName;
    public Image imgIcon;

    public Image imgFillHP;
    public Image imgFillAP;
    public Image imgFillMove;

    [Header("Status")]
    public Image imgSelected;
    public GameObject objDead;
    public GameObject objNormal;


    private BattleCharacterData characterData;
    private GameData gameData;
    private bool isInit = false;

    public void Init(BattleCharacterData characterData)
    {
        this.characterData = characterData;
        this.gameData = PublicTool.GetGameData();

        btnBg.onClick.RemoveAllListeners();
        btnBg.onClick.AddListener(delegate ()
        {
            if (PublicTool.GetGameData().gamePhase == GamePhase.Battle && BattleMgr.Instance.battleTurnPhase == BattlePhase.CharacterPhase && InputMgr.Instance.GetInteractState() != InteractState.WaitAction)
            {
                EventCenter.Instance.EventTrigger("InputChooseCharacter", characterData.typeID);
            }
        });

        CharacterExcelItem item = PublicTool.GetCharacterExcelItem(characterData.typeID);

        codeName.text = item.name;
        imgIcon.sprite = Resources.Load("Sprite/CharacterIcon/" + item.iconUrl, typeof(Sprite)) as Sprite;


        isInit = true;
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (!isInit)
        {
            return;
        }
        imgFillHP.fillAmount = characterData.HPrate;
        imgFillAP.fillAmount = characterData.curAP * 1f / characterData.curMaxAP;
        imgFillMove.fillAmount = characterData.curMOV * 1f / characterData.curMaxMOV;

        if (characterData.isDead)
        {
            btnBg.interactable = false;
            objDead.SetActive(true);
            objNormal.SetActive(false);
        }
        else
        {
            btnBg.interactable = true;
            objDead.SetActive(false);
            objNormal.SetActive(true);
        }
    }

    public void RefreshButton(bool isSelected)
    {
        if (isSelected)
        {
            imgSelected.gameObject.SetActive(true);
        }
        else
        {
            imgSelected.gameObject.SetActive(false);
        }
    }
}
