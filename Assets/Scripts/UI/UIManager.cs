using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIManager : MonoBehaviour
{   
    // current mouse position
    public static Vector3 mousePosition { get; protected set; }

    // input data
    public static bool towerPurchaseReceived { get; protected set; }
    public static bool clickReceived { get; protected set; }
    public static bool playReceived { get; protected set; }
    public static bool pausedReceived { get; protected set; }
    public static bool WaveStartReceived { get; protected set; }

    public static bool setTower1Received { get; protected set; }
    public static bool setTower2Received { get; protected set; }

    public static bool cancelTowerBuild { get; protected set; }
    public static bool cancelTowerBuy { get; protected set; }
    public static bool sellReceived { get; protected set; }
    public static bool upgradeReceived { get; protected set; }

    public static bool GoldCheckReceived { get; protected set; }

    protected static bool buttonClicked = false;
    public static bool TowerButtonClicked = false;

    public static TowerBehaviour towerPurchased { get; private set; }

    public static void CustomUpdate() {
        Clear();

        // get current mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !buttonClicked) {
            clickReceived = true;
        }
    }

    // clear user input
    protected static void Clear() {
        clickReceived = false;
        playReceived = false;
        pausedReceived = false;
        buttonClicked = false;
        cancelTowerBuild = false;
        sellReceived = false;

        TowerButtonClicked = false;
        cancelTowerBuy = false;
        towerPurchaseReceived = false;
        WaveStartReceived = false;
        GoldCheckReceived = false; // refactor this 

        upgradeReceived = false;

    }

    // the Register class is an organizational tool used for signalling input data
    protected static class Register {
        public static void Tower(TowerBehaviour tower) {
            TowerButtonClicked = true;
            towerPurchased = tower;
        }

        public static void WaveStart()
        {
            WaveStartReceived = true;
        }

        public static void Play()
        {
            playReceived = true;
        }
        public static void Pause()
        {
            pausedReceived = true;
        }

        public static void Cancel()
        {
            cancelTowerBuild = true;
        }

        public static void CancelBuy()
        {
            cancelTowerBuy = true;
        }

        public static void Sell()
        {
            sellReceived = true;
        }

        public static void Buy()
        {
            towerPurchaseReceived = true;
        }

        public static void GoldCheckConfirm()
        {
            GoldCheckReceived = true;
        }

        public static void Upgrade()
        {
            upgradeReceived = true;
        }
    }
}
