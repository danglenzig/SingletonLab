using UnityEngine;
using System.Collections.Generic;

/*
public struct ConversationData
{
    public string conversationID;
    public string conversationName;
    public string[] lineIDs;
    public string onConversationStartedEventString;
    public string onConversationEndedEventString;

    public ConversationData
        (
            string _convoID,
            string _convoName,
            string[] _lineIDs,
            string _onStartedEventString,
            string _onEndedEventString
        )
    {
        conversationID = _convoID;
        conversationName = _convoName;
        lineIDs = _lineIDs;
        onConversationStartedEventString = _onStartedEventString;
        onConversationEndedEventString = _onEndedEventString;
    }

}
*/


[CreateAssetMenu(fileName = "ConversationSO", menuName = "Dialogue/ConversationSO")]
public class ConversationSO : ScriptableObject
{
    [SerializeField] private string conversationName;
    [SerializeField] private List<DialogueLineSO> lines;
    [SerializeField] private string onConversationStartedEventString;
    [SerializeField] private string onConversationEndedEventString;
    [SerializeField, HideInInspector] private string conversationID = string.Empty;

    public string ConversationName { get => conversationName; }
    public IReadOnlyList<DialogueLineSO> Lines { get => lines; }
    public string OnConversationStartedEventString { get => onConversationStartedEventString; }
    public string OnConversationEndedEventString { get => onConversationEndedEventString; }
    public string ConversationID { get => conversationID; }



    private void OnValidate()
    {
        if (lines.Count < 1) { Debug.LogError("A conversation must have at least one line"); }
        if (string.IsNullOrEmpty(conversationID))
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }


}
