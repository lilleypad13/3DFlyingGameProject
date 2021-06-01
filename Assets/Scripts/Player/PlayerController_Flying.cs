using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileController))]
public class PlayerController_Flying : MonoBehaviour
{
    ProjectileController projectileController;

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

    private void Awake()
    {
        projectileController = GetComponent<ProjectileController>();
    }

    public void Flying(Vector3 dir, bool isPressingCharge, bool isPressingAccelerate)
    {
        RotatePlayer(dir);
        MovePlayer(dir, isPressingCharge, isPressingAccelerate);
    }

    private void RotatePlayer(Vector3 dir)
    {
        transform.Rotate(dir.y * Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void MovePlayer(Vector3 dir, bool isPressingCharge, bool isPressingAccelerate)
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

        if (isTurbo)
        {
            EnteredTurboState();
        }

        DetermineMovement(dir);
    }

    private void EnteredTurboState()
    {
        Debug.Log("Entered Turbo Speed.");
        movementSpeed = maxBaseSpeed + extraTurboSpeed;
        isTurbo = false;
        chargeValue = 0.0f;

        projectileController.FireProjectile();
    }

    private void DetermineMovement(Vector3 dir)
    {
        Vector3 movementDirection = Vector3.Normalize(transform.forward + dir.x * Vector3.up);
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }

}
