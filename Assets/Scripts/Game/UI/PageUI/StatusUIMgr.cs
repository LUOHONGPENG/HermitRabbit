using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Text codeATK;
    public Text codeDEF;
    public Text codeRES;

    public Button btnChoose1001;
    public Button btnChoose1002;

    public BattleCharacterData characterData;

    public void Init() 
    {
        characterData = null;
    }

    public void RefreshCharacterInfo()
    {

    }
}
