using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScripPerro : MonoBehaviour
{

    public NavMeshAgent agentPerro;
    public Transform player;
    public Transform Perro;

    public Transform centro;
    public float radio;
    public LayerMask layermask;

    public int perroDa�o;

    private void Update()
    {
        if ((Perro.position - player.position).magnitude > 0.5f)
        {
            agentPerro.SetDestination(player.position);
        }
        Damage();
    }


    private void Damage()
    {
        Collider[] collider = Physics.OverlapSphere(centro.position, radio, layermask);

        foreach (Collider player in collider)
        {

            if (player == null)
            {
                return;
            }

            move healthSystem = player.GetComponent<move>();
            if (healthSystem != null)
            {
                Debug.Log("Hace da�o");
                healthSystem.SetDamage(perroDa�o);

            }

            else
            {
                Debug.Log("no tiene el componenete");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centro.position, radio);
    }

}
