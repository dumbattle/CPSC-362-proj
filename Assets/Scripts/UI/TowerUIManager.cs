using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIManager : UIManager
{
    private static TowerUIManager _main;

    // button for each type of tower
    public Button tower1Button;
    public Button tower2Button;

    // tower objects for each type of tower                
    [SerializeField]
    private TowerBehaviour tower1 = null;
     [SerializeField]
    private TowerBehaviour tower2 = null; 

    void Awake() {
        _main = this;
    }

    void Start() {
        // when the user clicks any tower button, that tower type is registered
        //   as the current tower type selected
        tower1Button.onClick.AddListener(() => Register.Tower(tower1));
        tower2Button.onClick.AddListener(() => Register.Tower(tower2));
    }

    /*
    The tower purchase panel and the upgrades panel should be exclusive states so 
    that only one can be active during any one frame. The game controller has to 
    go through the TowerManager to determine if a tower exists at a tile or not. 
    If a tower does exist at a tile the user clicks on, the tower purchase panel 
    will activate; otherwise, the upgrades panel will activate.
    */

    private void ShowTowerPanelUI() {
        // implement
    }

    private void ShowUpgradesPanelUI() {
        // implement
    }

    public static void SetTowerPanelState() {
        _main.ShowTowerPanelUI();
    }

    public static void SetUpgradesPanelState() {
        _main.ShowUpgradesPanelUI();
    }

}
