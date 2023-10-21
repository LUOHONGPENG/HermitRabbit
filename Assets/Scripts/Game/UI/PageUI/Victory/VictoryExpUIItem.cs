using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryExpUIItem : MonoBehaviour
{
    public Image imgIcon;

    public Image imgExpBar;
    public Image imgExpFill;

    public Text codeLevel;
    public Text codeExp;
    public Text codeAddExp;

    public bool isGrowExp = false;
    private int beforeExp;
    private int curTotalExp;
    private int addExp;
    private int addedExp;
    private int addExpPart;
    private CharacterExpExcelData expExcelData;

    public void Init(int characterID,int beforeExp,int addExp)
    {
        expExcelData = ExcelDataMgr.Instance.characterExpExcelData;
        isGrowExp = false;
        //Data
        this.beforeExp = beforeExp;
        curTotalExp = beforeExp;
        this.addExp = addExp;
        addedExp = 0;
        addExpPart = addExp / 10;
        //Refresh
        RefreshExpView();

    }

    public void GrowExp()
    {
        StartCoroutine(IE_GrowExp());
    }

    private IEnumerator IE_GrowExp()
    {
        for(int i = 0; i < 9; i++)
        {
            curTotalExp += addExpPart;
            addedExp += addedExp;
            RefreshExpView();
            yield return new WaitForSeconds(0.3f);
        }
        //Final
        curTotalExp = beforeExp + addExp;
        addedExp = addExp;
        RefreshExpView();

        isGrowExp = true;
        yield break;
    }

    private void RefreshExpView()
    {
        int level = expExcelData.GetLevelFromExp(curTotalExp);
        int curExp = curTotalExp - expExcelData.GetExpItem(level).EXP;
        int requiredExp = expExcelData.GetRequiredExp(level);

        codeLevel.text = string.Format("Lv.{0}", level);
        codeExp.text = curExp.ToString();
        //AddExp
        int needToAdd = addExp - addedExp;
        if (needToAdd > 0)
        {
            codeAddExp.text = string.Format("+{0}", needToAdd);
        }
        else
        {
            codeAddExp.text = "";
        }

        if (requiredExp > 0)
        {
            imgExpFill.fillAmount = curExp * 1.0f / requiredExp * 1.0f;
        }
        else
        {
            imgExpFill.fillAmount = 1;
        }
    }
}
