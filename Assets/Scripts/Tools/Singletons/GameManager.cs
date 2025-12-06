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
    public Dictionary<string, EnumQuestStatus> questStatus;

    public GameStateData()
    {
        timePlayedSeconds = 0f;
        questStatus = new Dictionary<string, EnumQuestStatus>();
    }
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private string defaultGameplaySceneName;

    [SerializeField] private EmptyPayloadEvent gameSavedEvent;
    //[SerializeField] private EmptyPayloadEvent saveLoadedEvent;
    [SerializeField] private EmptyPayloadEvent gameDataUpdatedEvent;

    private QuestManager qm;

    private GameStateData currentGameStateData;
    public GameStateData CurrentGameStateData { get => currentGameStateData; }

    private void Awake()
    {
        
    }

    private void OnNewGameStart()
    {
        Debug.Log("NEW GAME");
        qm.InitializeQuests();
        BuildGameStateData();

    }
    private void OnSavedGameStart()
    {
        if (qm == null) { qm = ServiceManager.Instance.Quests; }
        Debug.Log("LOADING SAVED GAME");
        currentGameStateData = SaveService.Load();
        gameDataUpdatedEvent.TriggerEvent();
        qm.LoadSavedQuestData(currentGameStateData.questStatus);
    }


    public void RefreshGameState()
    {
        BuildGameStateData();
    }

    private void BuildGameStateData()
    {
        GameStateData data = new GameStateData();
        data.questStatus = new Dictionary<string, EnumQuestStatus>(qm.QuestStatus);
        data.timePlayedSeconds = Time.time;
        currentGameStateData = data;
        gameDataUpdatedEvent.TriggerEvent();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleOnSceneLoaded;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleOnSceneLoaded;
    }

    
    public void StartNewGame()
    {
        currentGameStateData = new GameStateData();
        //ui.ShowHud();
        //testingCanvas.SetActive(true); // temp...

        

        //QuestManager qm = ServiceManager.Instance.Quests;
        //ui.ShowLoadingScreen();
        //SceneManager.LoadScene(defaultGameplaySceneName);
    }

    public void SaveGame()
    {
        BuildGameStateData();
        SaveService.Save(currentGameStateData);
    }

    ////////////////////
    // EVENT HANDLERS //
    ////////////////////
    
    
    private void HandleOnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        
    }
    

    /////////////
    // HELPERS //
    /////////////
    

}
