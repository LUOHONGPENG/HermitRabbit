using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    UnlockSkillNode,
    Victory,
    RabbitAttack,
    Strike,
    //Skill
    FireBall,
    Boom,
    LightingDestroy,
    FinalWork,
    //Foe
    Bite
}

public enum MusicType
{
    Menu,
    Peace,
    Battle
}

public class SoundMgr : MonoSingleton<SoundMgr>
{
    public Transform tfBGM;
    public Transform tfSound;

    public Dictionary<SoundType, AudioSource> dicSoundAudio = new Dictionary<SoundType, AudioSource>();
    public Dictionary<SoundType, float> dicSoundTime = new Dictionary<SoundType, float>();

    public AudioSource musicMenu;
    public AudioSource musicPeace;
    public AudioSource musicBattle;

    public Dictionary<MusicType, AudioSource> dicMusic = new Dictionary<MusicType, AudioSource>();


    [Header("Test")]
    public SoundType testSoundType;

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("PlaySound", PlaySoundEvent);
        EventCenter.Instance.AddEventListener("StopSound", StopSoundEvent);
        EventCenter.Instance.AddEventListener("PlayMusic", PlayMusicEvent);
        EventCenter.Instance.AddEventListener("StopMusic", StopMusicEvent);

    }


    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("PlaySound", PlaySoundEvent);
        EventCenter.Instance.RemoveEventListener("StopSound", StopSoundEvent);
        EventCenter.Instance.RemoveEventListener("PlayMusic", PlayMusicEvent);
        EventCenter.Instance.RemoveEventListener("StopMusic", StopMusicEvent);

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

        dicMusic.Clear();
        dicMusic.Add(MusicType.Menu, musicMenu);
        dicMusic.Add(MusicType.Peace, musicPeace);
        dicMusic.Add(MusicType.Battle, musicBattle);

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

    private void PlayMusicEvent(object arg0)
    {
        MusicType musicType = (MusicType)arg0;
        PlayMusic(musicType);
    }


    public void PlayMusic(MusicType musicType)
    {
        foreach(var info in dicMusic)
        {
            if(info.Key != musicType)
            {
                info.Value.Stop();
            }
            else
            {
                info.Value.Play();
            }
        }
    }

    private void StopMusicEvent(object arg0)
    {
        foreach (var info in dicMusic)
        {
            info.Value.Stop();
        }
    }

}
