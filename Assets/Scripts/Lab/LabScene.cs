using UnityEngine;
using Events;
using System.Collections.Generic;



public class LabScene : MonoBehaviour
{

    [SerializeField] private string questOneID;
    [SerializeField] private string questTwoID;
    [SerializeField] private string questThreeID;
    [SerializeField] private StringPayloadEvent questFinishedEvent;
    [SerializeField] private StringPayloadEvent questStartedEvent;

    //[SerializeField] private EmptyPayloadEvent saveLoadedEvent;

    private Dictionary<string, GameObject> questButtonDict = new Dictionary<string, GameObject>();



    private ServiceManager sm;
    private void Start()
    {
        
        sm = ServiceManager.Instance;
        UIDocManager ui = sm.UI;
        // Show the Main Menu


    }

    private void OnEnable()
    {
        questFinishedEvent.OnEventTriggered += HandleOnQuestFinished;
        questStartedEvent.OnEventTriggered += HandleOnQuestStarted;
        //saveLoadedEvent.OnEventTriggered += HandleOnSaveLoaded;
    }
    private void OnDisable()
    {
        questFinishedEvent.OnEventTriggered -= HandleOnQuestFinished;
        questStartedEvent.OnEventTriggered -= HandleOnQuestStarted;
        //saveLoadedEvent.OnEventTriggered -= HandleOnSaveLoaded;
    }

    private void HandleOnSaveLoaded()
    {

    }

    
    private void HandleOnQuestStarted(string _id)
    {
        
    }

    private void HandleOnQuestFinished(string _id)
    {
        
    }

}
