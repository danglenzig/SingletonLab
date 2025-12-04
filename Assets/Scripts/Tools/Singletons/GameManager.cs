using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Timeline.Actions.MenuPriority;


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
    public List<string> inventoryItemIDs;
    public int timePlayedSeconds;
    public Dictionary<string, EnumQuestStatus> questStatus;

    public GameStateData()
    {
        inventoryItemIDs = new List<string>();
        timePlayedSeconds = 0;
        questStatus = new Dictionary<string, EnumQuestStatus>();
    }
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject testingCanvas;

    [SerializeField] private string defaultGameplaySceneName;
    private UIManager ui;
    private QuestManager qm;

    private GameStateData currentGameStateData;

    private void Start()
    {
        ui = ServiceManager.Instance.UI;
        qm = ServiceManager.Instance.Quests;

        if (!GetSavedGameExists())
        {
            qm.InitializeQuests();
            currentGameStateData = BuildGameStateData();
        }
        else
        {
            // TODO...
        }
    }

    private GameStateData BuildGameStateData()
    {
        GameStateData data = new GameStateData();
        data.questStatus = new Dictionary<string, EnumQuestStatus>(qm.QuestStatus);

        // todo get the seconds played from somewhere
        // todo get the current inventory status

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
    

    private bool GetSavedGameExists()
    {
        return false; // <-- placeholder logic
    }

}
