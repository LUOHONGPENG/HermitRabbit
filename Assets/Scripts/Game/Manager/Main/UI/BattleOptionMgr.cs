using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOptionMgr : MonoBehaviour
{
    public Transform tfPage;
    public GameObject pfPage;

    public void Init()
    {
        GameObject objPage = GameObject.Instantiate(pfPage, tfPage);
        BattleOptionPage itemPage = objPage.GetComponent<BattleOptionPage>();
        itemPage.Init();
    }
}
