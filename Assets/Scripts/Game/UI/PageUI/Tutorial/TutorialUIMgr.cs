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

public struct StartTutorialInfo
{
    public TutorialMode mode;
    public TutorialGroup group;

    public StartTutorialInfo(TutorialMode mode,TutorialGroup group)
    {
        this.mode = mode;
        this.group = group;
    }
}

public class TutorialUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    [Header("Basic")]
    public TextMeshProUGUI codeTitle;
    public TextMeshProUGUI codeContent;
    public Image imgDesc;

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
    private int curTutorialID = -1;

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
            itemTag.Init(i, delegate ()
            {
                SelectTutorialGroup(i);
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

        StartTutorial(info.mode, info.group);
    }

    public void StartTutorial(TutorialMode tutorialMode,TutorialGroup group)
    {
        if (tutorialMode == TutorialMode.First)
        {
            objFirst.SetActive(true);
            objCommon.SetActive(false);

            SelectTutorialGroup(group);
        }
        else if(tutorialMode == TutorialMode.Common)
        {
            objFirst.SetActive(false);
            objCommon.SetActive(true);

            SelectTutorialGroup(TutorialGroup.Battle);
            RefreshPageNum();
        }
    }

    public void SelectTutorialGroup(TutorialGroup group)
    {
        listCurTutorial = PublicTool.GetTutorialGroup(group);
        if (listCurTutorial != null)
        {
            curTutorialID = 0;
            ReadTutorialData();
            objPopup.SetActive(true);
        }
    }

    public void TutorialNextStep()
    {
        curTutorialID++;
        if(curTutorialID < listCurTutorial.Count && curTutorialID >= 0)
        {
            ReadTutorialData();

            if (curTutorialID == listCurTutorial.Count - 1)
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
    }

    public void ReadTutorialData()
    {
        if(curTutorialID < listCurTutorial.Count && curTutorialID >= 0)
        {
            TutorialExcelItem tutorialItem = listCurTutorial[curTutorialID];

            //codeTitle.text = tutorialItem.
            codeTitle.text = tutorialItem.strTitle;
            codeContent.text = tutorialItem.strContent;

            if (tutorialItem.imgUrl.Length > 0)
            {

            }
            else
            {
                imgDesc.sprite = null;
            }

        }
    }

    public void HidePopup()
    {
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
}
