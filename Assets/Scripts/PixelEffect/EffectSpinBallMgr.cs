using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpinBallMgr : MonoBehaviour
{
    public Transform tfBall;

    public List<EffectSpinBallItem> listSpinBall = new List<EffectSpinBallItem>();

    

    public void Init()
    {
        foreach(var item in listSpinBall)
        {
            item.Init();
        }
    }

    public void ShowBall(int skillID)
    {
        foreach (var item in listSpinBall)
        {
            item.SetBall(skillID);
        }

        tfBall.gameObject.SetActive(true);
    }

    public void HideBall()
    {
        tfBall.gameObject.SetActive(false);
    }
}
