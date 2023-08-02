using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EffectBattleTextItem : EffectPosTextItem
{
    public List<Color> listColor = new List<Color>();

    public void Init(EffectBattleTextInfo info)
    {
        switch (info.type)
        {
            case BattleTextType.Damage:
                txContent.color = listColor[0];
                break;
            case BattleTextType.Heal:
                txContent.color = listColor[1];
                break;
            case BattleTextType.Debuff:
                txContent.color = listColor[2];
                break;
            case BattleTextType.Buff:
                txContent.color = listColor[3];
                break;
            case BattleTextType.Special:
                txContent.color = listColor[4];
                break;
        }

        Vector3 pos3D = PublicTool.ConvertPosFromID(info.posID);
        pos3D = new Vector3(pos3D.x, 0.5f, pos3D.z);
        SetPosContent(info.content, pos3D);

        transform.localPosition = PublicTool.CalculateScreenUIPos(posSource,GameMgr.Instance.curMapCamera);

        seq = DOTween.Sequence();
        seq.Append(txContent.transform.DOLocalMoveY(200F, 2F));
        seq.Insert(0.6f, txContent.DOFade(0, 2f));

        Destroy(gameObject, 3f);
        isInit = true;
    }
}
