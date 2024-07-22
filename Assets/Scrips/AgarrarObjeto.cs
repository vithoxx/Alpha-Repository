using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    public bool isPickable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interaccion"))
        {
            other.GetComponentInParent<ObjetoAgarrable>().objectToPickuUp = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interaccion"))
        {
            other.GetComponentInParent<ObjetoAgarrable>().objectToPickuUp = null;
        }
    }

}

