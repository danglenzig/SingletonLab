using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public void DialogueManagerTest(string speakerName, string lineText)
    {
        Debug.Log($"{speakerName} says: {lineText}");
    }
}
