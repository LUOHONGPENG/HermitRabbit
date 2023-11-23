using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleUnitData
{
    public MapTileData GetCurTileData()
    {
        MapTileData tempTile = PublicTool.GetGameData().GetMapTileData(posID);
        if (tempTile != null)
        {
            return tempTile;
        }
        else
        {
            return null;
        }
    }

    public MapTileType GetCurTileExpressType()
    {
        if (GetCurTileData() != null)
        {
            return GetCurTileData().GetDisplayMapType();
        }
        else
        {
            return MapTileType.End;
        }
    }


    #region TileBuff

    public int tileBuffATK
    {
        get
        {
            int temp = 0;
            if (GetCurTileExpressType() == MapTileType.Stealth)
            {
                temp-=2;
            }
            else if(GetCurTileExpressType() == MapTileType.Grass && battleUnitType == BattleUnitType.Plant)
            {
                temp++;
            }
            return temp;
        }
    }


    public int tileBuffDEF
    {
        get
        {
            int temp = 0;
            if (GetCurTileExpressType() == MapTileType.Duel)
            {
                temp--;
            }
            return temp;
        }
    }


    public int tileBuffRES
    {
        get
        {
            int temp = 0;
            if(GetCurTileExpressType() == MapTileType.Magic)
            {
                temp++;
            }
            else if(GetCurTileExpressType() == MapTileType.Duel)
            {
                temp--;
            }
            return temp;
        }
    }

    public int tileBuffRegenMOV
    {
        get
        {
            int temp = 0;
            if (GetCurTileExpressType() == MapTileType.Guard)
            {
                temp -= (curMaxMOV / 2);
            }
            return temp;
        }
    }

    public float tileBuffReduceHurtRate
    {
        get
        {
            float temp = 0;
            if (GetCurTileExpressType() == MapTileType.Guard)
            {
                temp += 0.4f;
            }
            return temp;
        }
    }

    public bool tileBuffChangeDmgReal
    {
        get
        {
            if (GetCurTileExpressType() == MapTileType.Duel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool tileBuffMagicDamageAdd
    {
        get
        {
            if (GetCurTileExpressType() == MapTileType.Magic)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool tileBuffHope
    {
        get
        {
            if (GetCurTileExpressType() == MapTileType.Hope)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public int tileBuffHateChange
    {
        get
        {
            int temp = 0;
            if (GetCurTileExpressType() == MapTileType.Stealth)
            {
                temp -= 9999;
            }
            return temp;
        }
    }


    #endregion

}
