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
    private const string BOOTSTRAP_SCENE = "Lab";


    [SerializeField] private string defaultGameplaySceneName;

    [Header("Event Channels")]
    [SerializeField] private EmptyPayloadEvent newGamePressedEvent;

    [SerializeField] private StringPayloadEvent questStartedEvent;
    [SerializeField] private StringPayloadEvent questFinishedEvent;
    [SerializeField] private StringPayloadEvent questObviatedEvent;
    [SerializeField] private EmptyPayloadEvent questsInitializedEvent;
    [SerializeField] private EmptyPayloadEvent questsLoadedFromSaveEvent;


    private QuestManager qm;
    private GameStateData currentGameStateData;
    public GameStateData CurrentGameStateData { get => currentGameStateData; }

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleOnSceneLoaded;
        newGamePressedEvent.OnEventTriggered += StartNewGame;
        questsInitializedEvent.OnEventTriggered += BuildGameStateData;
        questsLoadedFromSaveEvent.OnEventTriggered += BuildGameStateData;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleOnSceneLoaded;
        newGamePressedEvent.OnEventTriggered -= StartNewGame;
        questsInitializedEvent.OnEventTriggered -= BuildGameStateData;
        questsLoadedFromSaveEvent.OnEventTriggered -= BuildGameStateData;
    }

    private void BuildGameStateData()
    {
        Debug.Log("Building CurrentGameStateData");

        GameStateData data = new GameStateData();
        data.questStatus = new Dictionary<string, EnumQuestStatus>(qm.QuestStatus);
        data.timePlayedSeconds = Time.time;
        currentGameStateData = data;
    }

    private void SaveGame()
    {
        BuildGameStateData();
        SaveService.Save(currentGameStateData);
    }

    ////////////////////
    // EVENT HANDLERS //
    ////////////////////
    
    
    private void HandleOnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (scene.name == BOOTSTRAP_SCENE) return;
        currentGameStateData.currentSceneName = scene.name;
        BuildGameStateData();
    }

    private void StartNewGame()
    {
        Debug.Log("Foo");
        //ServiceManager.Instance.Quests.InitializeQuests();
        //SceneManager.LoadScene(defaultGameplaySceneName);
    }

    private void LoadSavedGame()
    {
        currentGameStateData = SaveService.Load();
        ServiceManager.Instance.Quests.LoadSavedQuestData(currentGameStateData.questStatus);
        SceneManager.LoadScene(currentGameStateData.currentSceneName);
    }


    /////////////
    // HELPERS //
    /////////////


}
