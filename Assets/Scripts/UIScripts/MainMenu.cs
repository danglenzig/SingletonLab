using UnityEngine;
using Events;
using UnityEngine.UIElements;




public class MainMenu : MonoBehaviour
{
    // button names
    const string NEW_GAME = "NewGameButton";

    // UXML things

    private UIDocument uiDoc;
    private VisualElement root;

    [Header("Event Channels")]
    [SerializeField] private EmptyPayloadEvent newGamePressedEvent;


    private void Start()
    {
        uiDoc = GetComponent<UIDocument>();
        root = uiDoc.rootVisualElement;
        
        // set up the buttons
        UQueryBuilder<Button> buttons = root.Query<Button>();
        buttons.ForEach(RegisterButton);
    }

    private void RegisterButton(Button butt)
    {

        Debug.Log(butt.style.display != DisplayStyle.None); // <-- Outputs "True"
        Debug.Log(butt.enabledInHierarchy); // <-- Outputs "True"
        Debug.Log(butt.pickingMode); // <-- Outputs "Position"

        butt.Query<VisualElement>().ForEach(child =>
        {
            Debug.Log($"Child: {child.name} picking={child.pickingMode}");
        });


        //butt.clicked += () => HandleButtonClicked(butt.name);
        butt.RegisterCallback<PointerUpEvent>(HandlePointerUpEvent);

        Debug.Log("FOO");
    }

    private void HandlePointerUpEvent(PointerUpEvent evt)
    {
        Debug.Log("Pointer up: BAR");
    }

    private void HandleButtonClicked(string buttName)
    {
        Debug.Log("Clicked: BAR"); // <-- this still does not happen


        switch (buttName)
        {
            case NEW_GAME:
                newGamePressedEvent.TriggerEvent();
                return;
        }
    }
}
