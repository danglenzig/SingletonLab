using UnityEngine;


public enum EnumPlayerStart
{
    DEFAULT,
    NORTH,
    SOUTH,
    EAST,
    WEST
}



public class GameScene : MonoBehaviour
{
    [SerializeField] private Transform defaultPlayerStart;
    [SerializeField] private Transform northPlayerStart;
    [SerializeField] private Transform southPlayerStart;
    [SerializeField] private Transform eastPlayerStart;
    [SerializeField] private Transform westPlayerStart;

    private GameObject player = null;


    public void SpawnPlayerAtPlayerStart(GameObject playerPrefab, EnumPlayerStart playerStart = EnumPlayerStart.DEFAULT, bool flipX = false)
    {
        player = Instantiate(playerPrefab);
        PlayerSprite playerSprite = player.GetComponent<PlayerComponentServer>().PlayerSpriteComponent;
        playerSprite.SetFlipX(flipX);
        
        switch (playerStart)
        {
            case EnumPlayerStart.DEFAULT:
                player.transform.position = defaultPlayerStart.position;
                return;
            case EnumPlayerStart.NORTH:
                player.transform.position = northPlayerStart.position;
                return;
            case EnumPlayerStart.SOUTH:
                player.transform.position = southPlayerStart.position;
                return;
            case EnumPlayerStart.EAST:
                player.transform.position = eastPlayerStart.position;
                return;
            case EnumPlayerStart.WEST:
                player.transform.position = westPlayerStart.position;
                return;
        }
    }
}
