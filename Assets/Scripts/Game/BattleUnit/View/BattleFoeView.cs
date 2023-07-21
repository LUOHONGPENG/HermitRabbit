using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFoeView : BattleUnitView
{
    public BattleFoeData foeData;
    

    public void Init(BattleFoeData foeData)
    {
        this.foeData = foeData;
        srUnit.sprite = Resources.Load(foeData.GetItem().pixelUrl, typeof(Sprite)) as Sprite;

        List<Vector2Int> listPos = PublicTool.GetGameData().listTempEmptyPos;
        int ran = Random.Range(0, listPos.Count);
        foeData.posID = listPos[ran];
        MoveToPos(foeData.posID);
    }

    public bool isExecutingBattleText = false;

    public override void RequestBattleText()
    {
        Queue<BattleTextInfo> queueInfo = foeData.GetQueueBattleText();
        if (queueInfo != null)
        {
            StartCoroutine(IE_ExecuteBattleText(queueInfo));
        }
    }

    public IEnumerator IE_ExecuteBattleText(Queue<BattleTextInfo> queueInfo)
    {
        isExecutingBattleText = true;
        while (queueInfo.Count > 0)
        {
            BattleTextInfo info = queueInfo.Dequeue();

            EventCenter.Instance.EventTrigger("EffectUIText", new EffectUITextInfo(EffectUITextType.Damage, foeData.posID, -1,info.info));

            yield return new WaitForSeconds(0.2f);
        }
        isExecutingBattleText = false;
    }
}
