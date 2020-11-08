using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    //find another way to make this private
    public static TestUI _main;

    // current mouse position
    public static Vector3 mousePosition { get; private set; }
    // tower objects for each type of tower                
    [SerializeField]
    private TowerBehaviour tower1 = null;
     [SerializeField]
    private TowerBehaviour tower2 = null; 
    // button for each type of tower
    public Button tower1Button;
    public Button tower2Button;

    //input data
    public static bool playReceived { get; protected set; }
    public static bool pausedReceived { get; protected set; }
    public static bool click { get; protected set; }

    public Button PlayButton;
    public Button PauseButton;

    public Text playPauseText;

    bool buttonClicked = false;

    public static TowerBehaviour towerSelected { get; private set; }

    public static bool towerReceived { get; private set; }
    // signals if user has clicked
    public static bool clickReceived { get; private set; }

    void Awake() {
        _main = this;
    }

    void Start() {
        // when the user clicks any tower button, that tower type is registered
        //   as the current tower type selected
        tower1Button.onClick.AddListener(() => Register.Tower(tower1));
        tower2Button.onClick.AddListener(() => Register.Tower(tower2));

        PlayButton.onClick.AddListener(() => Register.Play());
        PauseButton.onClick.AddListener(() => Register.Pause());

        PlayButton.onClick.AddListener(() => buttonClicked = true);
        PauseButton.onClick.AddListener(() => buttonClicked = true);

    }

    //temporary update, needs to be fixed (read line 9) there is an ordering issue
    public void CustomUpdate() {
        Clear();
        
        // get current mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !buttonClicked) {
            clickReceived = true;
        }

    }

    // clear user input
    protected virtual void Clear() {
        towerReceived = false;
        clickReceived = false;
        playReceived = false;
        pausedReceived = false;
        click = false;

        buttonClicked = false;

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

    // the Register class is an organizational tool used for signalling input data
    protected static class Register {
        public static void Tower(TowerBehaviour tower) {
            towerReceived = true;
            towerSelected = tower;
        }

        public static void Play()
        {
            playReceived = true;
        }
        public static void Pause()
        {
            pausedReceived = true;
        }
        public static void Click()
        {
            click = true;
        }
    }
}
