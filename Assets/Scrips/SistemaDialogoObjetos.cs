using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDialogoObjetos : MonoBehaviour
{
   // public GameObject activarTexto;
    public TextoProgresivo texto; 
    public string mensaje; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            if (texto != null)
            {
               texto.IniciarCorrutinaEscribir(mensaje);
            }
            else
            {
                Debug.LogError("TextoProgresivo no está asignado.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          
        }
    }
}
