using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum TutorialMode
{
    First,
    Common
}

public class StartTutorialInfo
{
    public TutorialMode mode;
    public TutorialGroup group;
    public int startIndex = -1;
    public int endIndex = -1;

    public StartTutorialInfo(TutorialMode mode,TutorialGroup group,int startIndex = -1, int endIndex = -1)
    {
        this.mode = mode;
        this.group = group;

        this.startIndex = startIndex;
        this.endIndex = endIndex;
    }
}

public class TutorialUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    [Header("Basic")]
    public TextMeshProUGUI codeTitle;
    public TextMeshProUGUI codeContent;
    public Image imgDesc;
    public Animator aniGif;

    [Header("First")]
    public GameObject objFirst;
    public Button btnContinue;
    public Button btnFinish;

    [Header("Common")]
    public GameObject objCommon;
    public Button btnClose;
    public Transform tfTag;
    public GameObject pfTag;
    private List<TutorialTagUIItem> listTag = new List<TutorialTagUIItem>();

    [Header("Page")]
    public Button btnLeft;
    public Button btnRight;
    public TextMeshProUGUI codePage;

    private List<TutorialExcelItem> listCurTutorial = new List<TutorialExcelItem>();
    private TutorialGroup curGroup = TutorialGroup.None;
    private int curTutorialID = -1;
    private int startIndex = -1;
    private int endIndex = -1;

    public void Init()
    {
        btnContinue.onClick.RemoveAllListeners();
        btnContinue.onClick.AddListener(delegate ()
        {
            TutorialNextStep();
        });

        btnFinish.onClick.RemoveAllListeners();
        btnFinish.onClick.AddListener(delegate ()
        {
            HidePopup();
        });

        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            HidePopup();
        });

        btnLeft.onClick.RemoveAllListeners();
        btnLeft.onClick.AddListener(delegate () 
        {
            LeftButtonEvent();
        });

        btnRight.onClick.RemoveAllListeners();
        btnRight.onClick.AddListener(delegate ()
        {
            RightButtonEvent();
        });

        listTag.Clear();
        PublicTool.ClearChildItem(tfTag);
        for(TutorialGroup i = TutorialGroup.None+1; i < TutorialGroup.End; i++)
        {
            GameObject objTag = GameObject.Instantiate(pfTag, tfTag);
            TutorialTagUIItem itemTag = objTag.GetComponent<TutorialTagUIItem>();
            TutorialGroup tempGroup = i;
            itemTag.Init(tempGroup, delegate ()
            {
                SelectTutorialGroup(tempGroup);
                RefreshPageNum();
            });
            listTag.Add(itemTag);
        }
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("StartTutorial", StartTutorialEvent);
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("StartTutorial", StartTutorialEvent);
    }

    private void StartTutorialEvent(object arg0)
    {
        StartTutorialInfo info = (StartTutorialInfo)arg0;

        StartTutorial(info.mode, info.group,info.startIndex,info.endIndex);
    }

    public void StartTutorial(TutorialMode tutorialMode,TutorialGroup group,int startID,int endID)
    {
        if (tutorialMode == TutorialMode.First)
        {
            objFirst.SetActive(true);
            objCommon.SetActive(false);

            startIndex = startID;
            endIndex = endID;

            SelectTutorialGroup(group, startIndex);
            RefreshButton();
        }
        else if(tutorialMode == TutorialMode.Common)
        {
            objFirst.SetActive(false);
            objCommon.SetActive(true);

            SelectTutorialGroup(TutorialGroup.Battle);
            RefreshPageNum();
        }
    }

    public void SelectTutorialGroup(TutorialGroup group,int startID = -1)
    {
        List<TutorialExcelItem> listTemp = PublicTool.GetTutorialGroup(group);
        if (listTemp != null && listTemp.Count>0)
        {
            listCurTutorial = listTemp;
            curGroup = group;
            if (startID < 0)
            {
                curTutorialID = 0;
            }
            else
            {
                curTutorialID = startID;
            }
            objPopup.SetActive(true);
            ReadTutorialData();
            RefreshSelectGroup();

        }
    }

    public void TutorialNextStep()
    {
        curTutorialID++;
        if(curTutorialID < listCurTutorial.Count && curTutorialID >= 0)
        {
            ReadTutorialData();

            RefreshButton();
        }
    }

    public void RefreshButton()
    {

        if (endIndex < 0 && curTutorialID == listCurTutorial.Count - 1)
        {
            btnContinue.gameObject.SetActive(false);
            btnFinish.gameObject.SetActive(true);
        }
        else if (endIndex >= 0 && curTutorialID == endIndex)
        {
            btnContinue.gameObject.SetActive(false);
            btnFinish.gameObject.SetActive(true);
        }
        else
        {
            btnFinish.gameObject.SetActive(false);
            btnContinue.gameObject.SetActive(true);
        }
    }

    public void ReadTutorialData()
    {
        if(curTutorialID < listCurTutorial.Count && curTutorialID >= 0)
        {
            TutorialExcelItem tutorialItem = listCurTutorial[curTutorialID];

            //codeTitle.text = tutorialItem.
            codeTitle.text = tutorialItem.GetTitle();
            codeContent.text = tutorialItem.GetContent();


            if (tutorialItem.gifUrl.Length > 0)
            {
                aniGif.Play(tutorialItem.gifUrl);
            }
            else
            {
                aniGif.Play("None");
            }

        }
    }

    public void HidePopup()
    {
        startIndex = -1;
        endIndex = -1;
        objPopup.SetActive(false);
    }

    public void LeftButtonEvent()
    {
        if (curTutorialID >= 1)
        {
            curTutorialID--;
            if (curTutorialID < listCurTutorial.Count && curTutorialID >= 0)
            {
                ReadTutorialData();
                RefreshPageNum();
            }
        }
    }

    public void RightButtonEvent()
    {
        if (curTutorialID < listCurTutorial.Count-1)
        {
            curTutorialID++;
            if (curTutorialID < listCurTutorial.Count && curTutorialID >= 0)
            {
                ReadTutorialData();
                RefreshPageNum();
            }
        }
    }

    private void RefreshPageNum()
    {
        if (listCurTutorial != null)
        {
            codePage.text = string.Format("{0}/{1}", curTutorialID + 1, listCurTutorial.Count);
        }
    }

    private void RefreshSelectGroup()
    {
        for(int i = 0; i < listTag.Count; i++)
        {
            TutorialTagUIItem tag = listTag[i];

            tag.SetSelected(curGroup);
        }
    }
}
