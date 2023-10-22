using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormalGameMgr : SceneGameMgr
{
    public void Start()
    {
        StartCoroutine(IE_Init());
    }
}
