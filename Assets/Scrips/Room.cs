using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class Room : MonoBehaviour
{
    public string TriggerCamera;
    public CinemachineVirtualCamera _primaryvVirtualCamera;
    public CinemachineVirtualCamera[] _virtualCameras;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TriggerCamera))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera); 

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TriggerCamera))
        {
            CinemachineVirtualCamera currentCamera = _virtualCameras.FirstOrDefault(camera => camera.enabled);
            if (currentCamera != null && currentCamera == other.GetComponentInChildren<CinemachineVirtualCamera>())
            {
                //SwitchToCamera(_primaryvVirtualCamera);
            }
        }
    }
    private void SwitchToCamera(CinemachineVirtualCamera targetcamera)
    {
        foreach(CinemachineVirtualCamera camera in _virtualCameras)
        {
            camera.enabled = camera == targetcamera;
        }
    }
}
