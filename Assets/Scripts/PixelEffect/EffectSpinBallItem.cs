using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpinBallItem : MonoBehaviour
{
    private bool isInit = false;

    public Transform tfSr;
    public SpriteRenderer srBall;
    public List<Sprite> listSpBall = new List<Sprite>();
    
    public void Init()
    {
        isInit = true;
    }

    public void SetBall(int skillID)
    {
        switch (skillID)
        {
            case 1101:
            case 1102:
                srBall.sprite = listSpBall[2];
                break;
            case 1201:
                srBall.sprite = listSpBall[1];
                break;
            case 1301:
            case 1302:
                srBall.sprite = listSpBall[0];
                break;
            default:
                srBall.sprite = null;
                break;
        }
    }

    private void LateUpdate()
    {
        if (isInit)
        {
            tfSr.LookAt(Camera.main.transform.forward + tfSr.position);
        }
    }


}
