using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamparaPick : MonoBehaviour
{
    public LamparaAceite lamp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            lamp.lamparaEnMano = true;
            GameManager.Instance.tieneLampara= true;
            Destroy(gameObject);
        }
    }
}
