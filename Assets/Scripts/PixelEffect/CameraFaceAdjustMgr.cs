using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFaceAdjustMgr : MonoBehaviour
{
    public Transform tfGroup;

    public bool isInit = false;

    public void Init()
    {
        isInit = true;
    }

    private void LateUpdate()
    {
        if (tfGroup != null && isInit)
        {
            RefreshGroupTransform();
        }
    }


    private void RefreshGroupTransform()
    {
        //Calculate the position between camera and unit to adjust the relative position of the character
/*        Vector2 cameraPos = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 direction = cameraPos - thisPos;
        direction.Normalize();
        tfGroup.localPosition = new Vector3(direction.x * 0.2f, 0, direction.y * 0.2f);*/

        tfGroup.LookAt(Camera.main.transform.forward + tfGroup.position);
    }
}
