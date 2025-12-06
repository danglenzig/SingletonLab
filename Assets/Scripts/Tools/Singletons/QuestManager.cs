using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Events;
using Unity.VisualScripting;


public struct StructQuestData
{
    public EnumQuestStatus currentStatus;
    public string questId;
    public string questName;
    public string questDescription;
    public List<string> startQuestIDsOnFinished;

    public List<string> obviateQuestIDsOnFinished;
    public List<string> obviateQuestIDsOnStarted;


    public StructQuestData
        (
            EnumQuestStatus _currentStatus,
            string _questID,
            string _questName,
            string _questDescription,
            List<string> _startQuestIDsOnFinished,
            List<string> _obviateQuestIDsOnFinished,
            List<string> _obviateQuestIDsOnStarted
        )
    {
        currentStatus = _currentStatus;
        questId = _questID;
        questName = _questName;
        questDescription = _questDescription;
        startQuestIDsOnFinished = _startQuestIDsOnFinished;
        obviateQuestIDsOnFinished = _obviateQuestIDsOnFinished;
        obviateQuestIDsOnStarted = _obviateQuestIDsOnStarted;
    }

}

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestSO> quests;

    [Header("Event Channels")]
    [SerializeField] private StringPayloadEvent questStartedEvent;
    [SerializeField] private StringPayloadEvent questFinishedEvent;
    [SerializeField] private StringPayloadEvent questObviatedEvent;

    [SerializeField] private EmptyPayloadEvent questsInitializedEvent;
    [SerializeField] private EmptyPayloadEvent questsLoadedFromSaveEvent;

    private Dictionary<string, EnumQuestStatus> allQuestIDsStatus = new Dictionary<string, EnumQuestStatus>();
    public Dictionary<string, EnumQuestStatus> QuestStatus { get => allQuestIDsStatus; }


    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    public void StartQuest(string _id)
    {
        if (!IsQuestValid(_id)) { return; }
        StructQuestData? data = GetQuestDataByID(_id);

        if (allQuestIDsStatus[_id] != EnumQuestStatus.NOT_STARTED)
        {
            Debug.LogError("Quest must be in a NOT_STARTED state to start"); return;
        }
        allQuestIDsStatus[_id] = EnumQuestStatus.STARTED;
        questStartedEvent.TriggerEvent(_id);


    }
    public void FinishQuest(string _id)
    {
        if (!IsQuestValid(_id)) { return; }
        StructQuestData? data = GetQuestDataByID(_id);

        if (allQuestIDsStatus[_id] != EnumQuestStatus.STARTED)
        {
            Debug.LogError("Quest not started"); return;
        }

        allQuestIDsStatus[_id] = EnumQuestStatus.FINISHED;
        if (data.Value.startQuestIDsOnFinished.Count > 0)
        {
            foreach(string startedQuest in data.Value.startQuestIDsOnFinished)
            {
                StartQuest(startedQuest);
            }
        }
        questFinishedEvent.TriggerEvent(_id);
    }


    public void InitializeQuests()
    {
        allQuestIDsStatus = new Dictionary<string, EnumQuestStatus>();
        foreach (QuestSO quest in quests)
        {
            allQuestIDsStatus[quest.QuestID] = quest.DefaultStatus;
        }
        questsInitializedEvent.TriggerEvent();
    }
    public void LoadSavedQuestData(Dictionary<string, EnumQuestStatus> savedQuestIDsStatus)
    {
        allQuestIDsStatus.Clear();
        allQuestIDsStatus = savedQuestIDsStatus;
        questsLoadedFromSaveEvent.TriggerEvent();
    }



    
    public StructQuestData? GetQuestDataByID(string _id)
    {
        if (!allQuestIDsStatus.Keys.Contains( _id) || GetQuestSOByID(_id) == null)
        {
            Debug.LogWarning($"Invalid quest ID: {_id}");
            return null;
        }
        QuestSO? questSO = GetQuestSOByID(_id);

        StructQuestData data = new StructQuestData
            (
                allQuestIDsStatus[_id],
                questSO.QuestID,
                questSO.QuestName,
                questSO.QuestDescription,
                questSO.StartQuestIDsOnFinished,
                questSO.ObviateQuestIDsOnFinished,
                questSO.ObviateQuestIDsOnStarted

            );
        return data;
    }
    

    private QuestSO? GetQuestSOByID(string _id)
    {
        foreach(QuestSO quest in quests)
        {
            if (quest.QuestID == _id) return quest;
        }
        return null;
    }


private bool IsQuestValid(string _id)
    {
        if (!allQuestIDsStatus.ContainsKey(_id))
        {
            Debug.LogError($"Invalid quest ID {_id}");

            //Debug.Log(allQuestIDsStatus.Keys.Count);

            return false;
        }
        StructQuestData? data = GetQuestDataByID(_id);
        if (data == null) { Debug.LogError($"Invalid quest ID"); return false; }
        return true;
    }

}
