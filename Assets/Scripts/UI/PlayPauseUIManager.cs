using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseUIManager : UIManager
{
    private static PlayPauseUIManager _main;

    public Button PlayButton;
    public Button PauseButton;
    public Text playPauseText;

    void Awake() {
        _main = this;
    }

    // Start is called before the first frame update
    void Start() {
        PlayButton.onClick.AddListener(() => Register.Play());
        PauseButton.onClick.AddListener(() => Register.Pause());

        PlayButton.onClick.AddListener(() => buttonClicked = true);
        PauseButton.onClick.AddListener(() => buttonClicked = true);
    }

    protected void ShowPausedUI()
    {
        playPauseText.text = "PLAY";
        PlayPause_Pause();
        PlayButton.gameObject.SetActive(true);
        PauseButton.gameObject.SetActive(false);

    }

    protected void ShowPlayUI()
    {
        playPauseText.text = "PAUSE";
        PlayPause_Play();
        PauseButton.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(false);

    }   

    public static void SetPlayState()
    {
        _main.ShowPlayUI();
    }

    public static void SetPauseState()
    {
        _main.ShowPausedUI();
    }

    void PlayPause_Pause()
    {
        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(() => Register.Play());
        PlayButton.onClick.AddListener(() => buttonClicked = true);
    }

    void PlayPause_Play()
    {
        PauseButton.onClick.RemoveAllListeners();
        PauseButton.onClick.AddListener(() => Register.Pause());
        PauseButton.onClick.AddListener(() => buttonClicked = true);
    }
}
