using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr : MonoBehaviour
{
    public Transform tfMapTile;
    public GameObject pfMapTile;


    public void Init()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                GameObject objMapTile = GameObject.Instantiate(pfMapTile,new Vector3(i,0,j),Quaternion.identity, tfMapTile);
                
            }
        }
    }
}
