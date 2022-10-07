using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    
    private CinemachineFramingTransposer cinemachineFramingTransposer;
    private float targetFollowOffset;

    Vector3 rotationVector = new Vector3(0,0,0);

    private void Start() 
    {
        cinemachineFramingTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();   
        targetFollowOffset = cinemachineFramingTransposer.m_CameraDistance; 
    }

    private void Update() 
    {
        HandleRotation();
        HandleZoom(); 
    }


    private void HandleRotation() 
    {
        rotationVector.y = InputManager.Instance.GetCameraRotateAmount();
        
        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom() 
    {
        float zoomAmount = 1f;

        targetFollowOffset += InputManager.Instance.GetCameraZoomAnout() * zoomAmount;

        targetFollowOffset = Mathf.Clamp(targetFollowOffset, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        float zoomSpeed = 5f;
        cinemachineFramingTransposer.m_CameraDistance = Mathf.Lerp(cinemachineFramingTransposer.m_CameraDistance,
                                                            targetFollowOffset,
                                                            Time.deltaTime * zoomSpeed);
    }

    public float GetCameraRotation()
    {
        return transform.rotation.y;
    }
}
