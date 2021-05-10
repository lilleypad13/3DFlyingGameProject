using UnityEngine;
using UnityEngine.UI;

public class UITextCurrentSpeed : MonoBehaviour
{
    private UIManager uiManager;
    private Rigidbody playerRB;
    private Text maxSpeedText;

    private float currentSpeed = 0.0f;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerRB = uiManager.playerRB;
        maxSpeedText = GetComponent<Text>();
    }

    private void Update()
    {
        currentSpeed = playerRB.velocity.magnitude;
        maxSpeedText.text = "Current Speed: " + currentSpeed.ToString("F2");
    }
}
