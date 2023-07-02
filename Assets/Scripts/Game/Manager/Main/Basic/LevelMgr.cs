using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour
{
    [Header("MapTile")]
    public Transform tfMapTile;
    public GameObject pfMapTile;
    public Dictionary<Vector2, MapTileBase> dicMapTile = new Dictionary<Vector2, MapTileBase>();
    [Header("Character")]
    public Transform tfCharacter;
    public GameObject pfCharacter;
    public Dictionary<int, BattleCharacterView> dicCharacter = new Dictionary<int, BattleCharacterView>();

    private LevelData levelData;

    public void Init()
    {
        //If New Game
        levelData = new LevelData();
        levelData.Init();

        InitMapTileView();

        InitCharacterView();
    }

    #region MapTile

    public void InitMapTileView()
    {
        dicMapTile.Clear();
        PublicTool.ClearChildItem(tfMapTile);
        GenerateTile(5, 5);
    }

    public void GenerateTile(int sizeX,int sizeZ)
    {
        int centerX = (sizeX - 1) / 2;
        int centerZ = (sizeZ - 1) / 2;


        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                GameObject objMapTile = GameObject.Instantiate(pfMapTile, new Vector3(i - centerX, 0, j - centerZ), Quaternion.identity, tfMapTile);
                objMapTile.name = string.Format("MapTile{0}_{1}", i, j);
                dicMapTile.Add(new Vector2(i, j), objMapTile.GetComponent<MapTileBase>());
            }
        }
    }

    #endregion

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
