using UnityEngine;
using Events;
using System.Collections.Generic;



public class LabScene : MonoBehaviour
{

    [SerializeField] private GameObject testingCanvas;
    [SerializeField] private GameObject finishQuestOneTestButton;
    [SerializeField] private GameObject finishQuestTwoTestButton;
    [SerializeField] private GameObject finishQuestThreeTestButton;
    [SerializeField] private string questOneID;
    [SerializeField] private string questTwoID;
    [SerializeField] private string questThreeID;
    [SerializeField] private StringPayloadEvent questFinishedEvent;
    [SerializeField] private StringPayloadEvent questStartedEvent;

    [SerializeField] private EmptyPayloadEvent saveLoadedEvent;

    private Dictionary<string, GameObject> questButtonDict = new Dictionary<string, GameObject>();



    private ServiceManager sm;
    private void Start()
    {
        questButtonDict[questOneID] = finishQuestOneTestButton;
        questButtonDict[questTwoID] = finishQuestTwoTestButton;
        questButtonDict[questThreeID] = finishQuestThreeTestButton;


        finishQuestOneTestButton.SetActive(false);
        finishQuestTwoTestButton.SetActive(false);
        finishQuestThreeTestButton.SetActive(false);

        testingCanvas.SetActive(false);

        sm = ServiceManager.Instance;
        sm.UI.ShowStartMenu();
    }

    private void OnEnable()
    {
        questFinishedEvent.OnEventTriggered += HandleOnQuestFinished;
        questStartedEvent.OnEventTriggered += HandleOnQuestStarted;
        saveLoadedEvent.OnEventTriggered += SetupTestCanvas;
    }
    private void OnDisable()
    {
        questFinishedEvent.OnEventTriggered -= HandleOnQuestFinished;
        questStartedEvent.OnEventTriggered -= HandleOnQuestStarted;
        saveLoadedEvent.OnEventTriggered -= SetupTestCanvas;
    }

    private void HandleOnQuestStarted(string _id)
    {
        SetupTestCanvas();
        /*
        if (_id == questTwoID)
        {
            finishQuestTwoTestButton.SetActive(true);
            return;
        }
        if (_id == questThreeID)
        {
            finishQuestThreeTestButton.SetActive(true);
            return;
        }
        */
    }

    private void HandleOnQuestFinished(string _id)
    {
        SetupTestCanvas();
        /*
        if (_id == questOneID)
        {
            finishQuestOneTestButton.SetActive(false);
            return;
        }
        if (_id == questTwoID)
        {
            finishQuestTwoTestButton.SetActive(false);
            return;
        }
        if (_id == questThreeID)
        {
            finishQuestThreeTestButton?.SetActive(false);
            return;
        }
        */
    }

    public void SetupTestCanvas()
    {
        GameStateData data = ServiceManager.Instance.Game.CurrentGameStateData;
        foreach (string id in data.questStatus.Keys)
        {
            if (questButtonDict.ContainsKey(id))
            {
                questButtonDict[id].SetActive(data.questStatus[id] == EnumQuestStatus.STARTED);
            }
        }
    }


    public void FinishQuestTestButtonPressed(string _id)
    {
        ServiceManager.Instance.Quests.FinishQuest(_id);
    }

}
