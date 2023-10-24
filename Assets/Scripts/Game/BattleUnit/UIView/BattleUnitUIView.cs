using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnitUIView : MonoBehaviour
{
    [Header("Basic")]
    public Canvas canvasUIView;
    public Transform tfUIView;
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
        Debug.Log("InitCamera");
        mapCamera = GameMgr.Instance.curMapCamera;
        //transform.localPosition = PublicTool.CalculateScreenUIPos(posSource, GameMgr.Instance.curMapCamera);

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
}
