using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMgr : MonoBehaviour
{
    public Light spotLight;

    public void Init()
    {
        spotLight.enabled = false;
    }


    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("SpotLightShowEvent", SetSpotLightPosEvent);
        EventCenter.Instance.AddEventListener("SpotLightHideEvent", HideSpotLight);

    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("SpotLightShowEvent", SetSpotLightPosEvent);
        EventCenter.Instance.RemoveEventListener("SpotLightHideEvent", HideSpotLight);
    }


    public void SetSpotLightPosEvent(object arg0)
    {
        spotLight.enabled = true;
        Vector3 pos = (Vector3)arg0;
        spotLight.transform.DOMove(new Vector3(pos.x, spotLight.transform.position.y, pos.z), 0.5f);
    }

    public void HideSpotLight(object arg0)
    {
        spotLight.enabled = false;
    }
}
