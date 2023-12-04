using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightMgr : MonoBehaviour
{
    public Light spotLight;
    public Light characterLight;


    public Light pointLight;
    public List<Light> listDirectionLight;

    public Volume volume;
    public VolumeProfile profileDay;
    public VolumeProfile profileNight;


    public void Init()
    {
        spotLight.enabled = false;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            characterLight.intensity = 50;
        }
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

    public void SetDay()
    {
        pointLight.enabled = false;
        foreach(var item in listDirectionLight)
        {
            item.intensity = 0.27f;
        }
        volume.profile = profileDay;
    }

    public void SetNight()
    {
        pointLight.enabled = true;
        foreach (var item in listDirectionLight)
        {
            item.intensity = 0.18f;
        }
        volume.profile = profileNight;

    }
}
