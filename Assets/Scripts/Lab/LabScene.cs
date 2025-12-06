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

    private ServiceManager sm;

    private void Start()
    {
        
        sm = ServiceManager.Instance;
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
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
