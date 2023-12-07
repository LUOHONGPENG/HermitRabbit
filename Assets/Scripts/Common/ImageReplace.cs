using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageReplace : MonoBehaviour
{
    protected Image imgContent;
    void OnEnable()
    {
        if (imgContent == null)
        {
            imgContent = this.gameObject.GetComponent<Image>();
        }

        RefreshContent();
    }

    private void RefreshContent()
    {
        if (imgContent != null)
        {
            string url = PublicTool.GetLanguageText(this.name);


        }
        
    }
}
