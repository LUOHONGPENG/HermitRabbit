using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUIMgr : MonoBehaviour
{
    [Header("Basic")]
    public GameObject objPopup;
    public GameObject objMapClip;
    public GameObject objPlant;
    public Button btnContinue;

    [Header("EXP")]
    public Transform tfExpFrame;
    public GameObject pfExpFrame;
    private List<Vector2Int> listGrowExp;
    private List<VictoryExpUIItem> listExpFrame = new List<VictoryExpUIItem>();
    private Dictionary<int, int> dicBeforeExp = new Dictionary<int, int>();
    private Dictionary<int, int> dicGrowExp = new Dictionary<int, int>();
    public ResourceCostUIItem battleBonusMemory;
    public ResourceCostUIItem battleBonusEssence;


    [Header("MapClip")]
    public Transform tfMapClip;
    public GameObject pfMapClip;
    public Button btnRefreshMap;
    public Button btnSkipMap;
    public ResourceCostUIItem costRefreshMap;
    public ResourceCostUIItem costSkipMap;

    [Header("MapTile")]
    public Transform tfTileTip;
    public GameObject pfTileTip;

    [Header("Plant")]
    public Transform tfPlant;
    public GameObject pfPlant;
    public Button btnRefreshPlant;
    public Button btnSkipPlant;
    public ResourceCostUIItem costRefreshPlant;
    public ResourceCostUIItem costSkipPlant;

    private VictoryPhase victoryPhase;
    private GameData gameData;

    public enum VictoryPhase
    {
        Start,
        EXP,
        MapClip1,
        MapClip2,
        Plant,
        End
    }

    #region Basic
    public void Init()
    {
        gameData = PublicTool.GetGameData();

        btnContinue.onClick.RemoveAllListeners();
        btnContinue.onClick.AddListener(delegate ()
        {
            //Need to modify
            if(victoryPhase == VictoryPhase.End)
            {
                HidePopup();

                if(gameData.numDay == 2)
                {
                    EventCenter.Instance.EventTrigger("TalkStart", TalkGroup.Day2);
                }

            }
        });

        btnSkipMap.onClick.RemoveAllListeners();
        btnSkipMap.onClick.AddListener(delegate ()
        {
            SkipMapClip();
        });

        btnRefreshMap.onClick.RemoveAllListeners();
        btnRefreshMap.onClick.AddListener(delegate ()
        {
            if(GameGlobal.CostRefreshMapClip <= gameData.GetMemory())
            {
                gameData.CostMemory(GameGlobal.CostRefreshMapClip);
                DrawMapClip();
            }
            else
            {

            }
        });

        btnSkipPlant.onClick.RemoveAllListeners();
        btnSkipPlant.onClick.AddListener(delegate ()
        {
            SkipPlant();
        });

        btnRefreshPlant.onClick.RemoveAllListeners();
        btnRefreshPlant.onClick.AddListener(delegate ()
        {
            if (GameGlobal.CostRefreshPlant <= gameData.GetMemory())
            {
                gameData.CostMemory(GameGlobal.CostRefreshPlant);
                DrawPlant();
            }
            else
            {

            }
        });

        costRefreshMap.Init(ResourceType.Memory,-GameGlobal.CostRefreshMapClip);
        costSkipMap.Init(ResourceType.Memory, GameGlobal.AddSkipMapClip);
        costRefreshPlant.Init(ResourceType.Memory, -GameGlobal.CostRefreshPlant);
        costSkipPlant.Init(ResourceType.Memory, GameGlobal.AddSkipPlant);

        PublicTool.ClearChildItem(tfTileTip);
        for(MapTileType i = MapTileType.Normal; i < MapTileType.End; i++)
        {
            GameObject objTileTip = GameObject.Instantiate(pfTileTip, tfTileTip);
            VictoryTileTipUIItem itemTileTip = objTileTip.GetComponent<VictoryTileTipUIItem>();
            itemTileTip.Init(i);
        }
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("NormalVictoryStart", ShowVictoryEvent);
        EventCenter.Instance.AddEventListener("VictoryAddMapClip", AddMapClipEvent);
        EventCenter.Instance.AddEventListener("VictoryAddPlant", AddPlantEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("NormalVictoryStart", ShowVictoryEvent);
        EventCenter.Instance.RemoveEventListener("VictoryAddMapClip", AddMapClipEvent);
        EventCenter.Instance.RemoveEventListener("VictoryAddPlant", AddPlantEvent);
    }


    private void ShowVictoryEvent(object arg0)
    {
        victoryPhase = VictoryPhase.Start;
        listGrowExp = (List<Vector2Int>)arg0;
        NextPhase();
    }

    public void HidePopup()
    {
        if (gameData.gamePhase == GamePhase.Peace)
        {
            GameMgr.Instance.SaveGameData(SaveSlotName.Auto);
        }
        objPopup.SetActive(false);
    }

    public void NextPhase()
    {
        victoryPhase++;
        switch (victoryPhase)
        {
            case VictoryPhase.EXP:
                CommonReward();
                StartExpPhase();
                objMapClip.SetActive(false);
                objPlant.SetActive(false);
                btnContinue.gameObject.SetActive(false);
                break;
            case VictoryPhase.MapClip1:
                StartMapClipPhase();
                break;
            case VictoryPhase.MapClip2:
                StartMapClipPhase();
                break;
            case VictoryPhase.Plant:
                objMapClip.SetActive(false);
                StartPlantPhase();
                break;
            case VictoryPhase.End:
                objPlant.SetActive(false);
                btnContinue.gameObject.SetActive(true);
                break;
        }
    }

    private void CommonReward()
    {
        battleBonusMemory.Init(ResourceType.Memory, GameGlobal.BattleBonusMemory);
        gameData.AddMemory(GameGlobal.BattleBonusMemory);
        battleBonusEssence.Init(ResourceType.Essence, GameGlobal.BattleBonusEssence);
        gameData.AddEssenceLimit(GameGlobal.BattleBonusEssence);

    }

    #endregion

    #region EXP

    private void StartExpPhase()
    {
        //Load Exp Info
        dicBeforeExp.Clear();
        dicGrowExp.Clear();
        foreach (Vector2Int expInfo in listGrowExp)
        {
            if (!dicGrowExp.ContainsKey(expInfo.x))
            {
                dicGrowExp.Add(expInfo.x, expInfo.y);
            }
            if (!dicBeforeExp.ContainsKey(expInfo.x))
            {
                int BeforeExp = gameData.GetBattleCharacterData(expInfo.x).EXP;
                dicBeforeExp.Add(expInfo.x, BeforeExp);
            }
            //Give Reward
            gameData.AddCharacterExp(expInfo.x, expInfo.y);
        }
        //ShowExp
        ShowExp();
        objPopup.SetActive(true);
        StartCoroutine(IE_ExpGrow());
    }

    public void ShowExp()
    {
        listExpFrame.Clear();
        PublicTool.ClearChildItem(tfExpFrame);

        foreach (var info in dicBeforeExp)
        {
            GameObject objExpFrame = GameObject.Instantiate(pfExpFrame, tfExpFrame);
            VictoryExpUIItem itemExpFrame = objExpFrame.GetComponent<VictoryExpUIItem>();
            itemExpFrame.Init(info.Key, info.Value, dicGrowExp[info.Key]);
            listExpFrame.Add(itemExpFrame);
        }

    }

    public IEnumerator IE_ExpGrow()
    {
        for(int i = 0; i < listExpFrame.Count; i++)
        {
            listExpFrame[i].GrowExp();
        }

        yield return new WaitUntil(()=> CheckAllExpDone());
        NextPhase();
    }

    private bool CheckAllExpDone()
    {
        bool isDone = true;
        for (int i = 0; i < listExpFrame.Count; i++)
        {
            if (!listExpFrame[i].isGrowExp)
            {
                isDone = false;
            }
        }
        return isDone;
    }
    #endregion

    #region MapClip

    public void StartMapClipPhase()
    {
        DrawMapClip();

        objMapClip.SetActive(true);
    }

    public void DrawMapClip()
    {
        //Draw
        MapClipExcelData mapClipExcel = ExcelDataMgr.Instance.mapClipExcelData;
        List<int> listPool = mapClipExcel.GetAllMapClipID();
        List<int> listDelete = new List<int>(gameData.listMapClipHeld);
        List<int> listWeight = new List<int>();
        for(int i = 0; i < listPool.Count; i++)
        {
            int keyID = listPool[i];
            listWeight.Add(mapClipExcel.GetMapClipWeight(keyID));
        }
        List<int> listDraw = PublicTool.DrawNumWeight(3, listPool, listWeight, listDelete);

        PublicTool.ClearChildItem(tfMapClip);
        for (int i = 0; i < listDraw.Count; i++)
        {
            GameObject objMapClip = GameObject.Instantiate(pfMapClip, tfMapClip);
            VictoryMapClipUIItem itemMapClip = objMapClip.GetComponent<VictoryMapClipUIItem>();
            itemMapClip.Init(listDraw[i]);
        }
    }

    private void AddMapClipEvent(object arg0)
    {
        gameData.AddMapClipHeld((int)arg0);

        NextPhase();
    }

    private void SkipMapClip()
    {
        gameData.AddMemory(GameGlobal.AddSkipMapClip);
        NextPhase();
    }


    #endregion

    #region ShowPlant

    public void StartPlantPhase()
    {
        DrawPlant();

        objPlant.SetActive(true);
    }

    public void DrawPlant()
    {
        //Draw
        List<int> listPool = ExcelDataMgr.Instance.plantExcelData.GetAllPlantID();
        List<int> listDelete = new List<int>(gameData.listPlantHeld);
        List<int> listDraw = PublicTool.DrawNum(2, listPool, listDelete);

        PublicTool.ClearChildItem(tfPlant);
        for (int i = 0; i < listDraw.Count; i++)
        {
            GameObject objPlant = GameObject.Instantiate(pfPlant, tfPlant);
            VictoryPlantItem itemPlant = objPlant.GetComponent<VictoryPlantItem>();
            itemPlant.Init(listDraw[i]);
        }
    }


    private void AddPlantEvent(object arg0)
    {
        gameData.AddPlantHeld((int)arg0);

        NextPhase();
    }
    private void SkipPlant()
    {
        gameData.AddMemory(GameGlobal.AddSkipPlant);
        NextPhase();
    }
    #endregion
}
