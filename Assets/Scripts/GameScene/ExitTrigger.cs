using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    [SerializeField] private string destinationSceneName = string.Empty;
    [SerializeField] private EnumPlayerStart destinationPlayerStart = EnumPlayerStart.DEFAULT;
    [SerializeField] private bool setFlipXOnStart = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        //Debug.Log("FOO");

        GameManager gm = ServiceManager.Instance.Game;
        gm.SetPlayerStart(destinationPlayerStart);
        gm.SetPlayerStartFlipX(setFlipXOnStart);
        gm.FadeToScene(destinationSceneName);
    }

}
