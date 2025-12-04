using UnityEngine;
using UnityEngine.InputSystem;
using Events;

public class InputHandler : MonoBehaviour
{
    private const float DAMP = 0.05f;
    private bool dampened = false;
    private Keyboard myKB;

    [SerializeField] private EmptyPayloadEvent testInputEvent;

    private void Awake()
    {
        myKB = Keyboard.current;
    }


    private void Update()
    {
        if (myKB == null) return;
        if (dampened) return;
        if (myKB.spaceKey.wasPressedThisFrame)
        {
            dampened = true;
            testInputEvent.TriggerEvent();
            StartCoroutine(DampenInput());
        }

    }

    private System.Collections.IEnumerator DampenInput()
    {
        yield return new WaitForSeconds(DAMP);
        dampened = false;
    }
}
