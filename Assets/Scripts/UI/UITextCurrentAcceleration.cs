using UnityEngine;
using UnityEngine.UI;

public class UITextCurrentAcceleration : MonoBehaviour
{
    private UIManager uiManager;
    private PlayerController playerController;
    private Text currentAccelerationText;

    private float currentAccceleration = 0.0f;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerController = uiManager.playerController;
        currentAccelerationText = GetComponent<Text>();
    }

    private void Update()
    {
        currentAccceleration = playerController.CurrentAcceleration;
        currentAccelerationText.text = "Current Acceleration: " + currentAccceleration.ToString("F2");
    }
}
