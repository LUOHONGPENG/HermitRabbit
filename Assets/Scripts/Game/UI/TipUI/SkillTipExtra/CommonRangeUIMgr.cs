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

    public void Init(SkillRegionType regionType, int range, int radius)
    {
        PublicTool.ClearChildItem(tfRangeIcon);

        GenerateTarget();
        Vector2Int targetPos = Vector2Int.zero;
        Vector2Int sourcePos = new Vector2Int(0, -range);
        switch (regionType)
        {
            case SkillRegionType.BurnUnit:
                sourcePos = new Vector2Int(0, -3);
                break;
            case SkillRegionType.Line:
                sourcePos = new Vector2Int(0, -1);
                break;
            default:
                break;
        }
        GenerateSource(sourcePos);
        GenerateDot(targetPos, sourcePos);

        GenerateRadius(regionType, range, radius);
    }


    public void GenerateTarget()
    {
        GameObject objTarget = GameObject.Instantiate(pfRangeTarget, tfRangeIcon);
        objTarget.transform.localPosition = Vector2.zero;
    }

    public void GenerateSource(Vector2Int posID)
    {
        GameObject objSource = GameObject.Instantiate(pfRangeSource, tfRangeIcon);
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
