using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SkillLineUIItem : MonoBehaviour
{
    public RectTransform rtLine;
    public Image imgLine;

    private int recordCharacter;
    private int recordStartID;
    private int recordEndID;

    public List<Color> listColor = new List<Color>();


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

    public void Record(int characterID,int startID, int endID)
    {
        recordCharacter = characterID;
        recordStartID = startID;
        recordEndID = endID;
    }

    public void Refresh()
    {
        if (PublicTool.CheckWhetherCharacterUnlockSkill(recordCharacter, recordEndID))
        {
            if (PublicTool.CheckWhetherCharacterUnlockSkill(recordCharacter, recordStartID))
            {
                imgLine.color = listColor[2];

            }
            else
            {
                imgLine.color = listColor[0];

            }
        }
        else
        {
            imgLine.color = listColor[1];
        }
    }

}
