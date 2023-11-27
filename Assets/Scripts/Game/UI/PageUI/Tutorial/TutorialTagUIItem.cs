using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialTagUIItem : MonoBehaviour
{
    public Button btnTag;
    public Image imgBtn;
    public TextMeshProUGUI codeTag;

    public List<Color> listColorBg = new List<Color>();

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
            imgBtn.color = listColorBg[0];


        }
        else
        {
            imgBtn.color = listColorBg[1];

        }
    }

}
