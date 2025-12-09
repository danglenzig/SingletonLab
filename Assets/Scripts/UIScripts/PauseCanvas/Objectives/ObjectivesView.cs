using UnityEngine;
using UnityEngine.UI;

public class ObjectivesView : MonoBehaviour
{
    [SerializeField] private GameObject objectiveObjectPrefab;
    [SerializeField] private GameObject contentParent;
    [SerializeField] private Scrollbar verticalScrollbar;

    private void OnEnable()
    {
        verticalScrollbar.value = 1.0f;
        // TEMP LOGIC
        ClearObjectives();
        for (int i = 0; i < 6; i++)
        {
            GameObject objGO = Instantiate(objectiveObjectPrefab, contentParent.transform);
        }
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


}
