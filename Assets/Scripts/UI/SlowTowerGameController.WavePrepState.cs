using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public partial class SlowTowerGameController : MonoBehaviour
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

                if (UIManager.upgradeReceived)
                {
                    //get tower
                    var tower = tm.GetTower(x, y);
                    //get tower upgrade
                    var upgrade = tower.upgrade;
                    //check cost
                    if(upgrade != null && em.TrySpend(upgrade.cost))
                    {
                        tm.RemoveTower(x, y);
                        tower.DestroyTower();
                        //create upgraded tower
                        tm.CreateTower(upgrade, x, y);
                    }

                    //var upgrade = tower.UpgradeTower(x, y);

                    //if (upgrade != null)
                    //{
                    //    tower.DestroyTower();

                    //    TowerUIManager.SetTowerPanelState(false);
                    //    TowerUIManager.SetUpgradesPanelState(false);
                    //    TowerUIManager.SetCancelBuildState(false);

                    //    tileHighlight.SetActive(false);
                    //    return WavePrepState;
                    //}
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


                if (UIManager.TowerButtonClicked)
                {
                    if (em.TrySpend(UIManager.towerPurchased.cost))
                    {
                        var tower = tm.CreateTower(UIManager.towerPurchased, x, y);
                        TowerUIManager.SetTowerPanelState(false);
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