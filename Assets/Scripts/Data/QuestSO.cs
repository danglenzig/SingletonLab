using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Quests/Quest")]
public class QuestSO : ScriptableObject
{
    [SerializeField] private string questID = string.Empty;
    // ^^copy and paste UUID from www.uuidgenerator.net or similar

    [SerializeField] private string questName = string.Empty;
    [SerializeField] private string questDescription = string.Empty;
    [SerializeField] private EnumQuestStatus defaultStatus = EnumQuestStatus.NOT_STARTED;
    [SerializeField] private List<string> startQuestIDsOnFinished;
    [SerializeField] private List<string> obviateQuestIDsOnFinished;
    [SerializeField] private List<string> obviateQuestIDsOnStarted;

    public string QuestID { get => questID; }
    public string QuestName { get => questName; }
    public string QuestDescription { get => questDescription; }
    public EnumQuestStatus DefaultStatus { get => defaultStatus; }
    public List<string> StartQuestIDsOnFinished {  get => startQuestIDsOnFinished; }
    public List<string> ObviateQuestIDsOnFinished { get => obviateQuestIDsOnFinished; }
    public List<string> ObviateQuestIDsOnStarted {  get => obviateQuestIDsOnStarted; }
}
