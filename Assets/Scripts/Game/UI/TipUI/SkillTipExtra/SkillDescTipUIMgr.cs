using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillDescTipUIMgr : MonoBehaviour
{
    public GameObject objPopup;

    [Header("BasicInfo")]
    public TextMeshProUGUI codeName;
    public TextMeshProUGUI codeType;
    public TextMeshProUGUI codeDesc;

    [Header("Cost")]
    public SkillCostTipUIMgr costTipUIMgr;


    public void Init(int nodeID,bool isNode)
    {
        SkillNodeExcelItem nodeExcelItem = PublicTool.GetSkillNodeItem(nodeID);
        SkillDescExcelItem descExcelItem = PublicTool.GetSkillDescItem(nodeID);

        if (nodeExcelItem.nodeType == SkillNodeType.Active)
        {
            SkillExcelItem skillItem = PublicTool.GetSkillItem(nodeExcelItem.id);

            switch (skillItem.activeSkillType)
            {
                case ActiveSkillType.NormalAttack:
                    codeType.text = string.Format("{0} {1}","<sprite=\"SkillType\" name=\"NSkill\">", "tx_skill_type_normal".ToLanguageText()).ToString();
                    break;
                case ActiveSkillType.DamageSkill:
                    codeType.text = string.Format("{0} {1}", "<sprite=\"SkillType\" name=\"HSkill\">", "tx_skill_type_hurt".ToLanguageText()).ToString();
                    break;
                case ActiveSkillType.SupportSkill:
                    codeType.text = string.Format("{0} {1}", "<sprite=\"SkillType\" name=\"SSkill\">", "tx_skill_type_support".ToLanguageText()).ToString();
                    break;
                case ActiveSkillType.UltimateSkill:
                    codeType.text = string.Format("{0} {1}", "<sprite=\"SkillType\" name=\"USkill\">", "tx_skill_type_ultimate".ToLanguageText()).ToString();
                    break;
            }

            if (isNode)
            {
                costTipUIMgr.UpdateUI(skillItem.costAP, skillItem.costMOV, skillItem.costHP, skillItem.costMemory);
            }
            else
            {
                costTipUIMgr.UpdateUI(skillItem.RealCostAP, skillItem.costMOV, skillItem.RealCostHP, skillItem.costMemory);
            }
            costTipUIMgr.ShowCost();
        }
        else
        {
            costTipUIMgr.HideCost();
            codeType.text = string.Format("{0} {1}", "<sprite=\"SkillType\" name=\"PSkill\">", "tx_skill_type_passive".ToLanguageText()).ToString();
        }

        codeName.text = descExcelItem.GetName();
        codeDesc.text = descExcelItem.GetDesc();

        objPopup.SetActive(true);
    }

}
