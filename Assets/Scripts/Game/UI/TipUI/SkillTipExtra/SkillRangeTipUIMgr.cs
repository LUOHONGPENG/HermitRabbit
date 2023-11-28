using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillRangeTipUIMgr : MonoBehaviour
{
    public Text txRange;
    public Text txRadius;

    public CommonRangeUIMgr rangeIconUI;

    public void Init(SkillRegionType regionType,int range, int radius)
    {
        rangeIconUI.Init(regionType, range, radius);

        txRange.text = range.ToString();
        txRadius.text = radius.ToString();
    }

}
