using UnityEngine;

public class PlayerComponentServer : MonoBehaviour
{
    [SerializeField] private PlayerSprite playerSpriteComponent;
    private PlayerStateMachine playerStateMachineComponent;
    private PlayerMovement playerMovementComponent;

    private void Awake()
    {
        playerStateMachineComponent = GetComponent<PlayerStateMachine>();
        playerMovementComponent = GetComponent<PlayerMovement>();
    }

    public PlayerSprite PlayerSpriteComponent { get => playerSpriteComponent; }
    public PlayerStateMachine PlayerStateMachineComponent { get => playerStateMachineComponent; }
    public PlayerMovement PlayerMovementComponent { get => playerMovementComponent; }


}
