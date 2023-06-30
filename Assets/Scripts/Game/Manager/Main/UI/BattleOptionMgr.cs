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


        ShowPage();
    }

    public void ShowPage()
    {
        GameObject objPage = GameObject.Instantiate(pfPage, tfPage);
        BattleOptionPage itemPage = objPage.GetComponent<BattleOptionPage>();
        itemPage.Init(this);

        stackPage.Push(itemPage);
        btnClose.gameObject.SetActive(true);
    }

    public void ClosePage()
    {
        BattleOptionPage closePage = stackPage.Pop();
        Destroy(closePage.gameObject);

        if (stackPage.Count < 1)
        {
            btnClose.gameObject.SetActive(false);
        }
    }
}
