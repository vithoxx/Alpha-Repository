using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextoProgresivo : MonoBehaviour
{
    public GameObject panelTexto;
    public TextMeshProUGUI textoAEscribir;
    public bool escribiendo;
    public float tiempoEntreLetras;
    public int tiempoParaEliminarTexto;

    public void IniciarCorrutinaEscribir(string texto)
    {
        StartCoroutine(EscribirTextos(texto));
    }

    public IEnumerator EscribirTextos(string textToWrite)
    {
        if (!escribiendo)
        {
            textoAEscribir.text = "";
            escribiendo = true;
            panelTexto.SetActive(true);

            foreach (char letra in textToWrite)
            {
                textoAEscribir.text = textoAEscribir.text + letra;
                yield return new WaitForSeconds(tiempoEntreLetras);
            }

            yield return new WaitForSeconds(tiempoParaEliminarTexto);
            textoAEscribir.text = "";

            panelTexto.SetActive(false);
            escribiendo = false;
        }
        yield return null;

    }
}
