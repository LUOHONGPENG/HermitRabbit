using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMgr : MonoBehaviour
{
    [Header("Character")]
    public Transform tfCharacter;
    public GameObject pfCharacter;
    public Dictionary<int, BattleCharacterView> dicCharacter = new Dictionary<int, BattleCharacterView>();

    private LevelMgr parent;

    public void Init(LevelMgr parent)
    {
        this.parent = parent;

        InitCharacterView();
    }

    public void InitCharacterView()
    {
        PublicTool.ClearChildItem(tfCharacter);
        GameObject objCharacter = GameObject.Instantiate(pfCharacter, new Vector3(0, 0.35f, 0), Quaternion.identity, tfCharacter);
        BattleCharacterView characterView = objCharacter.GetComponent<BattleCharacterView>();
        characterView.Init(parent.GetLevelData().GetBattleCharacterData(1001));
    }
}
