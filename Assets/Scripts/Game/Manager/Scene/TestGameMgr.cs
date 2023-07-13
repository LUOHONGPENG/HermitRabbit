using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestGameMgr : SceneGameMgr
{
    public void Start()
    {
        GameGlobal.targetScene = SceneName.Test;
        StartCoroutine(IE_Init());
    }

}
