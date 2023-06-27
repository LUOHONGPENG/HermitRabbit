using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameMgr : MonoBehaviour
{
    public MenuUIMgr menuUIMgr;

    private void Start()
    {
        menuUIMgr.Init();
    }
}
