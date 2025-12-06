using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public enum EnumCanvasName
{
    NONE,
    MAIN_MENU,
    HUD,
    DIALOGUE,
    LOADING,
    PAUSE
}




public class UIDocManager : MonoBehaviour
{

    [SerializeField] private UIDocument mainMenu;
    [SerializeField] private UIDocument hud;
    [SerializeField] private UIDocument dialogue;
    [SerializeField] private UIDocument loading;
    [SerializeField] private UIDocument pause;
    private List<UIDocument> uiDocs = new List<UIDocument>();

    private VisualElement mainMenuRoot;
    private VisualElement hudRoot;
    private VisualElement pauseRoot;
    private VisualElement dialogueRoot;
    private VisualElement loadingRoot;
    private List<VisualElement> veRoots = new List<VisualElement>();

    private EnumCanvasName currentCanvas = EnumCanvasName.NONE;
    private EnumCanvasName previousCanvas = EnumCanvasName.NONE;

    private const int FOCUS_SORT_ORDER = 10;
    private const int UNFOCUSED_SORT_ORDER = 5;
    private const int DISABLED_SORT_ORDER = 0;

    private void Awake()
    {
        uiDocs.Add(mainMenu);
        uiDocs.Add(hud);
        uiDocs.Add(dialogue);
        uiDocs.Add(loading);
        uiDocs.Add(pause);

        mainMenuRoot = mainMenu.rootVisualElement;
        hudRoot = hud.rootVisualElement;
        pauseRoot = pause.rootVisualElement;
        loadingRoot = loading.rootVisualElement;
        dialogueRoot = dialogue.rootVisualElement;
        veRoots.Add(mainMenuRoot);
        veRoots.Add(hudRoot);
        veRoots.Add(pauseRoot);
        veRoots.Add(loadingRoot);
        veRoots.Add(dialogueRoot);

        DisplayCanvas(EnumCanvasName.MAIN_MENU);
    }

    public void DisplayCanvas(EnumCanvasName canv, bool clearFirst = true)
    {
        if (clearFirst) { ClearCanvases(); }
        previousCanvas = currentCanvas;
        currentCanvas = canv;

        UIDocument doc = GetUIDocFromName(canv);
        if (doc != null) { doc.sortingOrder = FOCUS_SORT_ORDER; doc.enabled = true; }

        /*
        VisualElement root = GetVisualElementFromName(canv);
        if(root != null)
        {
            root.SetEnabled(true);
        }
        */
    }
    private void ClearCanvases()
    {
        foreach(UIDocument doc in uiDocs)
        {
            doc.sortingOrder = DISABLED_SORT_ORDER;
            doc.enabled = false;
        }

        /*
        foreach (VisualElement root in veRoots)
        {   
            root.SetEnabled(false);
        }
        */
    }
    private VisualElement GetVisualElementFromName(EnumCanvasName canv)
    {
        switch (canv)
        {
            case EnumCanvasName.MAIN_MENU:
                return mainMenuRoot;
            case EnumCanvasName.DIALOGUE:
                return dialogueRoot;
            case EnumCanvasName.LOADING:
                return loadingRoot;
            case EnumCanvasName.HUD:
                return hudRoot;
            case EnumCanvasName.PAUSE:
                return pauseRoot;
            default: return null;
        }
    }
    private UIDocument GetUIDocFromName(EnumCanvasName canv)
    {
        switch (canv)
        {
            case EnumCanvasName.MAIN_MENU:
                return mainMenu;
            case EnumCanvasName.DIALOGUE:
                return dialogue;
            case EnumCanvasName.LOADING:
                return loading;
            case EnumCanvasName.HUD:
                return hud;
            case EnumCanvasName.PAUSE:
                return pause;
            default: return null;
        }
    }
}
