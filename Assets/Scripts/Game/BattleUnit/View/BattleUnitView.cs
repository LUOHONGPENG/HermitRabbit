using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public partial class BattleUnitView : MonoBehaviour
{
    public Transform tfSr;
    public SpriteRenderer srUnit;
    public Collider colUnit;

    public BattleUnitData unitData;
    public BattleUnitUIView uiView;

    protected bool isInit = false;

    public void CommonInit()
    {
        if (uiView != null)
        {
            uiView.Init(this);
        }
    }

    #region Basic Function
    public void MoveToPos()
    {
        Vector2Int targetPosID = unitData.posID;
        Vector3 tilePos = PublicTool.ConvertPosFromID(targetPosID);
        this.transform.localPosition = new Vector3(tilePos.x, GameGlobal.commonUnitPosY, tilePos.z);
    }

    public IEnumerator IE_MovePath(List<Vector2Int> path)
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 tilePos = PublicTool.ConvertPosFromID(path[i]);
            this.transform.DOLocalMove(new Vector3(tilePos.x, GameGlobal.commonUnitPosY, tilePos.z), 0.2f);
            PublicTool.EventCameraGoPosID(path[i]);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
    #endregion

    #region Update Component Pos
    private void LateUpdate()
    {
        RefreshUnitSpriteDirection();
        RefreshUnitUIPos();
    }

    private void RefreshUnitSpriteDirection()
    {
        //Calculate the position between camera and unit to adjust the relative position of the character
        Vector2 cameraPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 direction = cameraPos - thisPos;
        direction.Normalize();
        tfSr.localPosition = new Vector3(direction.x * 0.2f, 0, direction.y * 0.2f);

        //Adjust the Face of the unit
        tfSr.LookAt(Camera.main.transform.forward + tfSr.position);
    }

    private void RefreshUnitUIPos()
    {
        if (uiView != null)
        {
            uiView.RefreshUIPos();
        }
    }
    #endregion

    public void RefreshUnitUIInfo()
    {
        if (uiView != null)
        {
            uiView.RefreshHPBar(unitData.HPrate);
            uiView.RefreshBuffInfo(unitData.listBuff);
        }
        //RefreshHPBar
    }


    #region TextEffect
    public bool isExecutingBattleText = false;

    public void RequestBattleText()
    {
        Queue<EffectBattleTextInfo> queueInfo = unitData.GetQueueBattleText();
        if (queueInfo != null)
        {
            StartCoroutine(IE_ExecuteBattleText(queueInfo));
        }
    }

    public IEnumerator IE_ExecuteBattleText(Queue<EffectBattleTextInfo> queueInfo)
    {
        isExecutingBattleText = true;
        while (queueInfo.Count > 0)
        {
            EffectBattleTextInfo info = queueInfo.Dequeue();
            EventCenter.Instance.EventTrigger("EffectBattleText", info);
            if(info.type == BattleTextType.Damage)
            {
                StartCoroutine(IE_DamageSpriteEffect());
            }
            if (queueInfo.Count > 0)
            {
                
                yield return new WaitForSeconds(0.2f);
            }
        }
        isExecutingBattleText = false;
    }
    #endregion

    #region DamageEffect

    public IEnumerator IE_DamageSpriteEffect()
    {
        srUnit.transform.DOLocalMoveX(-0.1f, 0.2f);
        srUnit.DOColor(new Color(1f, 0, 0), 0.2f);
        yield return new WaitForSeconds(0.2f);
        srUnit.transform.DOLocalMoveX(0f, 0.2f);
        srUnit.DOColor(new Color(1F, 1F, 1F), 0.2f);
    }



    #endregion


}
