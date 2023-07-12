using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMgr : MonoBehaviour
{
    [Header("Character")]
    public Transform tfCharacter;
    public GameObject pfCharacter;
    //public List<BattleCharacterView>  
    public Dictionary<int, BattleCharacterView> dicCharacter = new Dictionary<int, BattleCharacterView>();

    [Header("Foe")]
    public Transform tfFoe;
    public GameObject pfFoe;




    private LevelMgr parent;

    public void Init(LevelMgr parent)
    {
        this.parent = parent;

        InitCharacterView();
    }

    public void InitCharacterView()
    {
        PublicTool.ClearChildItem(tfCharacter);
        foreach(var character in parent.GetLevelData().listCharacter)
        {
            GenerateCharacterView(character);
        }


    }

    public void GenerateCharacterView(BattleCharacterData characterData)
    {
        GameObject objCharacter = GameObject.Instantiate(pfCharacter, tfCharacter);
        BattleCharacterView characterView = objCharacter.GetComponent<BattleCharacterView>();
        characterView.Init(characterData);
        //characterView.Init(parent.GetLevelData().GetBattleCharacterData(1001));
    }
}
