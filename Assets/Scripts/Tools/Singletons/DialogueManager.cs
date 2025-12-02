using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    public void DialogueManagerTest(string speakerName, string lineText)
    {
        Debug.Log($"{speakerName} says: {lineText}");
    }
}
