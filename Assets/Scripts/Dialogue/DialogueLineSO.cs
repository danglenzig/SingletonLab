using UnityEngine;
using System.Collections.Generic;

/*
public struct DialogueLineData
{
    public string lineID;
    public string lineName;
    public string speakerName;
    public string[] choiceIDs;
    public bool isQuit;
    public string onLineStartedEventString;
    public string onLineEndedEventString;
    public string lineText;
    public float characterRevealInterval;
    public string portraitTextureResourcePath;
    public bool portraitOnRight;
    public Color speakerNameColor;
    public Color lineTextColor;

    public DialogueLineData
        (
            string _lineID,
            string _lineName,
            string _speakerName,
            string[] _choiceIDs,
            bool _isQuit,
            string _startEventString,
            string _endEventString,
            string _lineText,
            float _charInterval,
            string _portraitTextureResourcePath,
            bool _portraitOnRight,
            Color _speakerNameColor,
            Color _lineTextColor
        )
    {
        lineID = _lineID;
        lineName = _lineName;
        speakerName = _speakerName;
        choiceIDs = _choiceIDs;
        isQuit = _isQuit;
        onLineStartedEventString = _startEventString;
        onLineEndedEventString = _endEventString;
        lineText = _lineText;
        characterRevealInterval = _charInterval;
        portraitTextureResourcePath = _portraitTextureResourcePath;
        portraitOnRight = _portraitOnRight;
        speakerNameColor = _speakerNameColor;
        lineTextColor = _lineTextColor;
    }
}
*/

[CreateAssetMenu(fileName = "DialogueLineSO", menuName = "Dialogue/DialogueLineSO")]
public class DialogueLineSO : ScriptableObject
{
    [SerializeField] private string lineName;
    [SerializeField] private string speakerName = "Namey Nameson";
    [SerializeField] private List<DialogueChoiceSO> choices;
    [SerializeField] private bool isQuit;
    [SerializeField] private string onLineStartedEventString;
    [SerializeField] private string onLineEndedEventString;
    [SerializeField] private string lineText;
    [Range(0.001f, 1.0f)] [SerializeField] private float characterRevealInterval = 0.002f;
    [SerializeField] private string portraitTextureResourcePath = "Resources/...";
    [SerializeField] private bool portraitOnRight;
    [SerializeField] private DialogueLineSO nextLine; // if there are no choices...
    [SerializeField] private Color speakerNameColor = Color.gold;
    [SerializeField] private Color lineTextColor = Color.gold;
    [SerializeField, HideInInspector] private string lineID;


    public string LineName { get => lineName; }
    public string SpeakerName { get => speakerName; }
    public IReadOnlyList<DialogueChoiceSO> Choices { get => choices; }
    public bool IsQuit { get => isQuit; }
    public string OnLineStartedEventString { get => onLineStartedEventString; }
    public string OnLineEndedEventString { get => onLineEndedEventString; }
    public string LineText { get => lineText; }
    public float CharacterRevealInterval { get => characterRevealInterval; }
    public string PortraitTextureResourcePath { get => portraitTextureResourcePath; }
    public bool PortraitOnRight { get => portraitOnRight; }
    public Color SpeakerNameColor { get => speakerNameColor; }
    public Color LineTextColort { get => lineTextColor; }
    public string LineID { get => lineID; }


    private void OnValidate()
    {
        if (string.IsNullOrEmpty(lineID))
        {
            lineID = System.Guid.NewGuid().ToString();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

}
