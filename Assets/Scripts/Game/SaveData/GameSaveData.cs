using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class GameSaveData
{
    //Day
    public int numDay = 0;
    //Resource
    public int essence = 0;
    public int memory = 0;
    //MapClip
    public List<int> listMapClipHeld = new List<int>();
    public List<Vector3Int> listMapClipUsed = new List<Vector3Int>();//posx/posy/typeID
    //Character
    public List<Vector2Int> listCharacterExp = new List<Vector2Int>();//typeID/Exp
    public List<Vector2Int> listCharacterSkill = new List<Vector2Int>();//typeID/SkillNodeID
    //Plant
    public List<int> listPlantHeld = new List<int>();
    public List<Vector3Int> listPlantUsed = new List<Vector3Int>();//posx/posy/typeID

}
