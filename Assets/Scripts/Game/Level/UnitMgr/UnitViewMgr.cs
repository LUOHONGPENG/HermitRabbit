using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitViewMgr : MonoBehaviour
{
    [Header("Character")]
    public Transform tfCharacter;
    public GameObject pfCharacter;
    public Dictionary<int, BattleCharacterView> dicCharacterView = new Dictionary<int, BattleCharacterView>();

    [Header("Plant")]
    public Transform tfPlant;
    public GameObject pfPlant;
    public Dictionary<int, BattlePlantView> dicPlantView = new Dictionary<int, BattlePlantView>();

    [Header("Foe")]
    public Transform tfFoe;
    public GameObject pfFoe;
    public Dictionary<int, BattleFoeView> dicFoeView = new Dictionary<int, BattleFoeView>();

    private bool isInit = false;

    #region Bind
    public void Init()
    {
        InitCharacterView();
        this.isInit = true;
    }

    #endregion

    #region Character
    public void InitCharacterView()
    {
        dicCharacterView.Clear();
        PublicTool.ClearChildItem(tfCharacter);
        foreach(var character in PublicTool.GetGameData().listCharacter)
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
        }
        PublicTool.RecalculateOccupancy();
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
    public BattleUnitView GetViewFromUnitInfo(UnitInfo unitInfo)
    {
        Debug.Log(unitInfo.type + " " + unitInfo.keyID);
        switch (unitInfo.type)
        {
            case BattleUnitType.Character:
                return GetCharacterView(unitInfo.keyID);
            case BattleUnitType.Foe:
                return GetFoeView(unitInfo.keyID);
        }
        return null;
    }

    #endregion

    #region Foe
    public void GenerateFoeView(BattleFoeData foeData)
    {
        GameObject objFoe = GameObject.Instantiate(pfFoe, tfFoe);
        BattleFoeView foeView = objFoe.GetComponent<BattleFoeView>();
        foeView.Init(foeData);
        if (!dicFoeView.ContainsKey(foeData.keyID))
        {
            dicFoeView.Add(foeData.keyID, foeView);
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
            view.SelfDestroy();
        }
    }
    #endregion

    #region Plant
    public void GeneratePlantView(BattlePlantData plantData)
    {
        GameObject objPlant = GameObject.Instantiate(pfPlant, tfPlant);
        BattlePlantView plantView = objPlant.GetComponent<BattlePlantView>();
        plantView.Init(plantData);
        if (!dicPlantView.ContainsKey(plantData.keyID))
        {
            dicPlantView.Add(plantData.keyID, plantView);
        }
        PublicTool.RecalculateOccupancy();
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
            view.SelfDestroy();
        }
    }


    #endregion

}
