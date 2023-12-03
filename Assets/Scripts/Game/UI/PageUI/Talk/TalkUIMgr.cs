using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TalkUIMgr : MonoBehaviour
{
    public GameObject objPopup;
    public Button btnContinue;
    public Button btnSkip;
    public Image imgCharacter;
    public TextMeshProUGUI codeTalk;

    public List<Sprite> listSpCharacter = new List<Sprite>();

    private List<TalkExcelItem> listTalk;
    private int curIndex = -1;
    private GameData gameData;
    private TalkGroup curTalkGroup = TalkGroup.None;

    public void Init()
    {
        gameData = PublicTool.GetGameData();

        btnContinue.onClick.RemoveAllListeners();
        btnContinue.onClick.AddListener(delegate ()
        {
            Continue();
        });

        btnSkip.onClick.RemoveAllListeners();
        btnSkip.onClick.AddListener(delegate ()
        {
            CloseTalk();
        });
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("TalkStart", TalkStartEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("TalkStart", TalkStartEvent);
    }

    private void TalkStartEvent(object arg0)
    {
        TalkGroup group = (TalkGroup)arg0;
        StartTalk(group);
    }

    public void StartTalk(TalkGroup talkGroup)
    {
        curIndex = -1;

        if (ExcelDataMgr.Instance.talkExcelData.dicTalk.ContainsKey(talkGroup))
        {
            listTalk = ExcelDataMgr.Instance.talkExcelData.dicTalk[talkGroup];
            curTalkGroup = talkGroup;
            Continue();
            objPopup.SetActive(true);
        }
    }

    private void ReadTalk(TalkExcelItem item)
    {
        switch (item.step)
        {
            case TalkStep.Text:
                codeTalk.text = item.content;
                switch (item.talkSubjectID)
                {
                    case 1001:
                        imgCharacter.sprite = listSpCharacter[0];
                        break;
                    case 1002:
                        imgCharacter.sprite = listSpCharacter[1];
                        break;
                }
                break;
            case TalkStep.BattleStart:
                PublicTool.SetNight();
                EventCenter.Instance.EventTrigger("BattleStart", null);
                Continue();
                //May cause bug
                break;
            case TalkStep.End:
                CloseTalk();
                break;
        }

    }


    private void Continue()
    {
        curIndex++;
        if (curIndex < listTalk.Count)
        {
            ReadTalk(listTalk[curIndex]);
        }
    }

    private void CloseTalk()
    {
        CheckWhetherOpenTutorial();
        objPopup.SetActive(false);
        curTalkGroup = TalkGroup.None;
    }

    private void CheckWhetherOpenTutorial()
    {
        if (curTalkGroup == TalkGroup.Day1)
        {
            PublicTool.StartConditionalTutorial(TutorialGroup.Battle,0,3);
        }
        else if(curTalkGroup == TalkGroup.Day2)
        {
            PublicTool.StartConditionalTutorial(TutorialGroup.Skill,0,1);
        }
    }

}
