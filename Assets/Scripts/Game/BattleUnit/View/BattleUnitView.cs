using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleUnitView : MonoBehaviour
{
    public SpriteRenderer srUnit;

    public Collider colUnit;

    public BattleUnitData unitData;




    private void LateUpdate()
    {
        Vector2 cameraPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 direction = cameraPos - thisPos;
        direction.Normalize();
        srUnit.transform.localPosition = new Vector3(direction.x * 0.35f, 0, direction.y * 0.35f);
        srUnit.transform.LookAt(Camera.main.transform.forward + srUnit.transform.position);
    }

    public void MoveToPos(Vector2Int posID,bool battleMove)
    {
        Vector3 tilePos = PublicTool.ConvertPosFromID(posID);
        if (battleMove)
        {
            this.transform.DOLocalMove(new Vector3(tilePos.x, 0.35f, tilePos.z), 0.5f);
        }
        else
        {
            this.transform.localPosition = new Vector3(tilePos.x, 0.35f, tilePos.z);
        }
    }


    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }


    public bool isExecutingBattleText = false;

    public void RequestBattleText()
    {
        Queue<BattleTextInfo> queueInfo = unitData.GetQueueBattleText();
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

            switch (info.type)
            {
                case BattleTextType.Damage:
                    EventCenter.Instance.EventTrigger("EffectUIText", new EffectUITextInfo(EffectUITextType.Damage, unitData.posID, -1, info.info));
                    break;
                case BattleTextType.Heal:
                    EventCenter.Instance.EventTrigger("EffectUIText", new EffectUITextInfo(EffectUITextType.Damage, unitData.posID, -1, info.info));
                    break;
            }

            yield return new WaitForSeconds(0.2f);
        }
        isExecutingBattleText = false;
    }
}
