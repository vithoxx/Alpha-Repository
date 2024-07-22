using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHacha : MonoBehaviour
{
    public GameObject activarObjeto;
    private void Start()
    {
        activarObjeto.SetActive(false);
    }
    public void Activar()
    {
        activarObjeto.SetActive(true);
    }
    public void Desactivar()
    {
        activarObjeto.SetActive(false);
    }
}
