using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleOptionPage : MonoBehaviour
{
    public Transform tfOption;
    public GameObject pfOption;

    [HideInInspector]
    private BattleOptionMgr parent;

    public void Init(BattleOptionMgr parent)
    {
        this.parent = parent;

        InitOption("Move", delegate () { Debug.Log("Move"); });
        InitOption("Action", delegate () { Debug.Log("Action Page"); });
        InitOption("Wait", delegate () { Debug.Log("Wait"); });
    }

    public void InitOption(string strOption, UnityAction action)
    {
        GameObject objOption = GameObject.Instantiate(pfOption, tfOption);
        BattleOptionItem itemOption = objOption.GetComponent<BattleOptionItem>();
        itemOption.Init(strOption, action);
    }

}
