using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;

    public void ActivateCamera(int cameraIndex)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = (i == cameraIndex) ? 10 : 0;
        }
    }
}
