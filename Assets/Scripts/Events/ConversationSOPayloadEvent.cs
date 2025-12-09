using UnityEngine;
namespace Events
{
    [CreateAssetMenu(fileName = "ConversationSOPayloadEvent", menuName = "Event Channels/ConversationSOPayloadEvent")]
    public class ConversationSOPayloadEvent : ScriptableObject
    {
        public event System.Action<ConversationSO> OnEventTriggered;
        public void TriggerEvent(ConversationSO payload)
        {
            OnEventTriggered?.Invoke(payload);
        }
    }
}


