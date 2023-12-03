using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHoverMgr : CommonHoverUI
{
    public RectTransform rt;

    public Image imgBtn;
    public List<Sprite> listSpBtn;

    public Image txEN;
    public Image txCN;


    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            if (rt != null)
            {
                if (isHavor)
                {
                    imgBtn.sprite = listSpBtn[0];

                    if(GameGlobal.languageType == LanguageType.EN)
                    {
                        txEN.gameObject.SetActive(true);
                        txCN.gameObject.SetActive(false);
                    }
                    else
                    {
                        txCN.gameObject.SetActive(true);
                        txEN.gameObject.SetActive(false);
                    }
                }
                else
                {
                    imgBtn.sprite = listSpBtn[1];
                    txCN.gameObject.SetActive(false);
                    txEN.gameObject.SetActive(false);
                }
            }
        }
    }
}
