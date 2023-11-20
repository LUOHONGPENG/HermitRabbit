using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PublicTool
{
    /// <summary>
    /// Useful function for clearing the child objects 
    /// </summary>
    /// <param name="tf"></param>
    public static void ClearChildItem(UnityEngine.Transform tf)
    {
        foreach (UnityEngine.Transform item in tf)
        {
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// Useful function for Calculate angle. For example, from Target to (0,1)
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float CalculateAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? angle : -angle;
    }

    /// <summary>
    /// Useful function for calculating the UI position of a 3D object
    /// </summary>
    /// <param name="objPos"></param>
    /// <param name="tarCamera"></param>
    /// <returns></returns>
    public static Vector3 CalculateScreenUIPos(Vector3 objPos, Camera tarCamera)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(tarCamera, objPos);
        /*        screenPos = new Vector2(screenPos.x * 1920f / Screen.width, screenPos.y * 1080f / Screen.height);
                Vector2 targetPos = new Vector2(screenPos.x - 1920f / 2, screenPos.y - 1080f / 2);*/
        Vector2 targetPos = new Vector2(screenPos.x - Screen.width / 2, screenPos.y - Screen.height / 2);
        return new Vector3(targetPos.x, targetPos.y, 0);
    }

    /// <summary>
    /// Randomly pick one index with weight
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static int GetRandomIndexIntArray(int[] array)
    {
        int totalWeight = 0;
        //Sum up
        for (int i = 0; i < array.Length; i++)
        {
            totalWeight += array[i];
        }

        //Calculate
        if (totalWeight > 0)
        {
            int ran = Random.Range(0, totalWeight);
            for (int i = 0; i < array.Length; i++)
            {
                ran -= array[i];
                if (ran < 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public static List<int> DrawNum(int num, List<int> listPool, List<int> listDelete)
    {
        List<int> listTemp = new List<int>();
        List<int> listDraw = new List<int>(listPool);
        if (listDelete != null)
        {
            for (int i = 0; i < listDelete.Count; i++)
            {
                listDraw.Remove(listDelete[i]);
            }
        }

        for (int i = 0; i < num; i++)
        {
            int index = Random.Range(0, listDraw.Count);
            listTemp.Add(listDraw[index]);
            listDraw.RemoveAt(index);
        }
        return listTemp;
    }

    public static List<int> DrawNumWeight(int num,List<int> listPool, List<int> listWeight, List<int> listDelete)
    {
        //Weight cant be 0,Delete listDraw with 0
        List<int> listTemp = new List<int>();
        List<int> listDraw = new List<int>(listPool);
        List<int> listDrawWeight = new List<int>(listWeight);
        Dictionary<int, int> dicWeight = new Dictionary<int, int>();

        if(listDraw.Count != listWeight.Count)
        {
            Debug.LogError("Lose Weight!");
        }

        dicWeight.Clear();
        for(int i = 0; i < listDraw.Count; i++)
        {
            int keyID = listDraw[i];
            if (!dicWeight.ContainsKey(keyID))
            {
                dicWeight.Add(keyID, listDrawWeight[i]);
            }
        }

        if (listDelete != null)
        {
            for (int i = 0; i < listDelete.Count; i++)
            {
                listDraw.Remove(listDelete[i]);
            }
        }

        int TotalWeight = 0;
        List<int> listWeightSum = new List<int>();
        for (int i = 0; i < listDraw.Count; i++)
        {
            listWeightSum.Add(TotalWeight);
            TotalWeight += dicWeight[listDraw[i]];
        }

        while (listTemp.Count < num)
        {
            int ran = UnityEngine.Random.Range(0, TotalWeight);
            int keyIndex = listWeightSum.Count - 1;
            for(int i = 0;i< listWeightSum.Count-1; i++)
            {
                if(ran >= listWeightSum[i] && ran< listWeightSum[i + 1])
                {
                    keyIndex = i;
                    break;
                }
            }
            int keyID = listDraw[keyIndex];
            if (!listTemp.Contains(keyID))
            {
                listTemp.Add(keyID);
            }
        }

        return listTemp;
    }
}
