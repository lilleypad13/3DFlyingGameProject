using UnityEngine;
using UnityEngine.UI;

public class UITextMaxSpeed : MonoBehaviour
{
    private UIManager uiManager;
    private Rigidbody playerRB;
    private Text maxSpeedText;

    private float maxSpeed = 0.0f;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerRB = uiManager.playerRB;
        maxSpeedText = GetComponent<Text>();
    }

    private void Update()
    {
        if (playerRB.velocity.magnitude >= maxSpeed)
        {
            maxSpeed = playerRB.velocity.magnitude;
            maxSpeedText.text = "Max Speed: " + maxSpeed.ToString("F2");
        }
    }
}
