using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    public Rigidbody playerRB;

    private void Awake()
    {
        playerRB = player.GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>();
    }
}
