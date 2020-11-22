using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    public static bool open { 
        get { return _main.menuBackdrop.activeInHierarchy; } }
    private static InGameMenu _main;

    [Header("Menu Panel Backdrop")]
    public GameObject menuBackdrop;
    [Header("Menu Panel Buttons")]
    public Button menuButton;
    public Button restartButton;
    public Button mainMenuButton;
    public Button menuCancelButton;


    void Awake()
    {
        _main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        menuBackdrop.SetActive(false);
        menuButton.onClick.AddListener(() => menuBackdrop.SetActive(true));
        restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        menuCancelButton.onClick.AddListener(() => menuBackdrop.SetActive(false));
    }
}
