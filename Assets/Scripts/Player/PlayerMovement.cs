using UnityEngine;
using Constants;
using Events;

public class PlayerMovement : MonoBehaviour
{

    private const float BLOCKER_DETECT_RADIUS = 0.5f;

    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private EmptyPayloadEvent fadeCompleteEvent;

    private Collider2D myCollider;
    private Vector2 moveInput = Vector2.zero;

    private bool blockedRight = false;
    private bool blockedLeft = false;
    private bool blockedUp = false;
    private bool blockedDown = false;
    private bool canMove = false;

    private Rigidbody2D myRB;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        myRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //moveInputEvent.OnEventTriggered += IngestMoveInput;
        fadeCompleteEvent.OnEventTriggered += HandleOnFadeComplete;
    }
    private void OnDisable()
    {
        //moveInputEvent.OnEventTriggered -= IngestMoveInput;
        fadeCompleteEvent.OnEventTriggered -= HandleOnFadeComplete;
    }

    private void Update()
    {
        if (!canMove) return;

        // this works, but...ugh.
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, BLOCKER_DETECT_RADIUS);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, BLOCKER_DETECT_RADIUS);
        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, BLOCKER_DETECT_RADIUS);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector2.down, BLOCKER_DETECT_RADIUS);

        blockedRight = rightHit.collider != null && rightHit.collider.CompareTag(Tags.TAG_PLAYER_BLOCKER);
        if (blockedRight) { moveInput.x = Mathf.Clamp(moveInput.x, -1f, 0f); }
        blockedLeft = leftHit.collider != null && leftHit.collider.CompareTag(Tags.TAG_PLAYER_BLOCKER);
        if (blockedLeft) { moveInput.x = Mathf.Clamp(moveInput.x, 0f, 1f); }
        blockedUp = upHit.collider != null && upHit.collider.CompareTag(Tags.TAG_PLAYER_BLOCKER);
        if (blockedUp) { moveInput.y = Mathf.Clamp(moveInput.y, -1f, 0f); }
        blockedDown = downHit.collider != null && downHit.collider.CompareTag(Tags.TAG_PLAYER_BLOCKER);
        if (blockedDown) { moveInput.y = Mathf.Clamp(moveInput.y, 0f, 1f); }

        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }
    public void ParkedStateToggled(bool isParked)
    {
        myRB.simulated = !isParked;
    }
    public void SetMoveInput(Vector2 _moveInput)
    {
        moveInput = _moveInput;
    }

    private void HandleOnFadeComplete()
    {
        canMove = true;
    }

}
