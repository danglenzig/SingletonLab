using UnityEngine;

public class ServiceManager : Singleton<ServiceManager>
{
    [SerializeField] private string bootstrapSceneName = "Lab";

    [SerializeField] private GameManager game;
    [SerializeField] private DialogueManager dialogue;
    [SerializeField] private QuestManager quests;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private CanvasManager canvasMgr;
    //...and so on

    public DialogueManager Dialogue { get => dialogue; }
    public GameManager Game { get => game; }
    public QuestManager Quests { get => quests; }
    public InputHandler InputH { get => inputHandler; }
    public CanvasManager CanvasMgr { get => canvasMgr; }
    //...and so on

    public string BootstrapSceneName { get => bootstrapSceneName; }
}
