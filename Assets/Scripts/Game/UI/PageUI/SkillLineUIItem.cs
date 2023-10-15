using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLineUIItem : MonoBehaviour
{
    public RectTransform rtLine;
    public Image imgLine;

    public void Init(Vector2 posStart,Vector2 posEnd)
    {
        Vector2 direction = posEnd - posStart;
        float length = Mathf.Sqrt(direction.sqrMagnitude);
        rtLine.localPosition = (posStart + posEnd) / 2;
        rtLine.sizeDelta = new Vector2(5f, length);

        float angle = Vector2.Angle(Vector2.up, direction);
        if (direction.x > 0)
        {
            angle = -angle;
        }
        rtLine.rotation = Quaternion.Euler(new Vector3(0,0, angle));
    }
}
