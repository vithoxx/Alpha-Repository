using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class IASeguimiento : MonoBehaviour
{
    public float radius;
    [Range(0f, 360)]

    public float angle;
 
    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public Transform jugador;
    public bool canSeePlayer;

    public NavMeshAgent navMeshAgent;
    public List<Transform> waypoints;
    private int currentWaypointIndex = 0;
    public float waitTime = 2f;
    private bool isWaiting = false;


    public Transform centro;
    public float radio;
    public LayerMask layermask;
    public int dañoEnemiga;


    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        jugador = GameObject.FindWithTag("Player").transform;
        StartCoroutine(FovRoutine());
        SetNextWaypoint();
    }

    private void Update()
    {
        Damage();
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f && !isWaiting)
        {
            isWaiting = true;
            Invoke(nameof(SetNextWaypoint), waitTime);
        }

        if (!canSeePlayer)
        {
            return;

        }
        else
        {
            if (Vector3.Distance(transform.position, jugador.position) < angle)
            {
                canSeePlayer = true;
                navMeshAgent.SetDestination(jugador.position);
            }
            else
            {
                canSeePlayer = false;
            }
        }
    }
    private IEnumerator FovRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FielOfViewCheck();
        }
    }

    private void FielOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            foreach (Collider col in rangeChecks)
            {
                Transform target = col.transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeePlayer = true;
                        return; 
                    }
                }
            }
        }

        canSeePlayer = false;
    }
   
    void SetNextWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogWarning("No hay waypoints asignados.");
            return;
        }

        int randomIndex = Random.Range(0, waypoints.Count);
        Vector3 randomDestination = waypoints[randomIndex].position;

        navMeshAgent.SetDestination(randomDestination);

        isWaiting = false;
    }
    public void ObservandoUltimaZona()
    {

    }
    public IEnumerator ObservandoZona()
    {
        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centro.position, radio);
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
                Debug.Log("Hace daño");
                healthSystem.SetDamage(dañoEnemiga);

            }

            else
            {
                Debug.Log("no tiene el componenete");
            }
        }
    }

    // El enemigo debe buscar las colisiones de la habitacion actual, la cual debera tener un controlador que guardará en una variable y tendrá una lista de transforms
    // con los distintos puntos de interes, y despues de un tiempo, deberá volver al patruyaje.
}


