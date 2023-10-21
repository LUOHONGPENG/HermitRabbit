using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUIMgr : MonoBehaviour
{
    [Header("Basic")]
    public GameObject objPopup;
    public GameObject objMapClip;
    public GameObject objPlant;
    public Button btnContinue;

    [Header("EXP")]
    public Transform tfExpFrame;
    public GameObject pfExpFrame;
    private List<VictoryExpUIItem> listExpFrame = new List<VictoryExpUIItem>();

    private VictoryPhase victoryPhase;
    private Dictionary<int, int> dicBeforeExp = new Dictionary<int, int>();
    private Dictionary<int, int> dicGrowExp = new Dictionary<int, int>();

    public enum VictoryPhase
    {
        EXP,
        MapClip,
        Plant,
        End
    }

    #region Basic
    public void Init()
    {
        btnContinue.onClick.RemoveAllListeners();
        btnContinue.onClick.AddListener(delegate ()
        {
            //Need to modify
            HidePopup();
        });
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("NormalVictoryStart", ShowVictoryEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("NormalVictoryStart", ShowVictoryEvent);
    }

    private void ShowVictoryEvent(object arg0)
    {
        victoryPhase = VictoryPhase.EXP;

        //Load Exp Info
        dicBeforeExp.Clear();
        dicGrowExp.Clear();
        List<Vector2Int> listGrowExp = (List<Vector2Int>)arg0;
        foreach(Vector2Int expInfo in listGrowExp)
        {
            if (!dicGrowExp.ContainsKey(expInfo.x))
            {
                dicGrowExp.Add(expInfo.x, expInfo.y);
            }
            if (!dicBeforeExp.ContainsKey(expInfo.x))
            {
                int BeforeExp = PublicTool.GetGameData().GetBattleCharacterData(expInfo.x).EXP;
                dicBeforeExp.Add(expInfo.x, BeforeExp);
            }
        }
        //ShowExp
        ShowExp();
        objPopup.SetActive(true);
        StartCoroutine(IE_ExpGrow());
    }

    public void HidePopup()
    {
        objPopup.SetActive(false);
    }
    #endregion

    #region EXP
    public void ShowExp()
    {
        listExpFrame.Clear();

        foreach (var info in dicBeforeExp)
        {
            GameObject objExpFrame = GameObject.Instantiate(pfExpFrame, tfExpFrame);
            VictoryExpUIItem itemExpFrame = objExpFrame.GetComponent<VictoryExpUIItem>();
            itemExpFrame.Init(info.Key, info.Value, dicGrowExp[info.Key]);
            listExpFrame.Add(itemExpFrame);
        }

    }

    public IEnumerator IE_ExpGrow()
    {
        for(int i = 0; i < listExpFrame.Count; i++)
        {
            listExpFrame[i].GrowExp();
        }

        yield return new WaitUntil(()=> CheckAllExpDone());

        victoryPhase = VictoryPhase.MapClip;
    }

    public bool CheckAllExpDone()
    {
        bool isDone = true;
        for (int i = 0; i < listExpFrame.Count; i++)
        {
            if (!listExpFrame[i].isGrowExp)
            {
                isDone = false;
            }
        }
        return isDone;
    }
    #endregion

    #region

    #endregion
}
