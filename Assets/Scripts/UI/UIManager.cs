﻿using System.Collections;
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

    protected static bool buttonClicked = false;

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
        towerReceived = false;
        clickReceived = false;
        playReceived = false;
        pausedReceived = false;
        buttonClicked = false;
    }

    // the Register class is an organizational tool used for signalling input data
    protected static class Register {
        public static void Tower(TowerBehaviour tower) {
            towerReceived = true;
            towerPurchased = tower;
        }

        public static void Play()
        {
            playReceived = true;
        }
        public static void Pause()
        {
            pausedReceived = true;
        }
    }
}
