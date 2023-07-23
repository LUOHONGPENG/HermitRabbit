using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EffectPosTextItem : MonoBehaviour
{
    protected Vector3 posSource;
    protected bool isInit = false;

    public Text txContent;
    protected Sequence seq;

    private void Update()
    {
        if (isInit)
        {
            transform.localPosition = PublicTool.CalculateScreenUIPos(posSource, GameMgr.Instance.curMapCamera);
        }
    }

    public void SetPosContent(string content,Vector3 pos)
    {
        txContent.text = content;
        this.posSource = pos;
    }
}
