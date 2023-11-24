using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextReplace : MonoBehaviour
{
    protected TextMeshProUGUI txContent;

    void OnEnable()
    {
        if (txContent == null)
        {
            txContent = this.gameObject.GetComponent<TextMeshProUGUI>();
        }

        RefreshContent();
    }

    private void RefreshContent()
    {
        txContent.text = PublicTool.GetLanguageText(this.name);
    }

}
