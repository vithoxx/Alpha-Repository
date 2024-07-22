using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Destruible : MonoBehaviour
{
    public GameObject ventanaNormal;
    public GameObject ventanaRota;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RomperVentana"))
        {
            ventanaNormal.SetActive(false);
            ventanaRota.SetActive(true);

        }
    }
}
