using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour, ICanvasable
{
    [Range(10,1000)][SerializeField] private int steps = 100;
    [SerializeField] private Image blackingPanel;

    [SerializeField] private bool defaultIsVisible = false;
    private EnumCanvasName canvasName = EnumCanvasName.LOADING;
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

    public void FadeIn(float waitTime, float duration, EnumCanvasName toCanv = EnumCanvasName.NONE)
    {
        blackingPanel.color = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(FadeToCanvas(waitTime, duration, toCanv));
    }

    private System.Collections.IEnumerator FadeToCanvas(float waitTime, float duration, EnumCanvasName toCanv)
    {
        duration = Mathf.Clamp(duration, 0.01f, 2.0f);
        yield return new WaitForSeconds(waitTime);
        float inc = duration / ((float)steps);
        for (int i = 0; i < steps; i++)
        {
            float newAlpha = ((float)steps - i) / ((float)steps);
            yield return new WaitForSeconds(inc);
            blackingPanel.color = new Color(0f, 0f, 0f, newAlpha);
        }
        ServiceManager.Instance.CanvasMgr.DisplayCanvas(toCanv);
        blackingPanel.color = new Color(0f, 0f, 0f, 1f);
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
