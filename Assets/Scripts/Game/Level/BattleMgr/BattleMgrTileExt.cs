using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleMgr
{
    public void TileBurningExpand()
    {
        Queue<MapTileData> queueOpen = new Queue<MapTileData>();
        List<Vector2Int> listClose = new List<Vector2Int>();

        for (int i = 0; i < gameData.listMapTile.Count; i++)
        {
            MapTileData mapTileData = gameData.listMapTile[i];
            if(mapTileData.curMapTileStatus == MapTileStatus.Burning)
            {
                queueOpen.Enqueue(mapTileData);
            }
        }

        while (queueOpen.Count > 0)
        {
            MapTileData mapTileData = queueOpen.Dequeue();
            Vector2Int tempPos = mapTileData.posID;
            listClose.Add(tempPos);

            ApplyBurnToTile(tempPos + new Vector2Int(0, 1), listClose);
            ApplyBurnToTile(tempPos + new Vector2Int(0, -1), listClose);
            ApplyBurnToTile(tempPos + new Vector2Int(1, 0), listClose);
            ApplyBurnToTile(tempPos + new Vector2Int(-1, 0), listClose);
        }

        mapViewMgr.RefreshTileView();
    }

    public void ApplyBurnToTile(Vector2Int tarPos, List<Vector2Int> listIgnore)
    {
        if (listIgnore.Contains(tarPos))
        {
            return;
        }

        if (gameData.GetMapTileData(tarPos) != null)
        {
            MapTileData tileData = gameData.GetMapTileData(tarPos);
            if(tileData.curMapTileStatus == MapTileStatus.CanBurn)
            {
                tileData.isBurning = true;
            }
        }

        listIgnore.Add(tarPos);
    }


    public IEnumerator IE_TileApplyBurning()
    {
        List<Vector2Int> listBurn = new List<Vector2Int>();

        for (int i = 0; i < gameData.listMapTile.Count; i++)
        {
            MapTileData mapTileData = gameData.listMapTile[i];
            if (mapTileData.curMapTileStatus == MapTileStatus.Burning)
            {
                listBurn.Add(mapTileData.posID);
            }
        }

        bool isApplyBurn = false;

        for(int i = 0; i < listBurn.Count; i++)
        {
            Vector2Int burnPos = listBurn[i];
            UnitInfo burnUnit = gameData.GetUnitInfoFromPosID(burnPos);
            if(burnUnit.type != BattleUnitType.None)
            {
                BattleUnitData unitData = gameData.GetDataFromUnitInfo(burnUnit);
                int BurnDelta = 0;
                if (unitData.CheckBuffExist(4001))
                {
                    BurnDelta = 1;
                }
                else
                {
                    BurnDelta = 2;
                }
                SkillBuffEffectDeal(unitData, 4001, BurnDelta, PublicTool.GetBuffExcelItem(4001).name, SkillEffectType.Harm);

                BattleUnitView unitView = unitViewMgr.GetViewFromUnitInfo(burnUnit);
                unitView.RequestBattleText();

                isApplyBurn = true;
            }
        }

        if (isApplyBurn)
        {
            yield return new WaitForSeconds(1f);
        }

        yield break;
    }

    public IEnumerator IE_FlowerTileHealPlant()
    {
        List<Vector2Int> listFlower = new List<Vector2Int>();

        for (int i = 0; i < gameData.listMapTile.Count; i++)
        {
            MapTileData mapTileData = gameData.listMapTile[i];
            if (mapTileData.GetDisplayMapType() == MapTileType.Flower)
            {
                listFlower.Add(mapTileData.posID);
            }
        }

        bool isApplyHeal = false;

        for (int i = 0; i < listFlower.Count; i++)
        {
            Vector2Int healPos = listFlower[i];
            UnitInfo healUnit = gameData.GetUnitInfoFromPosID(healPos);
            if (healUnit.type == BattleUnitType.Plant)
            {
                BattleUnitData unitData = gameData.GetDataFromUnitInfo(healUnit);
                unitData.GetHeal(2);
                unitData.EnqueueBattleText(new EffectBattleTextInfo(BattleTextType.Heal, 2.ToString(), unitData.posID));

                BattleUnitView unitView = unitViewMgr.GetViewFromUnitInfo(healUnit);
                unitView.RequestBattleText();

                isApplyHeal = true;
            }
        }

        if (isApplyHeal)
        {
            yield return new WaitForSeconds(1f);
        }
    }

}
