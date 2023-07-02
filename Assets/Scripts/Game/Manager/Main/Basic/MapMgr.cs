using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr : MonoBehaviour
{
    public Transform tfMapTile;
    public GameObject pfMapTile;

    public GameObject pfCharacter;

    public void Init()
    {
        GenerateTile(5, 5);

        GameObject objCharacter = GameObject.Instantiate(pfCharacter, new Vector3(0, 0.35f, 0), Quaternion.identity, this.transform);
    }

    public void GenerateTile(int sizeX,int sizeZ)
    {
        int centerX = (sizeX - 1) / 2;
        int centerZ = (sizeZ - 1) / 2;


        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                GameObject objMapTile = GameObject.Instantiate(pfMapTile, new Vector3(i - centerX, 0, j - centerZ), Quaternion.identity, tfMapTile);
                objMapTile.name = string.Format("MapTile{0}_{1}", i, j);
            }
        }
    }
}
