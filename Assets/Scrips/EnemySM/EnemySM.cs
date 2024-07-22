using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemySM : StateMachine
{
    BaseState currentState;
    public HuntingState huntingState;

    public PatrolState patrolState;

    public PursuitState pursuitState;

    public float radius;
    [Range(0f, 360)]

    public float angle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public NavMeshAgent navMeshAgent;
    public List<Transform> waypoints;
    public int currentWaypointIndex = 0;
    public float waitTime = 2f;
    public bool isWaiting = false;

    public float timerCurrent, timerMax;

    public move player;


    public Transform centro;
    public float radio;
    public LayerMask layermask;
    public int dañoEnemiga;


    //Cambian variables para que Hunting y Pursuit tengan sus propios tiempos de espera antes de que se reseteen.
    private void Update()
    {
        Damage();
    }
    private void Awake()
    {
        huntingState = new HuntingState(this);
        patrolState = new PatrolState(this);
        pursuitState = new PursuitState(this);

    }
    protected override BaseState GetInitialState()
    {
        return patrolState;
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
}
