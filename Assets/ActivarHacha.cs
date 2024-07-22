using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarHacha : MonoBehaviour
{
    public PegarConHacha pegarConHacha;
    public GameObject activarHacha;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pegarConHacha.enabled = true;
            activarHacha.SetActive(true);
            Destroy(gameObject);
        }
       
    }
}
