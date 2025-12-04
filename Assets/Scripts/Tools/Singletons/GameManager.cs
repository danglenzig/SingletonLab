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

    [SerializeField] private GameObject testingCanvas;
    [SerializeField] private string defaultGameplaySceneName;

    [SerializeField] private EmptyPayloadEvent gameSavedEvent;
    [SerializeField] private EmptyPayloadEvent saveLoadedEvent;

    private UIManager ui;
    private QuestManager qm;

    private GameStateData currentGameStateData;
    public GameStateData CurrentGameStateData { get => currentGameStateData; }

    private void Start()
    {


        ui = ServiceManager.Instance.UI;
        qm = ServiceManager.Instance.Quests;

        if (!SaveService.SaveExists())
        {
            Debug.Log("NEW GAME");
            qm.InitializeQuests();
            currentGameStateData = BuildGameStateData();
            saveLoadedEvent.TriggerEvent();

        }
        else
        {
            Debug.Log("LOADING SAVED GAME");
            currentGameStateData = SaveService.Load();
            qm.LoadSavedQuestData(currentGameStateData.questStatus);
            saveLoadedEvent.TriggerEvent();
        }
    }
    public void RefreshGameState()
    {
        currentGameStateData = BuildGameStateData();
    }

    private GameStateData BuildGameStateData()
    {
        GameStateData data = new GameStateData();
        data.questStatus = new Dictionary<string, EnumQuestStatus>(qm.QuestStatus);
        data.timePlayedSeconds = Time.time;
        return data;
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
        ui.ShowHud();
        testingCanvas.SetActive(true); // temp...

        

        //QuestManager qm = ServiceManager.Instance.Quests;
        //ui.ShowLoadingScreen();
        //SceneManager.LoadScene(defaultGameplaySceneName);
    }

    public void SaveGame()
    {
        currentGameStateData = BuildGameStateData();
        SaveService.Save(currentGameStateData);
    }

    ////////////////////
    // EVENT HANDLERS //
    ////////////////////
    
    private void HandleOnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (ui == null) { ui = ServiceManager.Instance.UI; }
        ui.ShowHud();
    }

    /////////////
    // HELPERS //
    /////////////
    

}
