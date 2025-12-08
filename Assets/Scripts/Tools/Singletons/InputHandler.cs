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

    private Vector2 _moveInput = Vector2.zero;
    private Vector2 moveInput
    {
        get => _moveInput;
        set
        {
            if (value == _moveInput) return;
            _moveInput = value;
            moveInputEvent.TriggerEvent(_moveInput);
        }
    }


    private Keyboard myKB;

    [SerializeField] private EmptyPayloadEvent testInputEvent;
    [SerializeField] private EmptyPayloadEvent pauseInputEvent;
    [SerializeField] private Vector2PayloadEvent moveInputEvent;

    private void Awake()
    {
        myKB = Keyboard.current;
    }

    
    
    private void Update()
    {
        // move input

        float moveX = 0f;
        float moveY = 0f;

        if(myKB.dKey.isPressed) { moveX += 1f; }
        if(myKB.aKey.isPressed) { moveX -= 1f; }
        if(myKB.wKey.isPressed) { moveY += 1f; }
        if(myKB.sKey.isPressed) { moveY -= 1f; }
        moveInput = new Vector2(moveX, moveY).normalized;


        // pressed inputs

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
