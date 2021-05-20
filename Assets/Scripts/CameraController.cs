using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private PlayerController_Flying controller;
    [SerializeField] private GameObject cam;

    [SerializeField] private Vector3 baseOffset = new Vector3(0.0f, 9.0f, -10.0f);
    [SerializeField] private float maxSpeedExtraOffset = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    private float maxOffsetTime = 0.5f;
    private Vector3 extraSpeedOffset
    {
        get
        {
            return -cam.transform.InverseTransformDirection(cam.transform.forward) *
            maxSpeedExtraOffset;
        }
    }
    private float offsetPercentage;
    [SerializeField] private float extraZoomDamperFactor = 4.0f;

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

    #region Zoom Methods
    
    private void CameraZoom()
    {
        offsetPercentage = DetermineExtraOffsetFromPlayerSpeed();

        // Determine cam position it WANTS to move to
        Vector3 desiredCamPosition = originalCameraLocalPosition +
            extraSpeedOffset *
            offsetPercentage;

        float distanceFromCamPositionToDesired = Vector3.Distance(desiredCamPosition, cam.transform.localPosition);

        // Based on current cam position and position it wants to move to, choose 
        // whether to move directly there or partially there
        if(distanceFromCamPositionToDesired < 0.05f)
        {
            // Help prevent jitter for extremely small zoom changes
            return;
        }
        else if (distanceFromCamPositionToDesired < 0.1f)
        {
            // General Zoom
            GeneralZoom(desiredCamPosition);
        }
        else
        {
            // Controlled Zoom
            ExtraSpeedZoom(desiredCamPosition);
        }
    }

    private void GeneralZoom(Vector3 targetPosition)
    {
        cam.transform.localPosition = targetPosition;
        Debug.Log("Standard ZOOM");
    }

    private void ExtraSpeedZoom(Vector3 targetPosition)
    {
        cam.transform.localPosition += (targetPosition - cam.transform.localPosition) / extraZoomDamperFactor;
        Debug.Log("Extra Speed ZOOM");
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

    #endregion
}
