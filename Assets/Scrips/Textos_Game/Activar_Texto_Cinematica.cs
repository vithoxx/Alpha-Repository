using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activar_Texto_Cinematica : MonoBehaviour
{
    public TextoProgresivo texto;
    public string mensaje;

    private void Update()
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
