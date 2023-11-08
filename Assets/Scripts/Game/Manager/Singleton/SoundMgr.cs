using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    UnlockSkillNode,
    //Skill
    FireBall,
    Boom,
    LightingDestroy,
    FinalWork
}


public class SoundMgr : MonoSingleton<SoundMgr>
{
    public Transform tfBGM;
    public Transform tfSound;

    public Dictionary<SoundType, AudioSource> dicSoundAudio = new Dictionary<SoundType, AudioSource>();
    public Dictionary<SoundType, float> dicSoundTime = new Dictionary<SoundType, float>();


    [Header("Test")]
    public SoundType testSoundType;

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("PlaySound", PlaySoundEvent);
        EventCenter.Instance.AddEventListener("StopSound", StopSoundEvent);

    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("PlaySound", PlaySoundEvent);
        EventCenter.Instance.RemoveEventListener("StopSound", StopSoundEvent);

    }
    public IEnumerator IE_Init()
    {
        dicSoundAudio.Clear();

        foreach(var info in ExcelDataMgr.Instance.soundExcelData.dicSound)
        {
            GameObject objSound = new GameObject();
            //Instantiate(objSound, tfSound);
            objSound.transform.parent = tfSound;
            objSound.name = info.Key.ToString();
            AudioSource auSound = objSound.AddComponent<AudioSource>();
            auSound.clip = info.Value;
            auSound.loop = false;
            auSound.playOnAwake = false;
            dicSoundAudio.Add(info.Key, auSound);
        }

        dicSoundTime.Clear();
        foreach (var info in ExcelDataMgr.Instance.soundExcelData.dicSoundStartTime)
        {
            dicSoundTime.Add(info.Key, info.Value);
        }


        Debug.Log("Init Sound Manager");
        yield break;
    }

    public void PlaySoundEvent(object arg0)
    {
        SoundType soundType = (SoundType)arg0;

        if (dicSoundAudio.ContainsKey(soundType))
        {
            AudioSource targetSound = dicSoundAudio[soundType];

            float playTime = 0.6f;
            if (dicSoundTime.ContainsKey(soundType))
            {
                playTime = dicSoundTime[soundType];
            }
            targetSound.time = playTime;
            targetSound.Play();
        }
    }

    public void StopSoundEvent(object arg0)
    {
        SoundType soundType = (SoundType)arg0;

        if (dicSoundAudio.ContainsKey(soundType))
        {
            AudioSource targetSound = dicSoundAudio[soundType];

            targetSound.Stop();
        }
    }


    public void PlaySoundTime(SoundType soundType, float playtime)
    {
        if (dicSoundAudio.ContainsKey(soundType))
        {
            AudioSource targetSound = dicSoundAudio[soundType];

            float playTime = playtime;
            targetSound.time = playTime;
            targetSound.Play();
        }
    }
}
