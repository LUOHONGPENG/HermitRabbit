using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleOptionPage : MonoBehaviour
{
    public Transform tfOption;
    public GameObject pfOption;
    public RectTransform rtPage;

    [HideInInspector]
    private BattleOptionMgr parent;

    public void Init(BattleOptionMgr parent,int count)
    {
        this.parent = parent;

        if(count == 1)
        {
            SetPagePos(new Vector2(200F, -200F));
        }

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

    public void SetPagePos(Vector2 pos)
    {
        rtPage.localPosition = pos;
    }
}
