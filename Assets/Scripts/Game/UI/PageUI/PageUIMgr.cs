using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageUIMgr : MonoBehaviour
{
    public GameOverUIMgr gameOverUIMgr;
    public StatusUIMgr statusUIMgr;
    public VictoryUIMgr victoryUIMgr;
    public TalkUIMgr talkUIMgr;
    public TutorialUIMgr tutorialUIMgr;
    public SettingUIMgr settingUIMgr;

    private bool isInit = false;

    public void Init()
    {
        gameOverUIMgr.Init();
        statusUIMgr.Init();
        victoryUIMgr.Init();
        talkUIMgr.Init();
        tutorialUIMgr.Init();
        settingUIMgr.Init();

        isInit = true;
    }

    public bool isPageOn
    {
        get
        {
            if (gameOverUIMgr.objPopup.activeSelf)
            {
                return true;
            }
            else if (statusUIMgr.objPopup.activeSelf)
            {
                return true;
            }
            else if (victoryUIMgr.objPopup.activeSelf)
            {
                return true;
            }
            else if (talkUIMgr.objPopup.activeSelf)
            {
                return true;
            }
            else if (settingUIMgr.objPopup.activeSelf)
            {
                return true;
            }
            return false;
        }
    }
}
