using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;
    
    private void Start() 
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();   
        targetFollowOffset = cinemachineTransposer.m_FollowOffset; 
    }

    private void Update() 
    {
        HandleRotation();
        HandleZoom(); 
    }


    private void HandleRotation() 
    {
        Vector3 rotationVector = new Vector3(0,0,0);
        
        rotationVector.y = InputManager.Instance.GetCameraRotateAmount();
        
        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom() 
    {
        float zoomAmount = 1f;

        targetFollowOffset.y += InputManager.Instance.GetCameraZoomAnout() * zoomAmount;

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        float zoomSpeed = 5f;
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset,
                                                            targetFollowOffset,
                                                            Time.deltaTime * zoomSpeed);
    }
}
