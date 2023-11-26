using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillSPCostUIMgr : MonoBehaviour
{
    public TextMeshProUGUI codeCost;


    public void Init(int costNum)
    {
        codeCost.text = string.Format("Cost: {0}", costNum);
    }



}
