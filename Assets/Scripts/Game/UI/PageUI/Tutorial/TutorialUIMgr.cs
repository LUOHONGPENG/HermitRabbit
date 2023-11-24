using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIMgr : MonoBehaviour
{

    public GameObject objPopup;
    [Header("Basic")]
    public TextMeshProUGUI codeTitle;
    public TextMeshProUGUI codeContent;

    [Header("First")]
    public GameObject objFirst;
    public Button btnContinue;
    public Button btnFinish;

    [Header("Common")]
    public GameObject objClose;
    public Button btnClose;


    public void Init()
    {
        btnContinue.onClick.RemoveAllListeners();
        btnContinue.onClick.AddListener(delegate ()
        {

        });

        btnFinish.onClick.RemoveAllListeners();
        btnFinish.onClick.AddListener(delegate ()
        {

        });

        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {

        });
    }


    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }

}
