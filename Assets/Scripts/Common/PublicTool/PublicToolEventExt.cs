using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    public static void EventCameraGo(Vector2Int posID)
    {
        Vector3 targetPos = ConvertPosFromID(posID);
        EventCenter.Instance.EventTrigger("CameraGoTo", targetPos);
    }

    public static void EventChangeInteract(InteractState state, int data_0=-1)
    {
        if (data_0 >= 0)
        {
            EventCenter.Instance.EventTrigger("ChangeInteract", new InteractInfo(state, data_0));
        }
        else
        {
            EventCenter.Instance.EventTrigger("ChangeInteract", new InteractInfo(state));
        }
    }

    public static void EventRefreshOccupancy()
    {
        EventCenter.Instance.EventTrigger("RefreshOccupancy", null);
    }
}
