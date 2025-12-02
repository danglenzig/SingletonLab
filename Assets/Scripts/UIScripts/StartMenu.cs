using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        UIManager.Instance.ShowHud();
    }
}
