using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public partial class UIRefactorGCduplicate : MonoBehaviour
{
    GameState WavePrepState()
    {
        WaitUpdate();


        TowerUIManagerRefactor.SetPathIndicator(true); // shows path indicator starting at prep phase and ends when wave starts

        var h = handleClick();
        if(h != null)
        {
            return h;
        }

        if (UIManager.WaveStartReceived)
        {
            PlayPauseUIManager.SetStartWave(false);
            PlayPauseUIManager.SetPlayState();
            return WaveStartState;
        }



        return null;


        GameState handleClick()
        {
            if (UIManager.TowerButtonClicked)
            {
                if (em.EconCheck(UIManager.towerPurchased.cost))
                {
                    TowerUIManagerRefactor.SetBuyInstructions(true);
                    TowerUIManagerRefactor.SetBuyCancelButton(true);
                    ShowTileHighlights();

                    return TowerPlacementSubstate(UIManager.towerPurchased);
                }
                else
                {
                    TowerUIManagerRefactor.SetGoldCheck(true);

                    IEnumerator FadeTimer()
                    {

                        float timer = 0;
                        while (timer < 2.0f)
                        {
                            timer += Time.deltaTime;
                            yield return null;
                        }
                        TowerUIManagerRefactor.SetGoldCheck(false);
                    }

                    GlobalGameplayUpdate.AddGameplayWaitUpdate(FadeTimer());
                    return WavePrepState;
                }

            }

            if (UIManager.clickReceived)
            {
                var (x, y) = mm.GetTilePosition(UIManager.mousePosition);

                if (tm.TileInRange(x, y) && mm.ValidTowerTile(x, y))
                {

                    if (tm.TileOccupied(x, y))
                    {
                        TowerUIManagerRefactor.SetUpgradesPanelState(true);
                        HideTileHighlights();
                        return TowerSelectedState(x, y);
                    }
                }
            }

            

            return null;
        }

        GameState TowerSelectedState(int x, int y)
        {
            tileHighlight.SetActive(true);
            tileHighlight.transform.position = new Vector3(x, y);

            return () =>
            {
                WaitUpdate();

                if (UIManager.sellReceived)
                {
                    var t = tm.GetTower(x, y);
                    em.AddMoney((int)(t.cost * .75f));
                    tm.RemoveTower(x, y);
                    t.DestroyTower();
                    TowerUIManagerRefactor.SetUpgradesPanelState(false);
                    tileHighlight.SetActive(true);

                    return WavePrepState;
                }

                if (UIManager.cancelTowerBuild)
                {
                    TowerUIManagerRefactor.SetUpgradesPanelState(false);
                    tileHighlight.SetActive(false);


                    return WavePrepState;
                }

                if (UIManager.upgradeReceived)
                {
                    //get tower
                    var tower = tm.GetTower(x, y);
                    //get tower upgrade
                    var upgrade = tower.upgrade;
                    //check cost
                    if (upgrade != null && em.TrySpend(upgrade.cost))
                    {
                        tm.RemoveTower(x, y);
                        tower.DestroyTower();
                        //create upgraded tower
                        tm.CreateTower(upgrade, x, y);
                    }
                }
                    var b = handleClick();
                if (b != null)
                {
                    return b;
                }
                return null;


            };
        }

    

        GameState TowerPlacementSubstate(TowerBehaviour tower)
        {
            TowerUIManagerRefactor.SetBuyCancelButton(true);
            TowerUIManagerRefactor.SetBuyInstructions(true);
            return ()=> {

                WaitUpdate();
                //User can cancel
                if (UIManager.cancelTowerBuy)
                {
                    HideTileHighlights();

                    TowerUIManagerRefactor.SetBuyCancelButton(false);
                    TowerUIManagerRefactor.SetBuyInstructions(false);

                    return WavePrepState;
                }


                //User can select a tile
                if (UIManager.clickReceived)
                {
                    var (a, b) = mm.GetTilePosition(UIManager.mousePosition);

                    if (mm.ValidTowerTile(a, b) && tm.TileInRange(a, b) && !tm.TileOccupied(a, b))
                    {
                        TowerUIManagerRefactor.SetBuyButton(true);
                        TowerUIManagerRefactor.SetBuyInstructions(false);
                        HideTileHighlights();
                        tileHighlight.SetActive(true);
                        tileHighlight.transform.position = new Vector3(a, b);
                        return ConfirmTowerPurchase(a, b, tower);
                    }              
                }         
                return null;
            };
        }

        GameState ConfirmTowerPurchase(int x,int y, TowerBehaviour t)
        {
            return () =>
            {
                WaitUpdate();

                if (UIManager.cancelTowerBuy)
                {
                    TowerUIManagerRefactor.SetBuyButton(false);
                    TowerUIManagerRefactor.SetBuyCancelButton(false);
                    TowerUIManagerRefactor.SetBuyInstructions(false);
                    tileHighlight.SetActive(false);

                    return WavePrepState;
                }

                if (UIManager.towerPurchaseReceived)
                {
                    TowerUIManagerRefactor.SetBuyButton(false);
                    TowerUIManagerRefactor.SetBuyCancelButton(false);
                    em.TowerBuyConfirmed(UIManager.towerPurchased.cost);
                    var tower = tm.CreateTower(UIManager.towerPurchased, x, y);
                    tileHighlight.SetActive(false);


                    return WavePrepState;
                }

                if (UIManager.TowerButtonClicked)
                {
                    TowerUIManagerRefactor.SetBuyInstructions(true);
                    TowerUIManagerRefactor.SetBuyButton(false);
                    tileHighlight.SetActive(false);
                    ShowTileHighlights();


                    return TowerPlacementSubstate(UIManager.towerPurchased);
                }
                return null;
            };
        }   
    }
}