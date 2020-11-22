using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseUIManager : UIManager
{
    private static PlayPauseUIManager _main;

    public Button PlayButton;
    public Button PauseButton;
    public Button WaveStartButton;
    //public Text playPauseText;

    public GameObject WinLosePanel;

    void Awake() {
        _main = this;
    }

    // Start is called before the first frame update
    void Start() {
        PlayButton.onClick.AddListener(() => Register.Play());
        PauseButton.onClick.AddListener(() => Register.Pause());
        WaveStartButton.onClick.AddListener(() => Register.WaveStart());

        PlayButton.onClick.AddListener(() => buttonClicked = true);
        PauseButton.onClick.AddListener(() => buttonClicked = true);
    }

    protected void ShowPausedUI()
    {
        //playPauseText.text = "PLAY";
        PlayPause_Pause();
        PlayButton.gameObject.SetActive(true);
        PauseButton.gameObject.SetActive(false);

    }

    protected void ShowPlayUI()
    {
        //playPauseText.text = "PAUSE";
        PlayPause_Play();
        PauseButton.gameObject.SetActive(true);
        PlayButton.gameObject.SetActive(false);

    }   

    protected void ShowStartWave()
    {
        WaveStartButton.gameObject.SetActive(true);
    }

    protected void HideStartWave()
    {
        WaveStartButton.gameObject.SetActive(false);
    }

    protected void ShowWinLosePanel()
    {
        WinLosePanel.gameObject.SetActive(true);
    }

    protected void HideWinLosePanel()
    {
        WinLosePanel.gameObject.SetActive(false);
    }

    public static void SetWinLosePanel(bool active)
    {
        if (active)
        {
            _main.ShowWinLosePanel();
        }
        else
            _main.HideWinLosePanel();

    }


    public static void SetPlayState()
    {
        _main.ShowPlayUI();
    }

    public static void SetPauseState()
    {
        _main.ShowPausedUI();
    }

    public static void SetStartWave(bool active)
    {
        if (active)
        {
            _main.ShowStartWave();
        }
        else
        {
            _main.HideStartWave();
        }
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
