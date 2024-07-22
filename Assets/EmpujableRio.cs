using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmpujableRio : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 position
    {
        get => rb.position;
        set => rb.position = value; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ReiniciarTronco"))
        {
            transform.localPosition = new Vector3(7f, 2.5f, -5.7f);
        }
    }
}
