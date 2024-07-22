using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarSuelo : MonoBehaviour
{
    public move Piso;
    private void OnTriggerStay(Collider other)
    {
        Piso.tocandoPiso = true;

    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
