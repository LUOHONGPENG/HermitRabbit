using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    public MapMgr mapMgr;

    [Header("Character")]
    public Transform tfCharacter;
    public GameObject pfCharacter;
    public Dictionary<int, BattleCharacterView> dicCharacter = new Dictionary<int, BattleCharacterView>();

    private LevelData levelData;
    private bool isInit = false;

    public void Init()
    {
        //If New Game
        levelData = new LevelData();
        levelData.Init();

        mapMgr.Init(this);

        InitCharacterView();

        isInit = true;
    }

    public LevelData GetLevelData()
    {
        return levelData;
    }

    private void FixedUpdate()
    {
        mapMgr.TimeGo();
    }


    #region Character
    public void InitCharacterView()
    {
        PublicTool.ClearChildItem(tfCharacter);
        GameObject objCharacter = GameObject.Instantiate(pfCharacter, new Vector3(0, 0.35f, 0), Quaternion.identity, tfCharacter);
        BattleCharacterView characterView = objCharacter.GetComponent<BattleCharacterView>();
        characterView.Init(levelData.GetBattleCharacterData(1001));
    }
    #endregion
}
