using UnityEngine;

public class ServiceManager : Singleton<ServiceManager>
{
    [SerializeField] private GameManager game;
    [SerializeField] private DialogueManager dialogue;
    [SerializeField] private QuestManager quests;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private UIDocManager ui;
    //...and so on

    public UIDocManager UI { get => ui; }
    public DialogueManager Dialogue { get => dialogue; }
    public GameManager Game { get => game; }
    public QuestManager Quests { get => quests; }
    public InputHandler InputH { get => inputHandler; }
    //...and so on
}
