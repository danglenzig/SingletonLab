using UnityEngine;
using Events;
using StateMachine;

public class PlayerStateConstants
{
    public const string PARKED_STATE = "PARKED";
    public const string IDLE_STATE = "IDLE";
    public const string MOVING_STATE = "MOVING";

    public const string TO_PARKED_TRANSITION = "TO_PARKED";
    public const string TO_IDLE_TRANSITION = "TO_IDLE";
    public const string TO_MOVING_TRANSITION = "TO_MOVING";
}


public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private SimpleStateMachineSO stateMachine;
    [SerializeField] private Vector2PayloadEvent moveInputEvent;
    //[SerializeField] private EmptyPayloadEvent pauseInputEvent;
    [SerializeField] private BoolPayloadEvent gameIsPausedEvent;

    private PlayerMovement playerMovement;
    public StateData CurrentPlayerStateData { get => stateMachine.CurrentStateData; }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        moveInputEvent.OnEventTriggered += HandleOnMoveInput;
        gameIsPausedEvent.OnEventTriggered += HandleOnPausedChanged;
        foreach(StateSO state in stateMachine.GetStates())
        {
            state.OnStateEntered += HandleOnStateEntered;
            state.OnStateExited += HandleOnStateExited;
        }
    }
    private void OnDisable()
    {
        moveInputEvent.OnEventTriggered -= HandleOnMoveInput;
        gameIsPausedEvent.OnEventTriggered -= HandleOnPausedChanged;
        foreach (StateSO state in stateMachine.GetStates())
        {
            state.OnStateEntered -= HandleOnStateEntered;
            state.OnStateExited -= HandleOnStateExited;
        }
    }

    private void Start()
    {
        stateMachine.Initialize();
        stateMachine.TriggerTransition(PlayerStateConstants.TO_IDLE_TRANSITION);
    }

    private void HandleOnParkedEntered()
    {

    }
    private void HandleOnIdleEntered()
    {

    }
    private void HandleOnMovingEntered()
    {

    }
    
    private void HandleOnPausedChanged(bool isPaused)
    {
        if (isPaused)
        {
            stateMachine.TriggerTransition(PlayerStateConstants.TO_PARKED_TRANSITION); return;
        }
        else
        {
            stateMachine.TriggerTransition(PlayerStateConstants.TO_IDLE_TRANSITION); return;
        }
    }
    private void HandleOnMoveInput(Vector2 _moveInput)
    {
        switch (stateMachine.CurrentStateData.StateName)
        {
            case PlayerStateConstants.PARKED_STATE:
                return;
            case PlayerStateConstants.IDLE_STATE:
                if (_moveInput.sqrMagnitude > Mathf.Epsilon)
                {
                    playerMovement.SetMoveInput(_moveInput);
                    stateMachine.TriggerTransition(PlayerStateConstants.TO_MOVING_TRANSITION);
                    return;
                }
                return;

            case PlayerStateConstants.MOVING_STATE:
                playerMovement.SetMoveInput(_moveInput);
                if (_moveInput.sqrMagnitude <= Mathf.Epsilon)
                {
                    stateMachine.TriggerTransition(PlayerStateConstants.TO_IDLE_TRANSITION);
                    return;
                }
                return;
        }
    }

    private void HandleOnStateEntered(StateData stateData)
    {
        switch (stateData.StateName)
        {
            case PlayerStateConstants.PARKED_STATE:
                playerMovement.ParkedStateToggled(true);
                break;
            case PlayerStateConstants.IDLE_STATE:
                break;
            case PlayerStateConstants.MOVING_STATE:
                break;
        }
    }
    private void HandleOnStateExited(StateData stateData)
    {
        switch (stateData.StateName)
        {
            case PlayerStateConstants.PARKED_STATE:
                playerMovement.ParkedStateToggled(false);
                break;
            case PlayerStateConstants.IDLE_STATE:
                break;
            case PlayerStateConstants.MOVING_STATE:
                break;
        }
    }


}
