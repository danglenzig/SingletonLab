using UnityEngine;
using UnityEngine.InputSystem;
using Events;

public class InputHandler : MonoBehaviour
{
    private const float DAMP = 0.05f;
    private bool _dampened = false;
    private bool dampened
    {
        get => _dampened;
        set
        {
            if (value == _dampened) return;
            _dampened = value;
            if (_dampened) { StartCoroutine(DampenInput()); }
        }
    }


    private Keyboard myKB;

    [SerializeField] private EmptyPayloadEvent testInputEvent;
    [SerializeField] private EmptyPayloadEvent pauseInputEvent;

    private void Awake()
    {
        myKB = Keyboard.current;
    }


    private void Update()
    {
        if (myKB == null) return;
        if (dampened) return;


        if (myKB.escapeKey.wasPressedThisFrame)
        {
            dampened = true;
            pauseInputEvent.TriggerEvent();
            return;
        }

        if (myKB.spaceKey.wasPressedThisFrame)
        {
            dampened = true;
            testInputEvent.TriggerEvent();
            return;
        }

    }

    private System.Collections.IEnumerator DampenInput()
    {
        yield return new WaitForSeconds(DAMP);
        dampened = false;
    }
}
