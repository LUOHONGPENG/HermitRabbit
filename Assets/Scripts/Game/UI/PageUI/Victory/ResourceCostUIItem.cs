using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCostUIItem : MonoBehaviour
{
    public Image imgIcon;
    public Text codeCostNum;

    public void Init(int num)
    {
        if(num >= 0)
        {
            codeCostNum.text = string.Format("+{0}", num);
        }
        else
        {
            codeCostNum.text = string.Format("{0}", num);
        }
    }

}
