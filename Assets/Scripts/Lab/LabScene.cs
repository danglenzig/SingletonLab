using UnityEngine;
using Events;



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



    private ServiceManager sm;
    private void Start()
    {
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
    }
    private void OnDisable()
    {
        questFinishedEvent.OnEventTriggered -= HandleOnQuestFinished;
        questStartedEvent.OnEventTriggered -= HandleOnQuestStarted;
    }

    private void HandleOnQuestStarted(string _id)
    {
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
    }

    private void HandleOnQuestFinished(string _id)
    {
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
    }


    public void FinishQuestTestButtonPressed(string _id)
    {
        ServiceManager.Instance.Quests.FinishQuest(_id);
    }

}
