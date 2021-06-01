using UnityEngine;
using UnityEngine.UI;

public class UITextMaxAcceleration : MonoBehaviour
{
    private UIManager uiManager;
    private PlayerController playerController;
    private Text maxAccelerationText;

    private float maxAcceleration = 0.0f;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerController = uiManager.playerController;
        maxAccelerationText = GetComponent<Text>();
    }

    private void Update()
    {
        //maxAcceleration = playerController.MaxAcceleration;
        //maxAccelerationText.text = "Max Acceleration: " + maxAcceleration.ToString("F2");
    }
}
