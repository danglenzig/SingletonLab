using UnityEngine;
using TMPro;

public class ObjectiveObject : MonoBehaviour
{
    [SerializeField] private TMP_Text objectiveText;

    private void Start()
    {
        
    }

    public void SetObjectiveText(string txt, bool subdued = false)
    {
        objectiveText.color = Color.gold;
        objectiveText.text = txt;
        if (subdued)
        {
            objectiveText.fontStyle = FontStyles.Strikethrough;
            objectiveText.color = Color.gray;
        }
    }


}
