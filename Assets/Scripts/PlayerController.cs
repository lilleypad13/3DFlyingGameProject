using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float force = 10.0f;
    public float CurrentAcceleration { get => currentAcceleration; }
    private float currentAcceleration;
    public float MaxAcceleration { get => maxAcceleration; }
    private float maxAcceleration;
    public float MaxVelocity { get => maxVelocity; }
    private float maxVelocity;
    public float CurrentVelocity { get => currentVelocity; }
    private float currentVelocity;
    private float lastVelocity;

    private float originalMass;
    [SerializeField] private float brakingMass = 120.0f;

    private float horizontalInput;
    private float verticalInput;
    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalMass = rb.mass;
    }

    private void Update()
    {
        CheckMaxVelocity();
        CheckMaxAcceleration();

        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);

        if (Input.GetButton("Jump"))
        {
            Brake();
        }
        else
            rb.mass = originalMass;
    }

    private void FixedUpdate()
    {
        currentVelocity = Vector3.Magnitude(rb.velocity);
        currentAcceleration = (currentVelocity - lastVelocity) / Time.fixedDeltaTime;

        Vector3 direction = Vector3.Normalize(new Vector3 (horizontalInput, 0.0f, verticalInput));
        rb.AddForce(direction * force);

        lastVelocity = Vector3.Magnitude(rb.velocity);
    }

    private void CheckMaxAcceleration()
    {
        if (currentAcceleration > maxAcceleration)
            maxAcceleration = currentAcceleration;
    }

    private void CheckMaxVelocity()
    {
        float currentVelocity = Vector3.Magnitude(rb.velocity);

        if (currentVelocity > maxVelocity)
            maxVelocity = currentVelocity;
    }

    private void Brake()
    {
        rb.mass = brakingMass;
    }
}
