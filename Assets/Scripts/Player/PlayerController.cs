using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController_Flying))]
public class PlayerController : MonoBehaviour
{
    PlayerController_Flying flyingMovement;

    private float horizontalInput;
    private float verticalInput;

    private bool isPressingAccelerate = false;
    private bool isPressingCharge = false;

    private string driftButtonName = "Fire2";
    private string accelerationButtonName = "Fire1";

    public bool IsInvertedControls
    {
        get => isInvertedControls;
        set
        {
            isInvertedControls = value;
            inversion *= -1.0f;
        }
    }
    [SerializeField] private bool isInvertedControls = false;
    private float inversion = 1.0f;

    private void Awake()
    {
        flyingMovement = GetComponent<PlayerController_Flying>();
    }

    private void Start()
    {
        if (isInvertedControls)
        {
            inversion = -1.0f;
        }
    }

    private void Update()
    {
        ReceivePlayerInput();
    }

    private void ReceivePlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButton(driftButtonName))
        {
            isPressingCharge = true;
        }
        else
        {
            isPressingCharge = false;
        }

        if (Input.GetButton(accelerationButtonName))
        {
            isPressingAccelerate = true;
        }
        else
        {
            isPressingAccelerate = false;
        }
    }

    private void FixedUpdate()
    {
        // Inputs in reverse position for direction vector because that influences which axis that input rotates AROUND
        Vector3 direction = Vector3.Normalize(new Vector3(verticalInput * inversion, horizontalInput, 0.0f));
        flyingMovement.Flying(direction, isPressingCharge, isPressingAccelerate);
    }
}
