using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class UnitViewMgr : MonoBehaviour
{
    [Header("Character")]
    public Transform tfCharacter;
    public GameObject pfCharacter;
    public List<BattleCharacterView> listCharacterView = new List<BattleCharacterView>();
    public Dictionary<int, BattleCharacterView> dicCharacterView = new Dictionary<int, BattleCharacterView>();

    [Header("Plant")]
    public Transform tfPlant;
    public GameObject pfPlant;
    public List<BattlePlantView> listPlantView = new List<BattlePlantView>();
    public Dictionary<int, BattlePlantView> dicPlantView = new Dictionary<int, BattlePlantView>();

    [Header("Foe")]
    public Transform tfFoe;
    public GameObject pfFoe;
    public List<BattleFoeView> listFoeView = new List<BattleFoeView>();
    public Dictionary<int, BattleFoeView> dicFoeView = new Dictionary<int, BattleFoeView>();

    private GameData gameData;
    private bool isInit = false; 

    #region Bind
    public void Init()
    {
        gameData = PublicTool.GetGameData();

        InitCharacterView();
        InitPlantView();
        InitFoeView();

        PublicTool.RecalculateOccupancy();
        this.isInit = true;
    }

    #endregion

    #region Character
    public void InitCharacterView()
    {
        listCharacterView.Clear();
        dicCharacterView.Clear();
        PublicTool.ClearChildItem(tfCharacter);
        foreach(var character in gameData.listCharacter)
        {
            GenerateCharacterView(character);
        }
    }

    public void GenerateCharacterView(BattleCharacterData characterData)
    {
        GameObject objCharacter = GameObject.Instantiate(pfCharacter, tfCharacter);
        BattleCharacterView characterView = objCharacter.GetComponent<BattleCharacterView>();
        characterView.Init(characterData);
        if (!dicCharacterView.ContainsKey(characterData.keyID))
        {
            dicCharacterView.Add(characterData.keyID,characterView);
            listCharacterView.Add(characterView);
        }
    }

    public BattleCharacterView GetCharacterView(int keyID)
    {
        if (dicCharacterView.ContainsKey(keyID))
        {
            return dicCharacterView[keyID];
        }
        else
        {
            return null;
        }
    }

    public void RemoveCharacterView(int keyID)
    {
        if (dicCharacterView.ContainsKey(keyID))
        {
            BattleCharacterView view = dicCharacterView[keyID];
            dicCharacterView.Remove(keyID);
            listCharacterView.Remove(view);
            view.SelfDestroy();
        }
    }

    public BattleUnitView GetViewFromUnitInfo(UnitInfo unitInfo)
    {
        switch (unitInfo.type)
        {
            case BattleUnitType.Character:
                return GetCharacterView(unitInfo.keyID);
            case BattleUnitType.Foe:
                return GetFoeView(unitInfo.keyID);
            case BattleUnitType.Plant:
                return GetPlantView(unitInfo.keyID);
        }
        return null;
    }
    #endregion

    #region Foe
    public void InitFoeView()
    {
        listFoeView.Clear();
        dicFoeView.Clear();
        PublicTool.ClearChildItem(tfFoe);
    }

    public void GenerateFoeView(BattleFoeData foeData)
    {
        GameObject objFoe = GameObject.Instantiate(pfFoe, tfFoe);
        BattleFoeView foeView = objFoe.GetComponent<BattleFoeView>();
        foeView.Init(foeData);
        if (!dicFoeView.ContainsKey(foeData.keyID))
        {
            dicFoeView.Add(foeData.keyID, foeView);
            listFoeView.Add(foeView);
        }
        PublicTool.RecalculateOccupancy();
    }

    public BattleFoeView GetFoeView(int keyID)
    {
        if (dicFoeView.ContainsKey(keyID))
        {
            return dicFoeView[keyID];
        }
        else
        {
            return null;
        }
    }

    public void RemoveFoeView(int keyID)
    {
        if (dicFoeView.ContainsKey(keyID))
        {
            BattleFoeView view = dicFoeView[keyID];
            dicFoeView.Remove(keyID);
            listFoeView.Remove(view);
            view.SelfDestroy();
        }
    }
    #endregion

    #region Plant
    public void InitPlantView()
    {
        listPlantView.Clear();
        PublicTool.ClearChildItem(tfPlant);
        dicPlantView.Clear();
        for(int i = 0;i < gameData.listPlant.Count; i++)
        {
            BattlePlantData plantData = gameData.listPlant[i];
            GeneratePlantView(plantData);
        }
    }

    public void GeneratePlantView(BattlePlantData plantData)
    {
        GameObject objPlant = GameObject.Instantiate(pfPlant, tfPlant);
        BattlePlantView plantView = objPlant.GetComponent<BattlePlantView>();
        plantView.Init(plantData);
        if (!dicPlantView.ContainsKey(plantData.keyID))
        {
            dicPlantView.Add(plantData.keyID, plantView);
            listPlantView.Add(plantView);
        }
    }

    public BattlePlantView GetPlantView(int keyID)
    {
        if (dicPlantView.ContainsKey(keyID))
        {
            return dicPlantView[keyID];
        }
        else
        {
            return null;
        }
    }

    public void RemovePlantView(int keyID)
    {
        if (dicPlantView.ContainsKey(keyID))
        {
            BattlePlantView view = dicPlantView[keyID];
            dicPlantView.Remove(keyID);
            listPlantView.Remove(view);
            view.SelfDestroy();
        }
    }
    #endregion

    #region Unit
    public void RefreshUnitUI()
    {
        foreach(var item in listCharacterView)
        {
            item.RefreshUnitUIInfo();
        }

        foreach (var item in listFoeView)
        {
            item.RefreshUnitUIInfo();
        }

        foreach (var item in listPlantView)
        {
            item.RefreshUnitUIInfo();
        }
    }
    #endregion


}
