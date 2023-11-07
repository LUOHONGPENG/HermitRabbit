using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpinBallItem : MonoBehaviour
{
    private bool isInit = false;

    public Transform tfSr;
    public SpriteRenderer srBall;

    public void Init()
    {
        isInit = true;
    }

    private void LateUpdate()
    {
        if (isInit)
        {
            tfSr.LookAt(Camera.main.transform.forward + tfSr.position);
        }
    }


}
