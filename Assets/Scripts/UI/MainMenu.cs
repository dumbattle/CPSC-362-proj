using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
        /*
         * This script runs when the PLAY button is activated.  
         * The Scene manager gets the current index of the active scene and loads the next scene in the sequence.
         * 
         * Current scene build order:
         * Scene            Index
         * ______________________
         * Menu             0
         * TowerPlace..     1
         * LearnToPlay      2
         * 
         * The order may be edited in File -> Build Settings
         * Drag the desired scene into the box to give it an index
         */
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LearnToPlay()
        /*
         * This script runs when the LEARN TO PLAY button is activated.  
         * The scene manager will load scene 2 (The LearnToPlay scene) and a console message will be output for testing purposes.
         */
    {
        UnityEngine.Debug.Log("How-to Selected");
        SceneManager.LoadScene(2);
    }

    public void ReturnToMainMenu()
        /*
            * This script runs when the BACK button is activated in the LearnToPlay scene (scene number 2 in the build order).  
            * The scene manager will load scene 0 (the MainMenu scene) and output a console message for testing purposes.
            */
    {
        UnityEngine.Debug.Log("Return to MainMenu selected");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
            /*
            * This script runs when the QUIT button is activated.  
            * A "PLAYER QUIT" message will be output to the console for testing purposes, then the application will be terminated.
            */
    {
        UnityEngine.Debug.Log("PLAYER QUIT");
        Application.Quit();
    }
}
