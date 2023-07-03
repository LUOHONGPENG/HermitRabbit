using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleOptionMgr : MonoBehaviour
{
    public Transform tfPage;
    public GameObject pfPage;

    public Button btnClose;

    public Stack<BattleOptionPage> stackPage = new Stack<BattleOptionPage>();

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(ClosePage);
        btnClose.gameObject.SetActive(false);

        PublicTool.ClearChildItem(tfPage);
    }

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowBattleOption", ShowBattlePageEvent);
    }

    public void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("ShowBattleOption", ShowBattlePageEvent);
    }

    public void ShowBattlePageEvent(object arg0)
    {
        if (stackPage.Count < 1)
        {
            GameObject objPage = GameObject.Instantiate(pfPage,tfPage);
            BattleOptionPage itemPage = objPage.GetComponent<BattleOptionPage>();
            itemPage.Init(this,stackPage.Count + 1);

            stackPage.Push(itemPage);
            btnClose.gameObject.SetActive(true);
        }
    }

    public void ClosePage()
    {
        if (stackPage.Count >= 1)
        {
            BattleOptionPage closePage = stackPage.Pop();
            Destroy(closePage.gameObject);

            if (stackPage.Count < 1)
            {
                btnClose.gameObject.SetActive(false);
            }
        }
    }
}
