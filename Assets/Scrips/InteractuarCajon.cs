using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarCajon : MonoBehaviour
{

    public Animator Animator;
    public bool abrir;
    public bool cerrar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ListoParaAbrir");
            if(Input.GetKeyDown(KeyCode.E))
            {
                Animator animator = GetComponent<Animator>();

                Animator.Play("Cajon1");
            }
           
        }
       
    }
}
