using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjectivesView : MonoBehaviour
{
    [SerializeField] private GameObject objectiveObjectPrefab;
    [SerializeField] private GameObject contentParent;
    [SerializeField] private Scrollbar verticalScrollbar;
    [SerializeField] private EnumQuestStatus displayQuestStatus = EnumQuestStatus.STARTED;

    private void OnEnable()
    {
        RefreshContent();
    }

    private void RefreshContent()
    {
        verticalScrollbar.value = 1.0f;
        DisplayQuests(displayQuestStatus);
    }

    private void ClearObjectives()
    {
        Transform parentXForm = contentParent.transform;
        int count = parentXForm.childCount;
        if (count <= 0) return;
        for (int i = count -1; i >= 0; i--)
        {
            DestroyImmediate(parentXForm.GetChild(i).gameObject);
        }
    }

    private void DisplayQuests(EnumQuestStatus status)
    {
        ClearObjectives();
        List<string> questIDS = new List<string>();

        GameStateData gameData = ServiceManager.Instance.Game.CurrentGameStateData;
        foreach (string key in gameData.questStatus.Keys)
        {
            if (gameData.questStatus[key] == status) { questIDS.Add(key); }
        }

        if (questIDS.Count <= 0) return;
        QuestManager qm = ServiceManager.Instance.Quests;
        foreach (string id in questIDS)
        {
            GameObject objGO = Instantiate(objectiveObjectPrefab, contentParent.transform);
            ObjectiveObject obj = objGO.GetComponent<ObjectiveObject>();
            StructQuestData? questData = qm.GetQuestDataByID(id);
            if (questData != null)
            {
                obj.SetObjectiveText(questData.Value.questDescription, (status == EnumQuestStatus.FINISHED));
            }
        }
    }



}
