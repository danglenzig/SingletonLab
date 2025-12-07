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

    [Header("Quest Event Channels")]
    [SerializeField] private EmptyPayloadEvent questsInitializedEvent;
    [SerializeField] private EmptyPayloadEvent questsLoadedFromSaveEvent;

    [Header("Input Event Channels")]
    [SerializeField] private EmptyPayloadEvent pauseInputEvent;



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

        // Quest events
        questsInitializedEvent.OnEventTriggered += BuildGameStateData;
        questsLoadedFromSaveEvent.OnEventTriggered += BuildGameStateData;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleOnSceneLoaded;

        //UI Events
        newGamePressedEvent.OnEventTriggered -= StartNewGame;
        continuePressedEvent.OnEventTriggered -= LoadSavedGame;
        quitPressedEvent.OnEventTriggered -= QuitGame;

        // Quest events
        questsInitializedEvent.OnEventTriggered -= BuildGameStateData;
        questsLoadedFromSaveEvent.OnEventTriggered -= BuildGameStateData;
    }

    private void BuildGameStateData()
    {
        Debug.Log("Building CurrentGameStateData");
        QuestManager qm = ServiceManager.Instance.Quests;
        GameStateData data = new GameStateData();
        data.questStatus = new Dictionary<string, EnumQuestStatus>(qm.QuestStatus);
        data.timePlayedSeconds = Time.time;
        data.currentSceneName = SceneManager.GetActiveScene().name;
        currentGameStateData = data;
    }

    private void SaveGame()
    {
        BuildGameStateData();
        SaveService.Save(currentGameStateData);
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
    
    
    private void HandleOnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (scene.name == ServiceManager.Instance.BootstrapSceneName) return;
        BuildGameStateData();
        GameScene gameScene = GameObject.FindFirstObjectByType<GameScene>();
        if (gameScene == null) return;
        gameScene.SpawnPlayerAtPlayerStart(playerPrefab, playerStart);

    }

    private void StartNewGame()
    {
        ServiceManager.Instance.Quests.InitializeQuests();
        playerStart = EnumPlayerStart.DEFAULT;
        FadeToScene(defaultGameplaySceneName);
    }

    private void LoadSavedGame()
    {
        currentGameStateData = SaveService.Load();
        ServiceManager.Instance.Quests.LoadSavedQuestData(currentGameStateData.questStatus);
        FadeToScene(currentGameStateData.currentSceneName);
    }



    private void QuitGame()
    {
        if (UnityEditor.EditorApplication.isPlaying) { UnityEditor.EditorApplication.isPlaying = false; return; }
        Application.Quit();

    }


    /////////////
    // HELPERS //
    /////////////


}
