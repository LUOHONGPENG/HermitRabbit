using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EffectWarningTextItem : EffectPosTextItem
{
    public void Init(EffectWarningTextInfo info)
    {
        Vector3 pos3D = PublicTool.ConvertPosFromID(info.posID);
        pos3D = new Vector3(pos3D.x, 0.5f, pos3D.z);
        SetPosContent(info.content, pos3D);

        transform.localPosition = PublicTool.CalculateScreenUIPosText(posSource, GameMgr.Instance.curMapCamera);

        seq = DOTween.Sequence();
        seq.Append(txContent.transform.DOLocalMoveY(200F, 2F));
        seq.Insert(0.6f, txContent.DOFade(0, 2f));

        Destroy(gameObject, 3f);
        isInit = true;
    }
}
