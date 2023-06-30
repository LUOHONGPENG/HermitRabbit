using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public Canvas thisCanvas;

    public BattleOptionMgr battleOptionMgr;

    public void Init(Camera camera)
    {
        thisCanvas.worldCamera = camera;

        //Debug
        battleOptionMgr.Init();
    }
}
