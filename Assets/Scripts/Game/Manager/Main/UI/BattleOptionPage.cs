using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleOptionPage : MonoBehaviour
{
    public Transform tfOption;
    public GameObject pfOption;

    public void Init()
    {
        InitOption("111", delegate () { Debug.Log(111); });
        InitOption("222", delegate () { Debug.Log(111); });

    }

    public void InitOption(string strOption, UnityAction action)
    {
        GameObject objOption = GameObject.Instantiate(pfOption, tfOption);
        BattleOptionItem itemOption = objOption.GetComponent<BattleOptionItem>();
        itemOption.Init(strOption, action);
    }
}
