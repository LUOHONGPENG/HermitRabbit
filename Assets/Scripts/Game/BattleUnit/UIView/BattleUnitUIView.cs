using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnitUIView : MonoBehaviour
{

    public Canvas canvasUIView;
    public Transform tfUIView;

    public Image imgHPBar;
    public Image imgHPFill;

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
}
