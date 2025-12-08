using UnityEngine;
using Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseCanvas : MonoBehaviour, ICanvasable
{
    private enum EnumButtonName
    {
        SAVE,
        MAIN
    }

    [SerializeField] private Button saveButton;
    [SerializeField] private Button mainMenuButton;

    [SerializeField] private EmptyPayloadEvent saveGamePressedEvent;
    [SerializeField] private EmptyPayloadEvent gameSavedEvent;

    [SerializeField] private bool defaultIsVisible = false;
    private EnumCanvasName canvasName = EnumCanvasName.PAUSE;
    public EnumCanvasName CanvasName { get => canvasName; }

    private bool _isVisible = true;
    private bool isVisible
    {
        get => _isVisible;
        set
        {
            if (value != _isVisible)
            {
                _isVisible = value;
                gameObject.GetComponent<Canvas>().enabled = _isVisible;
            }
        }
    }
    public bool IsVisible { get => isVisible; }

    private void OnEnable()
    {
        saveButton.onClick.AddListener( () => HandleButtonClick(EnumButtonName.SAVE));
        mainMenuButton.onClick.AddListener(() => HandleButtonClick(EnumButtonName.MAIN));
        gameSavedEvent.OnEventTriggered += HandleOnGameSaved;
    }
    private void OnDisable()
    {
        saveButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
        gameSavedEvent.OnEventTriggered -= HandleOnGameSaved;
    }

    private void HandleButtonClick(EnumButtonName buttName)
    {
        switch (buttName)
        {
            case EnumButtonName.SAVE:
                saveGamePressedEvent.TriggerEvent();
                return;
            case EnumButtonName.MAIN:
                string sceneName = ServiceManager.Instance.BootstrapSceneName;
                SceneManager.LoadScene(sceneName);
                return;
            default: return;
        }
    }
    private void HandleOnGameSaved()
    {
        // visual confirmation feedback
        Debug.Log("Pause menu says: game saved");
    }

    ///////////////////////
    // Interface Methods //
    ///////////////////////

    public EnumCanvasName GetCanvasName()
    {
        return canvasName;
    }
    public Canvas GetCanvas()
    {
        return gameObject.GetComponent<Canvas>();
    }
    public void SetIsVisible(bool val)
    {
        isVisible = val;
    }
}
