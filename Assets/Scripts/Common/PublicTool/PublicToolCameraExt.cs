using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static void EventNormalCameraGoPosID(Vector2Int posID)
    {
        Vector3 targetPos = ConvertPosFromID(posID);
        EventCenter.Instance.EventTrigger("NormalCameraGoTo", targetPos);
    }

    public static void EventChangeCamera(ChangeCameraInfo info)
    {
        EventCenter.Instance.EventTrigger("ChangeCamera", info);
    }

}
