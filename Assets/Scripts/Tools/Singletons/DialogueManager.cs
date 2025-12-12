using UnityEngine;
using Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private ConversationSOPayloadEvent dialogueTriggeredEvent;
    // Start a convo when hearing this
    // Servialize this into whatever game object is initiating a dialogue

    [SerializeField] private ConversationSOPayloadEvent dialogueFinishedEvent; // Trigger this to indicate the dialogue is finished
    [SerializeField] private StringPayloadEvent         onConversationStartedEvent;
    [SerializeField] private StringPayloadEvent         onConversationEndedEvent;
    [SerializeField] private StringPayloadEvent         onLineRevealStartEvent;
    [SerializeField] private StringPayloadEvent         onLineRevealEndEvent;
    [SerializeField] private StringPayloadEvent         onChoiceTakenEvent;

    private ConversationSO currentConvo = null;


    private void OnEnable()
    {
        dialogueTriggeredEvent.OnEventTriggered +=      HandleDialogueTriggered;
        onConversationStartedEvent.OnEventTriggered +=  HandleOnConversationStartedEvent;
        onConversationEndedEvent.OnEventTriggered +=    HandleOnConversationEndedEvent;
        onLineRevealStartEvent.OnEventTriggered +=      HandleOnLineRevealStartEvent;
        onLineRevealEndEvent.OnEventTriggered +=        HandleOnLineRevealEndEvent;
        onChoiceTakenEvent.OnEventTriggered +=          HandleOnChoiceTakenEvent;

    }

    private void OnDisable()
    {
        dialogueTriggeredEvent.OnEventTriggered -=      HandleDialogueTriggered;
        onConversationStartedEvent.OnEventTriggered -=  HandleOnConversationStartedEvent;
        onConversationEndedEvent.OnEventTriggered -=    HandleOnConversationEndedEvent;
        onLineRevealStartEvent.OnEventTriggered -=      HandleOnLineRevealStartEvent;
        onLineRevealEndEvent.OnEventTriggered -=        HandleOnLineRevealEndEvent;
        onChoiceTakenEvent.OnEventTriggered -=          HandleOnChoiceTakenEvent;
    }


    private void HandleDialogueTriggered(ConversationSO convo)
    {

    }
    private void HandleOnConversationStartedEvent(string eventString)
    {

    }
    private void HandleOnConversationEndedEvent(string eventString)
    {

    }
    private void HandleOnLineRevealStartEvent(string eventString)
    {

    }
    private void HandleOnLineRevealEndEvent(string eventString)
    {

    }
    private void HandleOnChoiceTakenEvent(string eventString)
    {

    }


}
