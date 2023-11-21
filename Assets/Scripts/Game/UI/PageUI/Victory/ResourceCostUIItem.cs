using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCostUIItem : MonoBehaviour
{
    public Image imgIcon;
    public Text codeCostNum;

    public List<Sprite> listSp = new List<Sprite>();

    public void Init(ResourceType resourceType,int num)
    {
        switch (resourceType)
        {
            case ResourceType.Essence:
                imgIcon.sprite = listSp[0];
                break;
            case ResourceType.Memory:
                imgIcon.sprite = listSp[1];
                break;
        }
        imgIcon.SetNativeSize();


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
