using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private ServiceManager sm;

    private void Start()
    {
        sm = ServiceManager.Instance;
    }
    public void OnNewGameClicked()
    {
        sm.Game.StartNewGame();
    }
}
