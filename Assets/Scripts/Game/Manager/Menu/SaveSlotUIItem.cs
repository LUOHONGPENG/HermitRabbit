using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SaveSlotUIItem : MonoBehaviour
{
    public enum SaveButtonType
    {
        MenuLoad,
        GameSave,
        GameLoad
    }

    public Text txSlotName;
    public Button btnSlot;
    public GameObject objInfo;
    public GameObject objNull;

    [Header("Info")]
    public Text codeDayNum;
    public Text codeMemory;
    public Text codeLv1001;
    public Text codeLv1002;

    [Header("PlantInfo")]
    public Transform tfPlant;
    public GameObject pfPlant;

    private bool isNull = true;
    private SaveSlotName slotName;
    private GameData gameData;

    public void Init(SaveButtonType saveButtonType, SaveSlotName tempSlotName,UnityAction extraAction)
    {
        this.slotName = tempSlotName;
        txSlotName.text = slotName.ToString();
        GameSaveData saveData = GameMgr.Instance.GetSaveData(slotName);
        gameData = PublicTool.GetGameData();
        CheckWhetherNull(saveData);

        btnSlot.onClick.RemoveAllListeners();
        btnSlot.onClick.AddListener(delegate ()
        {
            switch (saveButtonType)
            {
                case SaveButtonType.MenuLoad:
                    if (!isNull)
                    {
                        GameMgr.Instance.LoadScene(SceneName.Game, false, slotName);
                    }
                    break;
                case SaveButtonType.GameLoad:
                    if (!isNull)
                    {
                        //if(gameData.gamePhase == GamePhase.Battle && InputMgr.Instance.interactState == InteractState.WaitAction)
                        PublicTool.BeforeLoad();
                        GameMgr.Instance.LoadScene(SceneName.Game, false, slotName);
                    }
                    break;
                case SaveButtonType.GameSave:
                    GameMgr.Instance.SaveGameData(slotName);
                    break;
            }

            if (extraAction != null)
            {
                extraAction.Invoke();
            }
        });
    }

    private void CheckWhetherNull(GameSaveData saveData)
    {
        if (saveData != null)
        {
            isNull = false;
            RefreshInfo(saveData);
            objInfo.SetActive(true);
            objNull.SetActive(false);
        }
        else
        {
            isNull = true;
            objInfo.SetActive(false);
            objNull.SetActive(true);
        }
    }

    private void RefreshInfo(GameSaveData saveData)
    {
        codeDayNum.text = string.Format("tx_basic_dayNum".ToLanguageText(), saveData.numDay);
        codeMemory.text = saveData.memory.ToString();

        for(int i = 0; i< saveData.listCharacterExp.Count; i++)
        {
            Vector2Int characterExp = saveData.listCharacterExp[i];
            int Level = ExcelDataMgr.Instance.characterExpExcelData.GetLevelFromExp(characterExp.y);
            if (characterExp.x == 1001)
            {
                codeLv1001.text = string.Format("Lv.{0}", Level);
            }
            else if(characterExp.x == 1002)
            {
                codeLv1002.text = string.Format("Lv.{0}", Level);
            }
        }

        PublicTool.ClearChildItem(tfPlant);
        for(int i = 0; i < saveData.listPlantHeld.Count; i++)
        {
            GameObject objPlant = GameObject.Instantiate(pfPlant, tfPlant);
            SaveSlotPlantUIItem itemPlant = objPlant.GetComponent<SaveSlotPlantUIItem>();
            itemPlant.Init(saveData.listPlantHeld[i]);
        }
    }
}
