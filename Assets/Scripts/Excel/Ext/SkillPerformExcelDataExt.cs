using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillPerformExcelData
{
    public Dictionary<int, List<SkillPerformInfo>> dicSkillPerformInfo = new Dictionary<int, List<SkillPerformInfo>>();
    public Dictionary<int, float> dicSkillPerformTotalTime = new Dictionary<int, float>();

    public void Init()
    {
        dicSkillPerformInfo.Clear();
        dicSkillPerformTotalTime.Clear();
        for (int i = 0; i < items.Length; i++)
        {
            SkillPerformExcelItem performItem = items[i];
            int keyID = performItem.id;
            if (!dicSkillPerformTotalTime.ContainsKey(keyID))
            {
                dicSkillPerformTotalTime.Add(keyID, performItem.totalTime);
            }

            //SubjectAni
            if (!dicSkillPerformInfo.ContainsKey(keyID))
            {
                List<SkillPerformInfo> listInfo = new List<SkillPerformInfo>();

                for (int j = 0; j < performItem.listUnitAniState.Count; j++)
                {
                    string stateStr = performItem.listUnitAniState[j];
                    if(stateStr == "0")
                    {
                        continue;
                    }
                    UnitAniState aniState = (UnitAniState)System.Enum.Parse(typeof(UnitAniState), stateStr);
                    
                    if(j < performItem.listUnitAniTime.Count)
                    {
                        float startTime = performItem.listUnitAniTime[j] * 0.1f;
                        SkillPerformInfo newInfo = new SkillPerformInfo(SkillPerformInfoType.SubjectAni, startTime);
                        newInfo.unitAniState = aniState;
                        listInfo.Add(newInfo);
                    }
                }

                //EffectView
                for (int k = 0; k < performItem.listEffectViewType.Count; k++)
                {
                    string viewStr = performItem.listEffectViewType[k];
                    if (viewStr == "0")
                    {
                        continue;
                    }
                    EffectViewType viewType = (EffectViewType)System.Enum.Parse(typeof(EffectViewType), viewStr);
                    
                    if (k < performItem.listEffectViewTime.Count && k < performItem.listEffectPosType.Count)
                    {
                        EffectViewPosType posType = (EffectViewPosType)System.Enum.Parse(typeof(EffectViewPosType), performItem.listEffectPosType[k]);

                        float startTime = performItem.listEffectViewTime[k] * 0.1f;
                        SkillPerformInfo newInfo = new SkillPerformInfo(SkillPerformInfoType.EffectView, startTime);
                        newInfo.effectViewType = viewType;
                        newInfo.effectPosType = posType;
                        listInfo.Add(newInfo);
                    }
                }

                //PlaySound
                for (int l = 0; l < performItem.listSoundType.Count; l++)
                {
                    string soundStr = performItem.listSoundType[l];
                    if (soundStr == "0")
                    {
                        continue;
                    }
                    SoundType soundType = (SoundType)System.Enum.Parse(typeof(SoundType), soundStr);

                    if (l < performItem.listSoundTime.Count)
                    {
                        float startTime = performItem.listSoundTime[l] * 0.1f;
                        SkillPerformInfo newInfo = new SkillPerformInfo(SkillPerformInfoType.PlaySound, startTime);
                        newInfo.soundType = soundType;
                        listInfo.Add(newInfo);
                    }
                }

                //ChangeCamera
                for(int m = 0; m < performItem.listCameraPosType.Count; m++)
                {
                    string cameraStr = performItem.listCameraPosType[m];
                    if(cameraStr == "0")
                    {
                        continue;
                    }
                    CameraPosType cameraPosType = (CameraPosType)System.Enum.Parse(typeof(CameraPosType), cameraStr);

                    if(m < performItem.listCameraTime.Count)
                    {
                        float startTime = performItem.listCameraTime[m] * 0.1f;
                        SkillPerformInfo newInfo = new SkillPerformInfo(SkillPerformInfoType.ChangeCamera, startTime);
                        newInfo.cameraPosType = cameraPosType;
                        listInfo.Add(newInfo);
                    }
                }

                dicSkillPerformInfo.Add(keyID, listInfo);
            }
        }
    }
}

public partial class SkillPerformExcelItem
{

}

public class SkillPerformInfo
{
    public SkillPerformInfoType infoType;
    public float startTime;
    public UnitAniState unitAniState;
    public EffectViewType effectViewType;
    public EffectViewPosType effectPosType;
    public SoundType soundType;
    public CameraPosType cameraPosType;

    public SkillPerformInfo(SkillPerformInfoType infoType,float startTime)
    {
        this.infoType = infoType;
        this.startTime = startTime;
    }

}