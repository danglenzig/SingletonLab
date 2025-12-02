using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private Canvas startMenu;
    [SerializeField] private Canvas hud;
    [SerializeField] private bool verbose = false;

    public Canvas DialogueCanvas { get => dialogueCanvas; }
    public Canvas LoadingScreen { get => LoadingScreen; }
    public Canvas StartMenu { get => startMenu; }
    public Canvas Hud { get => hud; }

    private List<Canvas> canvasList = new List<Canvas>();
    
    Canvas focusedCanvas = null;
    Canvas previousFocusedCanvas = null;

    /////////
    // API //
    /////////
    
    public void ShowStartMenu(bool clearFirst = true) { DisplayCanvas(startMenu, clearFirst); }
    public void ShowLoadingScreen(bool clearFirst = true) { DisplayCanvas(loadingScreen, clearFirst); }
    public void ShowDialogueCanvas(bool clearFirst = true) { DisplayCanvas(dialogueCanvas, clearFirst); }
    public void ShowHud(bool clearFirst = true) { DisplayCanvas(hud, clearFirst); }

    ///////////////////
    // Private Logic //
    ///////////////////

    private void DisplayCanvas(Canvas canvasToDisplay, bool clearFirst)
    {
        if (canvasList.Count <= 0) { BuildCanvasList(); }
        if (clearFirst) { ClearCanvases(); }
        if (focusedCanvas == canvasToDisplay) { Debug.LogWarning($"{canvasToDisplay.gameObject.name} is already displayed"); return; }

        previousFocusedCanvas = focusedCanvas;
        foreach (Canvas _canv in canvasList)
        {
            if (_canv != canvasToDisplay) { _canv.sortingOrder = 0; }
        }
        focusedCanvas = canvasToDisplay;
        canvasToDisplay.sortingOrder = 10;
        canvasToDisplay.enabled = true;

        // debugging logic
        if (verbose)
        {
            string previousName = string.Empty;
            string currentName = string.Empty;
            currentName = canvasToDisplay.gameObject.name;
            if (previousFocusedCanvas == null) { previousName = "None"; }
            else { previousName = previousFocusedCanvas.gameObject.name; }
            Debug.Log($"Previous focus: {previousName}\nCurrent focus: {currentName}");
        }
    }

    private void BuildCanvasList()
    {
        canvasList.Add(dialogueCanvas);
        canvasList.Add(loadingScreen);
        canvasList.Add(startMenu);
        canvasList.Add(hud);
    }

    private void ClearCanvases()
    {
        foreach (Canvas _canv in canvasList)
        {
            _canv.enabled = false;
        }
    }
}
