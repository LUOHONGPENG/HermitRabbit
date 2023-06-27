using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoSingleton<SoundMgr>
{
    public IEnumerator IE_Init()
    {
        Debug.Log("Init Sound Manager");
        yield break;
    }

    public void PlaySound()
    {
        Debug.Log("PlaySound");
        Debug.Log(this.gameObject.transform.childCount);
    }
}
