using UnityEngine;
using UnityEngine.UI;

public class MinigameCanvas : MonoBehaviour, ICanvasable
{

    [SerializeField] private bool defaultIsVisible = false;
    private EnumCanvasName canvasName = EnumCanvasName.MINIGAME;
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
