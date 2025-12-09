using UnityEngine;
using System.Collections.Generic;
/*
public struct DialogueChoiceData
{
    public string choiceID;
    public string choiceName;
    public string targetLineID;
    public bool isQuit;
    public string onChoiceTakenEventString;
    public string choiceButtonText;
    public Color choiceButtonColor;
    public Color choiceTextColor;

    public DialogueChoiceData
        (
            string _choiceID,
            string _choiceName,
            string _targetLineID,
            bool _isQuit,
            string _eventString,
            string _buttonText,
            Color _buttonColor,
            Color _textColor
        )
    {
        choiceID = _choiceID;
        choiceName = _choiceName;
        targetLineID = _targetLineID;
        isQuit = _isQuit;
        onChoiceTakenEventString = _eventString;
        choiceButtonText = _buttonText;
        choiceButtonColor = _buttonColor;
        choiceTextColor = _textColor;
    }

}
*/

[CreateAssetMenu(fileName = "DialogueChoiceSO", menuName = "Dialogue/DialogueChoiceSO")]
public class DialogueChoiceSO : ScriptableObject
{
    [SerializeField] private string choiceName;
    [SerializeField] private DialogueLineSO targetLine;
    [SerializeField] private bool isQuit;
    [SerializeField] private string onChoiceTakenEventString;
    [SerializeField] private string choiceButtonText;
    [SerializeField] private Color choiceButtonColor = Color.grey;
    [SerializeField] private Color choiceTextColor = Color.gold;
    [SerializeField, HideInInspector] private string choiceID;

    public string ChoiceName { get => choiceName; }
    public DialogueLineSO TargetLine { get => targetLine; }
    public bool IsQuit { get => isQuit; }
    public string OnChoiceTakenEventString { get => onChoiceTakenEventString; }
    public string ChoiceButtonText { get => choiceButtonText; }
    public Color ChoiceButtonColor { get => choiceButtonColor; }
    public Color ChoiceTextColort { get => choiceTextColor; }
    public string ChoiceID { get => choiceID; }

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(choiceID))
        {
            choiceID = System.Guid.NewGuid().ToString();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}
