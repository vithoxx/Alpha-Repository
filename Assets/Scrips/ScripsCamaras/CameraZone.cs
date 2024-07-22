using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public int cameraIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraSwitcher cameraSwitcher = FindObjectOfType<CameraSwitcher>();
            if (cameraSwitcher != null)
            {
                cameraSwitcher.ActivateCamera(cameraIndex);
            }
        }
    }
}
