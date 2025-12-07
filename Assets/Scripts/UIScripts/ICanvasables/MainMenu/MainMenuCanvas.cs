using UnityEngine;
using UnityEngine.EventSystems;
using Events;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour, ICanvasable
{

    private enum EnumButtonName
    {
        NEW_GAME,
        CONTINUE,
        QUIT
    }

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

    [Header("Event Channels")]
    [SerializeField] private EmptyPayloadEvent newGamePressedEvent;
    [SerializeField] private EmptyPayloadEvent continuePressedEvent;
    [SerializeField] private EmptyPayloadEvent quitPressedEvent;




    [SerializeField] private bool defaultIsVisible = false;
    private EnumCanvasName canvasName = EnumCanvasName.MAIN_MENU;
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
                GetComponent<Canvas>().enabled = _isVisible;
                continueButton.gameObject.SetActive(SaveService.SaveExists());
            }
        }
    }
    public bool IsVisible { get => isVisible; }

    private void OnEnable()
    {
        newGameButton.onClick.AddListener(() => HandleButtonClick(EnumButtonName.NEW_GAME));
        continueButton.onClick.AddListener(() => HandleButtonClick(EnumButtonName.CONTINUE));
        quitButton.onClick.AddListener(() => HandleButtonClick(EnumButtonName.QUIT));
    }
    private void OnDisable()
    {
        newGameButton.onClick.RemoveAllListeners();
        continueButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }

    ///////////////////////////
    // Button Click Handlers //
    ///////////////////////////
    
    private void HandleButtonClick(EnumButtonName buttonName)
    {
        switch (buttonName)
        {
            case EnumButtonName.NEW_GAME:
                Debug.Log("New Game was pressed");
                newGamePressedEvent.TriggerEvent();
                return;
            case EnumButtonName.CONTINUE:
                Debug.Log("Continue was pressed");
                continuePressedEvent.TriggerEvent();
                return;
            case EnumButtonName.QUIT:
                Debug.Log("Quit was pressed");
                quitPressedEvent.TriggerEvent();
                return;
            default: return;
        }
    }
    /*
    private void OnNewGamePressed()
    {

    }
    private void OnContinuePressed()
    {

    }
    private void OnQuitPressed()
    {

    }
    */

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
