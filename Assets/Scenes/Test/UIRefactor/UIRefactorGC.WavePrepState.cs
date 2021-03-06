﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public partial class UIRefactorGC : MonoBehaviour
{
    GameState WavePrepState()
    {
        WaitUpdate();

        var h = handleClick();
        if(h != null)
        {
            return h;
        }

        if (UIManager.cancelTowerBuild)
        {
            tileHighlight.SetActive(false);
            TowerUIManager.SetTowerPanelState(false);
            TowerUIManager.SetUpgradesPanelState(false);
            TowerUIManager.SetCancelBuildState(false);

        }

        if (UIManager.playReceived)
        {
            PlayPauseUIManager.SetPlayState();
            return WaveStartState;
        }



        return null;

        GameState handleClick()
        {

            if (UIManager.clickReceived)
            {
                var (x, y) = mm.GetTilePosition(UIManager.mousePosition);

                if (tm.TileInRange(x, y) && mm.ValidTowerTile(x, y))
                {

                    if (tm.TileOccupied(x, y))
                    {
                        TowerUIManager.SetUpgradesPanelState(true);
                        TowerUIManager.SetTowerPanelState(false);
                        TowerUIManager.SetCancelBuildState(true); ;
                        tileHighlight.SetActive(false);
                        return TowerSelectedState(x, y);
                    }

                    else
                    {
                        tileHighlight.SetActive(true);
                        tileHighlight.transform.position = new Vector3(x, y, 0);
                        TowerUIManager.SetUpgradesPanelState(false);
                        TowerUIManager.SetCancelBuildState(true);
                        TowerUIManager.SetTowerPanelState(true);
                        return TowerPurchaseSubState(x, y);
                    }

                }
            }
            return null;
        }

        GameState TowerSelectedState(int x, int y)
        {
            return () =>
            {
                if (UIManager.sellReceived)
                {
                    var t = tm.GetTower(x, y);
                    em.AddMoney((int)(t.cost * .75f));
                    tm.RemoveTower(x, y);
                    t.DestroyTower();
                    TowerUIManager.SetUpgradesPanelState(false);
                    TowerUIManager.SetCancelBuildState(false);
                    return WavePrepState;
                }

                var b = handleClick();
                if (b != null)
                {
                    return b;
                }

                if (UIManager.cancelTowerBuild)
                {
                    tileHighlight.SetActive(false);
                    TowerUIManager.SetTowerPanelState(false);
                    TowerUIManager.SetUpgradesPanelState(false);
                    TowerUIManager.SetCancelBuildState(false);
                    return WavePrepState;

                }
                return null;
            };
        }

        GameState TowerPurchaseSubState(int x, int y)
        {
            return () =>
            {

                WaitUpdate();

                var a = handleClick();
                if (a != null)
                {
                    return a;
                }

                if (UIManager.cancelTowerBuild)
                {
                    tileHighlight.SetActive(false);
                    TowerUIManager.SetTowerPanelState(false);
                    TowerUIManager.SetUpgradesPanelState(false);
                    TowerUIManager.SetCancelBuildState(false);
                }


                if (UIManager.towerPurchaseReceived)
                {
                    if (em.TrySpend(UIManager.towerPurchased.cost))
                    {
                        var tower = tm.CreateTower(UIManager.towerPurchased, x, y);
                        TowerUIManager.SetCancelBuildState(false);

                        tileHighlight.SetActive(false);
                        return WavePrepState;
                    }
                }
                return null;
            };
        }
    }
}