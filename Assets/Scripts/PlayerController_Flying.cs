using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Flying : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 20.0f;
    public float MaxBaseSpeed { get => maxBaseSpeed; }
    [SerializeField] private float maxBaseSpeed = 30.0f;
    [SerializeField] private float extraTurboSpeed = 20.0f;
    public float MovementSpeed { get => movementSpeed; }
    private float movementSpeed;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float driftChargeRate = 10.0f;
    public float ChargeValue { get => chargeValue; }
    private float chargeValue = 0.0f;
    public float MaxChargeValue { get => maxChargeValue; }
    private float maxChargeValue = 20.0f;

    private bool isAccelerating = false;
    private bool isTurbo = false;

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
    private float horizontalInput;
    private float verticalInput;

    private bool isPressingAccelerate = false;
    private bool isPressingCharge = false;

    private string driftButtonName = "Fire2";
    private string accelerationButtonName = "Fire1";

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

    private void ReceivePlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Inputs in reverse position for direction vector because that influences which axis that input rotates AROUND
        Vector3 direction = Vector3.Normalize(new Vector3(verticalInput * inversion, horizontalInput, 0.0f));
        Flying(direction);
    }

    private void Flying(Vector3 dir)
    {
        RotatePlayer(dir);
        MovePlayer(dir);
    }

    private void RotatePlayer(Vector3 dir)
    {
        transform.Rotate(dir.y * Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void MovePlayer(Vector3 dir)
    {
        if (isPressingCharge)
        {
            isAccelerating = false;
            chargeValue += driftChargeRate * Time.deltaTime;
        }
        else
        {
            if (chargeValue >= maxChargeValue)
            {
                isTurbo = true;
            }
            chargeValue -= driftChargeRate * Time.deltaTime;
        }

        chargeValue = Mathf.Clamp(chargeValue, 0.0f, maxChargeValue);




        if (isPressingAccelerate && !isPressingCharge)
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
        }

        if (isAccelerating && movementSpeed < maxBaseSpeed)
        {
            movementSpeed += acceleration * Time.deltaTime;
            movementSpeed = Mathf.Clamp(movementSpeed, 0.0f, maxBaseSpeed);
        }
        else
        {
            movementSpeed -= acceleration * Time.deltaTime;
            if(movementSpeed < 0.0f)
            {
                movementSpeed = 0.0f;
            }
        }

        // movementSpeed = some base speed value + turbo (or 0)
        if (isTurbo)
        {
            Debug.Log("Entered Turbo Speed.");
            movementSpeed = maxBaseSpeed + extraTurboSpeed;
            isTurbo = false;
            chargeValue = 0.0f;
        }

        DetermineMovement(dir);
    }

    private void DetermineMovement(Vector3 dir)
    {
        Vector3 movementDirection = Vector3.Normalize(transform.forward + dir.x * Vector3.up);
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }

    private void ApplyTurbo()
    {
        
    }

    private float CheckSpeedBelowZero(float speed)
    {
        if(speed < 0.0f)
        {
            speed = 0.0f;
        }

        return speed;
    }

}
