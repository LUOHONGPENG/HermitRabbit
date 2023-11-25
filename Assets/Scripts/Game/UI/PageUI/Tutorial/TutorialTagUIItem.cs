using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialTagUIItem : MonoBehaviour
{
    public Button btnTag;
    public TextMeshProUGUI codeTag;

    private TutorialGroup recordGroup;

    public void Init(TutorialGroup group ,UnityAction action)
    {
        this.recordGroup = group;

        codeTag.text = group.ToString();
        btnTag.onClick.RemoveAllListeners();
        btnTag.onClick.AddListener(action);
    }

    public void SetSelected(TutorialGroup curGroup)
    {
        if(recordGroup == curGroup)
        {

        }
        else
        {

        }
    }

}
