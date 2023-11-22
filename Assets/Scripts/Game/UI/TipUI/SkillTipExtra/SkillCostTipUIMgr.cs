using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCostTipUIMgr : MonoBehaviour
{
    public GameObject objPopus;

    [Header("Cost")]
    public GameObject objCostAP;
    public TextMeshProUGUI codeCostAP;
    public GameObject objCostMOV;
    public TextMeshProUGUI codeCostMOV;
    public GameObject objCostHP;
    public TextMeshProUGUI codeCostHP;
    public GameObject objCostMemory;
    public TextMeshProUGUI codeCostMemory;

    public void ShowCost()
    {
        objPopus.SetActive(true);
    }

    public void HideCost()
    {
        objPopus.SetActive(false);
    }

    public void UpdateUI(int AP,int MOV,int HP,int Memory)
    {
        if (AP > 0)
        {
            objCostAP.SetActive(true);
            codeCostAP.text = AP.ToString();
        }
        else
        {
            objCostAP.SetActive(false);
        }

        if (MOV > 0)
        {
            objCostMOV.SetActive(true);
            codeCostMOV.text = MOV.ToString();
        }
        else
        {
            objCostMOV.SetActive(false);
        }

        if (HP > 0)
        {
            objCostHP.SetActive(true);
            codeCostHP.text = HP.ToString();
        }
        else
        {
            objCostHP.SetActive(false);
        }

        if (Memory > 0)
        {
            objCostMemory.SetActive(true);
            codeCostMemory.text = Memory.ToString();
        }
        else
        {
            objCostMemory.SetActive(false);
        }
    }
}
