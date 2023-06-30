using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public Canvas thisCanvas;

    public void Init(Camera camera)
    {
        thisCanvas.worldCamera = camera;
    }
}
