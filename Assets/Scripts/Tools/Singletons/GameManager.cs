using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;


public enum EnumQuestStatus
{
    NOT_STARTED,
    STARTED,
    FINISHED,
    OBVIATED
}

[System.Serializable]
public class GameStateData
{
    public float timePlayedSeconds;
    public string currentSceneName;
    public Dictionary<string, EnumQuestStatus> questStatus;

    public GameStateData()
    {
        timePlayedSeconds = 0f;
        currentSceneName = string.Empty;
        questStatus = new Dictionary<string, EnumQuestStatus>();
    }
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private string defaultGameplaySceneName;
    [SerializeField] private GameObject playerPrefab;

    [Header("UI Event Channels")]
    [SerializeField] private EmptyPayloadEvent newGamePressedEvent;
    [SerializeField] private EmptyPayloadEvent continuePressedEvent;
    [SerializeField] private EmptyPayloadEvent quitPressedEvent;
    [SerializeField] private EmptyPayloadEvent saveGamePressedEvent;

    [Header("Quest Event Channels")]
    [SerializeField] private EmptyPayloadEvent questsInitializedEvent;
    [SerializeField] private EmptyPayloadEvent questsLoadedFromSaveEvent;

    [Header("Input Event Channels")]
    [SerializeField] private EmptyPayloadEvent pauseInputEvent;

    [Header("Game EventChannels")]
    [SerializeField] private BoolPayloadEvent gameIsPausedEvent;
    [SerializeField] private EmptyPayloadEvent gameSavedEvent;

    private bool gameIsPaused = false;

    private EnumPlayerStart playerStart = EnumPlayerStart.DEFAULT;
    private GameStateData currentGameStateData;
    public GameStateData CurrentGameStateData { get => currentGameStateData; }

    private void Awake()
    {
        
    }

    private void Start()
    {
        ServiceManager.Instance.CanvasMgr.DisplayCanvas(EnumCanvasName.MAIN_MENU);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleOnSceneLoaded;

        //UI Events
        newGamePressedEvent.OnEventTriggered += StartNewGame;
        continuePressedEvent.OnEventTriggered += LoadSavedGame;
        quitPressedEvent.OnEventTriggered += QuitGame;
        saveGamePressedEvent.OnEventTriggered += SaveGame;

        // Quest events
        questsInitializedEvent.OnEventTriggered += BuildGameStateData;
        questsLoadedFromSaveEvent.OnEventTriggered += BuildGameStateData;

        // Input events
        pauseInputEvent.OnEventTriggered += HandleOnPausePressed;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleOnSceneLoaded;

        //UI Events
        newGamePressedEvent.OnEventTriggered -= StartNewGame;
        continuePressedEvent.OnEventTriggered -= LoadSavedGame;
        quitPressedEvent.OnEventTriggered -= QuitGame;
        saveGamePressedEvent.OnEventTriggered -= SaveGame;

        // Quest events
        questsInitializedEvent.OnEventTriggered -= BuildGameStateData;
        questsLoadedFromSaveEvent.OnEventTriggered -= BuildGameStateData;

        // Input events
        pauseInputEvent.OnEventTriggered -= HandleOnPausePressed;

    }

    private void BuildGameStateData()
    {
        //Debug.Log("Building CurrentGameStateData");
        QuestManager qm = ServiceManager.Instance.Quests;
        GameStateData data = new GameStateData();
        data.questStatus = new Dictionary<string, EnumQuestStatus>(qm.QuestStatus);
        data.timePlayedSeconds = Time.time;
        data.currentSceneName = SceneManager.GetActiveScene().name;
        currentGameStateData = data;

        DebugCurrentGameData();
    }

    private void SaveGame()
    {
        BuildGameStateData();
        SaveService.Save(currentGameStateData);
        gameSavedEvent.TriggerEvent();

        DebugCurrentGameData();

    }

    public void FadeToScene(string sceneName)
    {
        CanvasManager cm = ServiceManager.Instance.CanvasMgr;
        cm.DisplayCanvas(EnumCanvasName.LOADING);
        LoadingCanvas loadCanv = cm.GetIcanvasable(EnumCanvasName.LOADING) as LoadingCanvas;
        loadCanv.FadeIn(1.0f, 1.0f, EnumCanvasName.HUD);
        SceneManager.LoadScene(sceneName);
    }

    public void SetPlayerStart(EnumPlayerStart _playerStart)
    {
        playerStart = _playerStart;
    }

    ////////////////////
    // EVENT HANDLERS //
    ////////////////////
    
    private void HandleOnPausePressed()
    {
        CanvasManager cm = ServiceManager.Instance.CanvasMgr;
        if (!gameIsPaused && cm.CurrentActiveCanvas == EnumCanvasName.HUD)
        {
            gameIsPaused = true;
            cm.DisplayCanvas(EnumCanvasName.PAUSE);
            Debug.Log("PAUSE");
            gameIsPausedEvent.TriggerEvent(gameIsPaused);
            return;
        }
        if (gameIsPaused)
        {
            gameIsPaused = false;
            EnumCanvasName prev = cm.ActiveCanvasHistory[cm.ActiveCanvasHistory.Count - 1];
            cm.DisplayCanvas(prev);
            Debug.Log("UNPAUSE");
            gameIsPausedEvent.TriggerEvent(gameIsPaused);
            return;
        }

    }

    private void HandleOnMainMenuPressed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(ServiceManager.Instance.BootstrapSceneName);
        CanvasManager cm = ServiceManager.Instance.CanvasMgr;
        cm.DisplayCanvas(EnumCanvasName.MAIN_MENU);
    }

    private void HandleOnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (scene.name == ServiceManager.Instance.BootstrapSceneName)
        {
            gameIsPaused = false;
            gameIsPausedEvent.TriggerEvent(gameIsPaused);
            ServiceManager.Instance.CanvasMgr.DisplayCanvas(EnumCanvasName.MAIN_MENU);
            //BuildGameStateData();
            return;
        }


        BuildGameStateData();
        GameScene gameScene = GameObject.FindFirstObjectByType<GameScene>();
        if (gameScene == null) return;
        gameScene.SpawnPlayerAtPlayerStart(playerPrefab, playerStart);

        //DebugCurrentGameData();

    }

    private void StartNewGame()
    {
        ServiceManager.Instance.Quests.InitializeQuests();
        playerStart = EnumPlayerStart.DEFAULT;
        FadeToScene(defaultGameplaySceneName);
        
        //DebugCurrentGameData();
    }

    private void LoadSavedGame()
    {

        GameStateData savedData = SaveService.Load();
        ServiceManager.Instance.Quests.LoadSavedQuestData(savedData.questStatus);
        FadeToScene(savedData.currentSceneName);
    }



    private void QuitGame()
    {
# if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying) { UnityEditor.EditorApplication.isPlaying = false; return; }
#else
        Application.Quit();
#endif

    }


    /////////////
    // HELPERS //
    /////////////

    private void DebugCurrentGameData()
    {
        string questStr = "";

        foreach(string key in currentGameStateData.questStatus.Keys)
        {
            questStr += $"{key} -- {currentGameStateData.questStatus[key]}\n";
        }


        string debugStr = $"Current game data\nQuests:\n{questStr}\nTime played: {currentGameStateData.timePlayedSeconds}\nScene: {currentGameStateData.currentSceneName}\n\n";

        Debug.Log(debugStr);
    }
}
