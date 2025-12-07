using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    [SerializeField] private string destinationSceneName = string.Empty;
    [SerializeField] private EnumPlayerStart destinationPlayerStart = EnumPlayerStart.DEFAULT;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("FOO");

        GameManager gm = ServiceManager.Instance.Game;
        gm.SetPlayerStart(destinationPlayerStart);
        gm.FadeToScene(destinationSceneName);
    }

}
