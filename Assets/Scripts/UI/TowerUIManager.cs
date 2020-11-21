using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIManager : UIManager
{
    private static TowerUIManager _main;

    // button for each type of tower
    [Header("Tower Panel Buttons")]
    public Button tower1Button;
    public Button tower2Button;
    public Button tower3Button;
    [Header("Tower Options Panel")]
    public Button sellTower;
    public Button upgradeTower;
    public Button cancelTowerBuild;
    [Header("Tower Panel Backdrop")]
    public GameObject TowerPurchasePanel;

    // tower objects for each type of tower   
    [Header("Starting Towers")]
    [SerializeField]
    private TowerBehaviour tower1 = null;
    [SerializeField]
    private TowerBehaviour tower2 = null;
    [SerializeField]
    private TowerBehaviour tower3 = null;
    [Header("Menu Panel Backdrop")]
    public GameObject menuBackdrop;
    [Header("Menu Panel Buttons")]
    public Button menuButton;
    public Button restartButton;
    public Button mainMenuButton;
    public Button menuCancel;

    void Awake() {
        _main = this;
    }

    void Start()
    {
        // when the user clicks any tower button, that tower type is registered
        //   as the current tower type selected

        tower1Button.onClick.AddListener(() => Register.Tower(tower1));
        tower2Button.onClick.AddListener(() => Register.Tower(tower2));
        cancelTowerBuild.onClick.AddListener(() => Register.Cancel());
        sellTower.onClick.AddListener(() => Register.Sell());
        tower3Button.onClick.AddListener(() => Register.Tower(tower3));
        upgradeTower.onClick.AddListener(() => Register.Upgrade());
        menuButton.onClick.AddListener(() => Register.Menu());
        restartButton.onClick.AddListener(() => Register.Restart());
        mainMenuButton.onClick.AddListener(() => Register.MainMenu());
        menuCancel.onClick.AddListener(() => Register.MenuCancel());
    }

    /*
    The tower purchase panel and the upgrades panel should be exclusive states so 
    that only one can be active during any one frame. The game controller has to 
    go through the TowerManager to determine if a tower exists at a tile or not. 
    If a tower does exist at a tile the user clicks on, the tower purchase panel 
    will activate; otherwise, the upgrades panel will activate.
    */

    private void ShowTowerPanelUI() {
       
        tower1Button.gameObject.SetActive(true);
        tower2Button.gameObject.SetActive(true);
        tower3Button.gameObject.SetActive(true);
        TowerPurchasePanel.gameObject.SetActive(true);

    }

    private void HideTowerPanelUI()
    {
        //print("hide tower panel");
        tower1Button.gameObject.SetActive(false);
        tower2Button.gameObject.SetActive(false);
        tower3Button.gameObject.SetActive(false);
        TowerPurchasePanel.gameObject.SetActive(false);
    }

    private void ShowMenuPanelUI()
    {
        menuCancel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        menuBackdrop.gameObject.SetActive(true);
    }

    private void HideMenuPanelUI()
    {
        menuCancel.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        menuBackdrop.gameObject.SetActive(false);
    }

    private void ShowUpgradesPanelUI() {
        sellTower.gameObject.SetActive(true);
        upgradeTower.gameObject.SetActive(true);
    }

    private void HideUpgradesPanelUI()
    {
        sellTower.gameObject.SetActive(false);
        upgradeTower.gameObject.SetActive(false);
    }

    private void ShowCancelBuild()
    {
        cancelTowerBuild.gameObject.SetActive(true);
    }

    private void HideCancelBuild()
    {
        cancelTowerBuild.gameObject.SetActive(false);
    }

    public static void SetTowerPanelState(bool active) {
        if (active)
        {
            _main.ShowTowerPanelUI();
        }
        else
        {
            _main.HideTowerPanelUI();
        }
    }

    public static void SetUpgradesPanelState(bool active) {

        if (active)
        {
            _main.ShowUpgradesPanelUI();
        }
        else
        {
            _main.HideUpgradesPanelUI();
        }
    }

    public static void SetCancelBuildState(bool active)
    {
        if (active)
        {
            _main.ShowCancelBuild();
        }
        else
        {
            _main.HideCancelBuild();
        }
    }

    public static void SetMenuPanelState(bool active)
    {
        if (active)
        {
            _main.ShowMenuPanelUI();
        }
        else
        {
            _main.HideMenuPanelUI();
        }
    }
}
