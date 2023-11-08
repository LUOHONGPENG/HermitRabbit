using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundExcelData
{
    public Dictionary<SoundType, AudioClip> dicSound = new Dictionary<SoundType, AudioClip>();
    public Dictionary<SoundType, float> dicSoundStartTime = new Dictionary<SoundType, float>();


    public void Init()
    {
        dicSound.Clear();
        dicSoundStartTime.Clear();
        for (int i = 0; i < items.Length; i++)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sound/" + items[i].soundUrl);
            if (clip != null)
            {
                dicSound.Add(items[i].name, clip);
            }

            dicSoundStartTime.Add(items[i].name, items[i].soundStartTime);
        }
    }
}
