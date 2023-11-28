using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRangeTargetItem : MonoBehaviour
{
    public GameObject objFire;

    public GameObject objWater;

    public void Init(SkillRegionType regionType)
    {
        switch (regionType)
        {
            case SkillRegionType.BurnUnit:
                objFire.SetActive(true);
                objWater.SetActive(false);
                break;
            case SkillRegionType.Water:
                objFire.SetActive(false);
                objWater.SetActive(true);
                break;
            default:
                objFire.SetActive(false);
                objWater.SetActive(false);
                break;
        }
    }
}
