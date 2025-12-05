// https://docs.unity3d.com/6000.0/Documentation/Manual/UIE-simple-ui-toolkit-workflow.html
// https://docs.unity3d.com/6000.0/Documentation/Manual/UIE-get-started-with-runtime-ui.html

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleCustomEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/SimpleCustomEditor")]
    public static void ShowExample()
    {
        SimpleCustomEditor wnd = GetWindow<SimpleCustomEditor>();
        wnd.titleContent = new GUIContent("SimpleCustomEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("These were created from code");
        root.Add(label);

        Button button3 = new Button();
        button3.name = "button3";
        button3.text = $"This is {button3.name}";
        root.Add(button3);

        Toggle toggle3 = new Toggle();
        toggle3.name = "toggle3";
        toggle3.label = $"NOWMBA????";
        root.Add(toggle3);


        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        // Import UXML created manually.
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/SimpleCustomEditor_uxml.uxml");
        VisualElement _labelFromUXML = visualTree.Instantiate();
        root.Add(_labelFromUXML);


        // call the event handler
        SetupButtonHandler();
        SetupToggleHandler();
    }

    private void SetupButtonHandler()
    {
        VisualElement root = rootVisualElement;
        UQueryBuilder<Button> buttons = root.Query<Button>();
        buttons.ForEach(RegisterButtonHandler);
    }
    private void SetupToggleHandler()
    {
        VisualElement root = rootVisualElement;
        UQueryBuilder<Toggle> toggles = root.Query<Toggle>();
        toggles.ForEach(RegisterToggleHandler);
    }

    private void RegisterButtonHandler(Button butt)
    {
        butt.RegisterCallback<ClickEvent>(OnClickBehavior);
    }

    private void RegisterToggleHandler(Toggle togg)
    {
        togg.RegisterCallback<ClickEvent>(OnToggleBehavior);
    }

    private void OnClickBehavior(ClickEvent evt)
    {
        Button butt = evt.target as Button;
        string buttonName = butt.name;

        Debug.Log($"CLICK on {buttonName}");
    }

    private void OnToggleBehavior(ClickEvent evt)
    {
        Toggle togg = evt.target as Toggle;
        string toggleName = togg.name;
        bool toggVal = togg.value;

        Debug.Log($"Toggle {toggleName} value is {toggVal.ToString()}");
    }

}
