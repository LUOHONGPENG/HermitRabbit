using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EffectDamageTextItem : MonoBehaviour
{
    public Text txContent;

    private Sequence seq;
    private Vector3 posSource;
    private bool isInit = false;

    public void Init(float info, Vector3 posSource)
    {
        txContent.text = ((int)info).ToString();

        this.posSource = posSource;

        transform.localPosition = PublicTool.CalculateScreenUIPos(posSource,GameMgr.Instance.curMapCamera);

        seq = DOTween.Sequence();
        seq.Append(txContent.transform.DOLocalMoveY(200F, 2F));
        seq.Insert(0.6f, txContent.DOFade(0, 2f));

        Destroy(gameObject, 3f);
        isInit = true;
    }

    private void Update()
    {
        if (isInit)
        {
            transform.localPosition = PublicTool.CalculateScreenUIPos(posSource, GameMgr.Instance.curMapCamera);
        }
    }
}
