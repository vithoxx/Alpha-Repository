using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPerros : MonoBehaviour
{
    public ScripPerro perro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            perro.enabled = true;
        }
    }
}
