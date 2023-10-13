using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeUIItem : MonoBehaviour
{
    public Image imgFrame;
    public Image imgIcon;

    public Button btnNode;

    public void Init(int id)
    {
        btnNode.onClick.RemoveAllListeners();
        btnNode.onClick.AddListener(delegate ()
        {

        });
    }

}
