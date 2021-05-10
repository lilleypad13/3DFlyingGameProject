using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private PlayerController_Flying controller;
    [SerializeField] private GameObject cam;

    [SerializeField] private Vector3 baseOffset = new Vector3(0.0f, 9.0f, -10.0f);
    [SerializeField] private float maxSpeedExtraOffset = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    private Vector3 originalCameraLocalPosition;

    private Vector3 cameraForwardRelativeToPlayerForward;

    private void Awake()
    {
        controller = player.GetComponent<PlayerController_Flying>();
        baseOffset = transform.position - player.transform.position;
    }

    private void Start()
    {
        originalCameraLocalPosition = cam.transform.localPosition;
        cameraForwardRelativeToPlayerForward = transform.forward - player.transform.forward;
    }

    private void Update()
    {
        CameraZoom();
        Follow();
        RotateView();
    }

    private void Follow()
    {
        transform.position = 
            player.transform.position + 
            baseOffset;
    }

    private void RotateView()
    {
        Vector3 lookDirection = new Vector3(player.transform.forward.x, 0.0f, player.transform.forward.z);

        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }

    private void CameraZoom()
    {
        cam.transform.localPosition = 
            originalCameraLocalPosition + 
            -cam.transform.InverseTransformDirection(cam.transform.forward) * maxSpeedExtraOffset * DetermineExtraOffsetFromPlayerSpeed();
    }

    private float DetermineExtraOffsetFromPlayerSpeed()
    {
        float offsetPercentage = controller.MovementSpeed / controller.MaxBaseSpeed;

        if(offsetPercentage > 1.0f)
        {
            offsetPercentage = 1.0f;
        }
        else
        {
            offsetPercentage = Mathf.Clamp(offsetPercentage, 0.0f, 0.8f);
        }

        return offsetPercentage;
    }

}
