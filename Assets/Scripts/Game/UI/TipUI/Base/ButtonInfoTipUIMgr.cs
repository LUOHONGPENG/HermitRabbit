using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfoTipUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Transform tfMouse;

    protected int recordID = -1;

    public virtual void ShowTipSetPos(Vector2 mousePos)
    {
        tfMouse.position = new Vector3(mousePos.x, mousePos.y, tfMouse.transform.position.z);
        objPopup.SetActive(true);
    }

    public virtual void HideTip()

    {
        objPopup.SetActive(false);
    }
}
