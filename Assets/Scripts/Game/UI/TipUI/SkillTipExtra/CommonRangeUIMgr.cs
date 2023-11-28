using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonRangeUIMgr : MonoBehaviour
{
    public Transform tfRangeIcon;
    public GameObject pfRangeTarget;
    public GameObject pfRangeRadius;
    public GameObject pfRangeSource;
    public GameObject pfRangeDot;

    public RectTransform rtSelf;

    bool isSelf = false;

    public void Init(SkillRegionType regionType, int range, int radius, BattleUnitType type)
    {
        PublicTool.ClearChildItem(tfRangeIcon);


        if (range == 0)
        {
            rtSelf.gameObject.SetActive(true);
            isSelf = true;
            //
        }
        else
        {
            rtSelf.gameObject.SetActive(false);
            isSelf = false;

            //GenerateTarget();
        }



        Vector2Int targetPos = Vector2Int.zero;
        Vector2Int sourcePos = new Vector2Int(0, -range);
        switch (regionType)
        {
            case SkillRegionType.BurnUnit:
                sourcePos = new Vector2Int(0, -3);
                rtSelf.anchoredPosition = new Vector2(0, -30 + 4 * -20f);
                GenerateTarget(regionType);
                break;
            case SkillRegionType.Line:
                sourcePos = new Vector2Int(0, -1);
                GenerateTarget();
                break;
            case SkillRegionType.Water:
                GenerateTarget(regionType);
                break;
            default:
                GenerateTarget();
                rtSelf.anchoredPosition = new Vector2(0, -30 + radius * -20f);
                break;
        }

        


        GenerateSource(sourcePos, type);
        GenerateDot(targetPos, sourcePos);

        GenerateRadius(regionType, range, radius);


    }


    public void GenerateTarget(SkillRegionType skillRegionType = SkillRegionType.Circle)
    {
        if(!isSelf || skillRegionType== SkillRegionType.BurnUnit)
        {
            GameObject objTarget = GameObject.Instantiate(pfRangeTarget, tfRangeIcon);
            SkillRangeTargetItem itemTarget = objTarget.GetComponent<SkillRangeTargetItem>();
            itemTarget.Init(skillRegionType);
            objTarget.transform.localPosition = Vector2.zero;
        }

    }

    public void GenerateSource(Vector2Int posID,BattleUnitType type)
    {
        GameObject objSource = GameObject.Instantiate(pfRangeSource, tfRangeIcon);
        SkillRangeSourceItem itemSource = objSource.GetComponent<SkillRangeSourceItem>();
        itemSource.Init(type);
        objSource.transform.localPosition = new Vector2(posID.x * 22f, posID.y * 22f);
    }

    public void GenerateDot(Vector2Int target, Vector2Int source)
    {
        for (int i = target.y - 1; i > source.y; i--)
        {
            GameObject objDot = GameObject.Instantiate(pfRangeDot, tfRangeIcon);
            objDot.transform.localPosition = new Vector2(0, i * 22f);
        }
    }

    public void GenerateRadius(SkillRegionType regionType, int range, int radius)
    {
        List<Vector2Int> listPos = new List<Vector2Int>();
        switch (regionType)
        {
            case SkillRegionType.Circle:
                listPos = PublicTool.GetTargetCircleRange(Vector2Int.zero, radius);
                break;
            case SkillRegionType.Square:
                listPos = PublicTool.GetTargetSquareRange(Vector2Int.zero, radius);
                break;
            case SkillRegionType.Line:
                listPos = PublicTool.GetTargetLineRange(Vector2Int.zero, new Vector2Int(0, -1), range, radius);
                break;
        }

        for (int i = 0; i < listPos.Count; i++)
        {
            if (listPos[i] != Vector2Int.zero)
            {
                Vector2Int posID = listPos[i];
                GameObject objRadius = GameObject.Instantiate(pfRangeRadius, tfRangeIcon);
                objRadius.transform.localPosition = new Vector2(posID.x * 22f, posID.y * 22f);
            }
        }
    }



}
