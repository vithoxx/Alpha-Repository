using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarObjetivo2 : MonoBehaviour
{

    public GameObject objetivo1;
    public GameObject objetivo2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objetivo1.SetActive(false);
            objetivo2.SetActive(true);
        }
    }
}
