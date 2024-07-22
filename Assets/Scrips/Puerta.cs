using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{

    public int vidaPuerta = 3;
    public GameObject puertaNormal;
    public GameObject puertaRota;

    public PegarConHacha pegarConHacha;
    public GameObject activarHacha;
    private void Update()
    {
        switch (vidaPuerta)
        {
            case 3:
                // Puerta normal
                //puertaNormal.SetActive(true);
                //puertaRota.SetActive(false);
                break;
            case 2:
                // Trizar puerta
                //puertaNormal.SetActive(true);
                //puertaRota.SetActive(false);
                break;
            case 1:
                // Puerta rota
                puertaNormal.SetActive(false);
                puertaRota.SetActive(true);
                pegarConHacha.enabled = false;
                activarHacha.SetActive(false);

                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hacha"))
        {
            if (vidaPuerta > 0)
            {
                vidaPuerta--;
            }
        }
    }
   
}
