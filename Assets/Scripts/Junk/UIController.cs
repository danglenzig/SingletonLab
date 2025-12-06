using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private float incrementAmount = 5.0f;

    private UIDocument uIDocument;
    private VisualElement root;
    private Label titleLabel;
    private Label messageLabel;
    private Button testButton;
    private Toggle testToggle;
    private ProgressBar testProgBar;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        UQueryBuilder<Label> labels = root.Query<Label>();
        UQueryBuilder<Button> buttons = root.Query<Button>();
        UQueryBuilder<ProgressBar> progBars = root.Query<ProgressBar>();

        labels.ForEach(SetupLabels);
        buttons.ForEach(SetupButtons);
        progBars.ForEach(SetupProgBars);
    }

    private void Start()
    {
        if (titleLabel == null) return;
        if (messageLabel == null) return;

        titleLabel.text = "Title text set at runtime with C# code";
        messageLabel.text = "This is the message text";
    }

    private void SetupLabels(Label label)
    {
        switch (label.name)
        {
            case "titleLabel":
                titleLabel = label;
                break;
            case "messageLabel":
                messageLabel = label;
                break;
            default: break;
        }
    }

    private void SetupButtons(Button button)
    {
        button.RegisterCallback<ClickEvent>(HandleButtonClick);
        switch (button.name)
        {
            case "testButton":
                testButton = button;
                testButton.name = button.name;
                break;
            default: break;
        }
    }

    private void SetupProgBars(ProgressBar progressBar)
    {
        switch(progressBar.name)
        {
            case "testProgBar":
                testProgBar = progressBar;
                break;
            default: break;
        }
    }

    private void HandleButtonClick(ClickEvent evt)
    {
        Button butt = evt.target as Button;
        if (butt == null) return;
        switch (butt.name)
        {
            case "testButton":
                //Debug.Log("Test button clicked");
                DisplayMessage($"{butt.name} clicked. Here's some randomness: {System.Guid.NewGuid().ToString()}");

                if (testProgBar != null)
                {
                    float max = testProgBar.highValue;
                    float val = testProgBar.value;
                    if (val + incrementAmount <= max) { testProgBar.value = val + incrementAmount; }
                    else { testProgBar.value = max; }
                }
                break;
            default: break;
        }
    }

    private void DisplayMessage(string msgStr)
    {
        if (messageLabel == null) return;
        messageLabel.text = msgStr;
    }

    

}
