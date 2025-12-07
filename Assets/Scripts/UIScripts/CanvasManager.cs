using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Constants;

public enum EnumCanvasName
{
    NONE,
    MAIN_MENU,
    HUD,
    DIALOGUE,
    LOADING,
    PAUSE,
    MINIGAME
}

public interface ICanvasable
{
    Canvas GetCanvas();
    EnumCanvasName GetCanvasName();
    void SetIsVisible(bool val);
}

public class CanvasManager : MonoBehaviour
{
    private const int HISTORY_SIZE = 10;
    private const int ACTIVE_CANVAS_SORT_ORDER = 10;
    private const int INACTIVE_VISIBLE_SORT_ORDER = 5;
    private const int INVISIBLE_SORT_ORDER = 0;

    [SerializeField] private Vector2 refRes;
    [SerializeField] private EventSystem eventSystem;

    [Header("Canvases")]
    [SerializeField] private MainMenuCanvas mainMenuCanvas;
    [SerializeField] private HUDCanvas hudCanvas;
    [SerializeField] private LoadingCanvas loadingCanvas;
    [SerializeField] private PauseCanvas pauseCanvas;
    [SerializeField] private DialogeCanvas dialogueCanvas;
    [SerializeField] private MinigameCanvas minigameCanvas;

    private EnumCanvasName currentActiveCanvas = EnumCanvasName.NONE;

    private List<ICanvasable> canvases = new List<ICanvasable>();
    private List<EnumCanvasName> activeCanvasHistory = new List<EnumCanvasName>();

    private void Awake()
    {
        // There can be only one!
        eventSystem.tag = Constants.Tags.TAG_EVENT_SYSTEM;
        EventSystem[] eventSystems =  GameObject.FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        foreach(EventSystem es in eventSystems)
        {
            if (!es.CompareTag(Constants.Tags.TAG_EVENT_SYSTEM)) { Destroy(es.gameObject); }
        }
        canvases.Add(mainMenuCanvas);
        canvases.Add(hudCanvas);
        canvases.Add(loadingCanvas);
        canvases.Add(pauseCanvas);
        canvases.Add(dialogueCanvas);
        canvases.Add(minigameCanvas);
        InitializeCanvases();
    }

    private void InitializeCanvases()
    {
        foreach(ICanvasable canv in canvases)
        {
            canv.SetIsVisible(true);
            canv.SetIsVisible(false);
            //Canvas _canv = canv.GetCanvas();
            canv.GetCanvas().sortingOrder = INVISIBLE_SORT_ORDER;
            CanvasScaler canvScaler = canv.GetCanvas().GetComponent<CanvasScaler>();
            canvScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvScaler.referenceResolution = new Vector2(refRes.x, refRes.y);
            canvScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvScaler.matchWidthOrHeight = 0.5f;
        }
    }

    public void FadeIn(EnumCanvasName toCanv = EnumCanvasName.NONE)
    {

        LoadingCanvas loadCanv = GetCanvasWithName(EnumCanvasName.LOADING) as LoadingCanvas;
    }


    public void DisplayCanvas(EnumCanvasName canvasName, bool clearFirst = true)
    {
        if (canvasName == EnumCanvasName.NONE) { ClearAllCanvases(); return; }

        if (currentActiveCanvas == canvasName) { Debug.LogWarning($"{canvasName.ToString()} is already the active canvas"); return; }
        if (clearFirst) { ClearAllCanvases(); }
        foreach(ICanvasable canv in canvases)
        {
            if (canv.GetCanvas().enabled) { canv.GetCanvas().sortingOrder = INACTIVE_VISIBLE_SORT_ORDER; }
        }
        ICanvasable newActiveIC = GetCanvasWithName(canvasName);
        newActiveIC.SetIsVisible(true);
        newActiveIC.GetCanvas().sortingOrder = ACTIVE_CANVAS_SORT_ORDER;

        activeCanvasHistory.Add(currentActiveCanvas);
        GroomCanvasHistory();
        currentActiveCanvas = canvasName;
    }

    private ICanvasable GetCanvasWithName(EnumCanvasName _name)
    {
        foreach(ICanvasable canv in canvases)
        {
            if (canv.GetCanvasName() == _name) { return canv; }
        }
        return null;
    }
    public ICanvasable GetIcanvasable(EnumCanvasName _name)
    {
        return GetCanvasWithName(_name);
    }

    private void ClearAllCanvases()
    {
        foreach(ICanvasable canv in canvases)
        {
            canv.SetIsVisible(false);
            canv.GetCanvas().sortingOrder = INVISIBLE_SORT_ORDER;
        }
    }

    private void GroomCanvasHistory()
    {
        if (activeCanvasHistory.Count <= HISTORY_SIZE) return;
        activeCanvasHistory.Remove(activeCanvasHistory[0]);
        Debug.Log($"Canvas history size: {activeCanvasHistory.Count}");
    }
}
