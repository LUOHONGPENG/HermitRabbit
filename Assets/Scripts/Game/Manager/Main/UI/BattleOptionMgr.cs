using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOptionMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Transform tfOption;
    public GameObject pfOption;

    public void Init()
    {
        objPopup.SetActive(true);

        GameObject objOption = GameObject.Instantiate(pfOption, tfOption);
        BattleOptionItem itemOption = objOption.GetComponent<BattleOptionItem>();
        itemOption.Init("11", delegate () { 
            Debug.Log(11); 
        });
    }
}
