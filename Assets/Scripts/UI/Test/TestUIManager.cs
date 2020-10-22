using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TestUIManager : MonoBehaviour
{
    //find another way to make this private
    public static TestUIManager _main;

    // map the player is on
    [SerializeField]
    private Tilemap tilemap = null;    
    // current position of the mouse
    private Vector3Int mousePosition;
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


    // 1D array of all tiles in the tilemap
    public static TileBase[] tiles { get; private set; }
    // currently selected tile
    public static Tile tileSelected { get; private set; }
    // position of current selected tile
    public static (int, int) tilePosition { get; private set; }
    // index of currently selected tile based on "tiles" array
    public static int tileIndex { get; private set; }
    // currently selected tower
    public static TowerBehaviour towerSelected { get; private set; }

    // signals if user input for tile is active
    public static bool tileReceived { get; private set; }
    // signals if user input for tower is active
    public static bool towerReceived { get; private set; }
    // signals if user has clicked
    public static bool clickReceived { get; private set; }

    void Awake() {
        // register the tilemap set in the Unity editor
        Register.Tilemap(tilemap);

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
        
        // get current mouse position in relation to the tilemap
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = tilemap.WorldToCell(worldPoint);
        // register the currently selected tile
        Register.Tile(tilemap, mousePosition);

        //if (TestUIManager.pausedReceived)
        //{ 
        //    print("Pause input received");
        //}

        if (Input.GetMouseButtonDown(0) && !buttonClicked) {
            clickReceived = true;
        }

    }

    // clear user input
    protected virtual void Clear() {
        tileReceived = false;

        if (clickReceived) {
                towerReceived = false;
                clickReceived = false;
        }
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

    // allow other scripts to set a tile without directly accessing the tilemap variable
    public void SetTile(Vector3Int position, Tile tile) {
        tilemap.SetTile(position, tile);
    }

    // returns the relative (x, y) position of the mouse as if the bottom left tile
    //   of the tilemap is (0, 0)
    // this is useful for interacting with the 1D array returned from GetAllTiles()
    private static (int, int) GetRelativeXY(Tilemap map, Vector3Int position) {
        int relativeX = position[0] - map.cellBounds.xMin;
        int relativeY = position[1] - map.cellBounds.yMin;
    
        return (relativeX, relativeY);
    }

    // returns the index of a tile as if it were a 1D array based on the relative x, y coordinates
    private static int GetTileIndex(Tilemap map, Vector3Int position) {
        // get relative coordinates of x, y
        (int, int) relativeXY = GetRelativeXY(map, position);

        return relativeXY.Item1 + (map.cellBounds.size[0] * relativeXY.Item2);
    }

    // the Register class is an organizational tool used for signalling input data
    

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
        Debug.Log("Game is paused");
    }

    void PlayPause_Play()
    {
        PauseButton.onClick.RemoveAllListeners();
        PauseButton.onClick.AddListener(() => Register.Pause());
        PauseButton.onClick.AddListener(() => buttonClicked = true);
        Debug.Log("Game is playing");

    }

    protected static class Register {
        public static void Tilemap(Tilemap map) {
            tiles = map.GetTilesBlock(map.cellBounds);
        }

        public static void Tile(Tilemap map, Vector3Int position) {
            tileReceived = map.HasTile(position);
            tileSelected = map.GetTile<Tile>(position);
            tilePosition = GetRelativeXY(map, position);
            tileIndex = GetTileIndex(map, position);
        }

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
