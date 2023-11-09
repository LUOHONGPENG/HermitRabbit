using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnitUIView : MonoBehaviour
{
    [Header("Basic")]
    public Canvas canvasUIView;
    public Transform tfUIView;
    public CanvasGroup canvasGroup;
    [Header("HP")]
    public Image imgHPBar;
    public Image imgHPFill;
    [Header("Buff")]
    public Transform tfBuff;
    public GameObject pfBuff;

    private Camera mapCamera;
    private BattleUnitView parent;


    public void Init(BattleUnitView parent)
    {
        this.parent = parent;
        canvasUIView.worldCamera = GameMgr.Instance.curUICamera;
        mapCamera = GameMgr.Instance.curMapCamera;
        //transform.localPosition = PublicTool.CalculateScreenUIPos(posSource, GameMgr.Instance.curMapCamera);

    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowUnitUI", ShowUnitUIEvent);
        EventCenter.Instance.AddEventListener("HideUnitUI", HideUnitUIEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("ShowUnitUI", ShowUnitUIEvent);
        EventCenter.Instance.RemoveEventListener("HideUnitUI", HideUnitUIEvent);
    }

    public void RefreshUIPos()
    {
        tfUIView.localPosition = PublicTool.CalculateScreenUIPos(parent.transform.position, GameMgr.Instance.curMapCamera);
    }

    public void RefreshHPBar(float HPrate)
    {
        imgHPFill.fillAmount = HPrate;
    }

    public void RefreshBuffInfo(List<Buff> listBuff)
    {
        PublicTool.ClearChildItem(tfBuff);
        for(int i = 0; i < listBuff.Count; i++)
        {
            GameObject objBuff = GameObject.Instantiate(pfBuff, tfBuff);
            BuffTinyUIItem itemBuff = objBuff.GetComponent<BuffTinyUIItem>();
            itemBuff.Init(listBuff[i]);
        }
    }

    private void HideUnitUIEvent(object arg0)
    {
        canvasGroup.DOFade(0, 0.2f);
    }

    private void ShowUnitUIEvent(object arg0)
    {
        canvasGroup.DOFade(1, 0.2f);
    }
}
