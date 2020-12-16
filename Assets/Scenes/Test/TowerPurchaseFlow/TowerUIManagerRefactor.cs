using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIManagerRefactor : UIManager
{
    private static TowerUIManagerRefactor _main;

    // button for each type of tower
    public Button tower1Button;
    public Button tower2Button;
    public Button tower3Button;

    public Button sellTower;
    public Button upgradeTower;
    public Button cancelTowerBuild;
    public Button BuyTower;
    public Button CancelBuy;

    public GameObject TowerInfoPanel;
    public GameObject TowerInfoOptions;
    public GameObject TowerBuyInstruction;
    public GameObject GoldCheck;
    public GameObject BuyIndicator;
    public GameObject TowerCost1;
    public GameObject TowerCost2;
    public GameObject TowerCost3;
    public GameObject UpgradeInfo;


    public GameObject PathIndicator;

    public Text UpgradeText;

    // tower objects for each type of tower                
    [SerializeField]
    private TowerBehaviour tower1 = null;
    [SerializeField]
    private TowerBehaviour tower2 = null;
    [SerializeField]
    private TowerBehaviour tower3 = null;


    void Awake()
    {
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
        upgradeTower.onClick.AddListener(() => Register.Upgrade());
        tower3Button.onClick.AddListener(() => Register.Tower(tower3));
        BuyTower.onClick.AddListener(() => Register.Buy());
        CancelBuy.onClick.AddListener(() => Register.CancelBuy());

    }

    /*
    The tower purchase panel and the upgrades panel should be exclusive states so 
    that only one can be active during any one frame. The game controller has to 
    go through the TowerManager to determine if a tower exists at a tile or not. 
    If a tower does exist at a tile the user clicks on, the tower purchase panel 
    will activate; otherwise, the upgrades panel will activate.
    */


    private void ShowPathIndicator()
    {
        PathIndicator.gameObject.SetActive(true);
    }

    private void HidePathIndicator()
    {
        PathIndicator.gameObject.SetActive(false);
    }

    private void ShowBuyButton()
    {
        BuyTower.gameObject.SetActive(true);
    }

    private void HideBuyButton()
    {
        BuyTower.gameObject.SetActive(false);
    }

    private void ShowUpgradesPanelUI(ITower T)
    {
        TowerInfoOptions.gameObject.SetActive(true);
        TowerInfoPanel.gameObject.SetActive(true);
        sellTower.gameObject.SetActive(true);
        cancelTowerBuild.gameObject.SetActive(true);
        upgradeTower.gameObject.SetActive(true);
        UpgradeInfo.gameObject.SetActive(true);
        UpgradeText.text = T.GetUpgradeDescription();

    }

    private void HideUpgradesPanelUI()
    {
        TowerInfoOptions.gameObject.SetActive(false);
        TowerInfoPanel.gameObject.SetActive(false);
        sellTower.gameObject.SetActive(false);
        cancelTowerBuild.gameObject.SetActive(false);
        UpgradeInfo.gameObject.SetActive(false);

    }

    private void ShowCancelBuy()
    {
        CancelBuy.gameObject.SetActive(true);
    }

    private void HideCancelBuy()
    {
        CancelBuy.gameObject.SetActive(false);
    }

    private void ShowBuyInstructions()
    {
        TowerBuyInstruction.gameObject.SetActive(true);

    }

    private void HideBuyInstructions()
    {
        TowerBuyInstruction.gameObject.SetActive(false);

    }

    private void ShowBuyIndicator()
    {
        BuyIndicator.gameObject.SetActive(true);

    }

    private void HideBuyIndicator()
    {
        BuyIndicator.gameObject.SetActive(false);

    }

    private void ShowGoldCheck()
    {
        GoldCheck.gameObject.SetActive(true);

    }

    private void HideGoldCheck()
    {
        GoldCheck.gameObject.SetActive(false);

    }

    private void ShowTowerCost()
    {
        TowerCost1.gameObject.SetActive(true);
        TowerCost2.gameObject.SetActive(true);
        TowerCost3.gameObject.SetActive(true);
    }
    private void HideTowerCost()
    {
        TowerCost1.gameObject.SetActive(false);
        TowerCost2.gameObject.SetActive(false);
        TowerCost3.gameObject.SetActive(false);
    }

    public static void SetTowerCost(bool active)
    {
        if (active)
        {
            _main.ShowTowerCost();
        }
        else
        {
            _main.HideTowerCost();
        }
    }


    public static void SetGoldCheck(bool active)
    {
        if (active)
        {
            _main.ShowGoldCheck();
        }
        else
        {
            _main.HideGoldCheck();
        }
    }
    public static void SetBuyInstructions(bool active)
    {
        if (active)
        {
            _main.ShowBuyInstructions();
        }
        else
        {
            _main.HideBuyInstructions();
        }
    }

    public static void SetBuyIndicator(bool active)
    {
        if (active)
        {
            _main.ShowBuyIndicator();
        }
        else
        {
            _main.HideBuyIndicator();
        }
    }

    public static void SetBuyButton(bool active)
    {
        if (active)
        {
            _main.ShowBuyButton();
        }
        else
        {
            _main.HideBuyButton();
        }
    }

    public static void SetBuyCancelButton(bool active)
    {
        if (active)
        {
            _main.ShowCancelBuy();
        }
        else
        {
            _main.HideCancelBuy();
        }
    }

    public static void SetUpgradesPanelState(bool active, ITower T)
    {

        if (active)
        {
            _main.ShowUpgradesPanelUI(T);
        }
        else
        {
            _main.HideUpgradesPanelUI();
        }
    }

    public static void SetPathIndicator(bool active)
    {
        if (active)
        {
            _main.ShowPathIndicator();
        }
        else
        {
            _main.HidePathIndicator();
        }
    }



}
