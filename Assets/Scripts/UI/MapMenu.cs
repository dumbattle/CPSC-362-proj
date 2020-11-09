using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SelectMap1() {
        SceneManager.LoadScene(3);
    }

    public void SelectMap2() {
        SceneManager.LoadScene(4);
    }

    public void SelectMap3() {
        SceneManager.LoadScene(5);
    }
}
