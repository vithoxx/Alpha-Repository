using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    EnemySM _enemySM;
    public PatrolState(EnemySM enemySM) : base(enemySM) { _enemySM = enemySM; }

    public override void Enter()
    {
        SetWaypoint();
    }

    public override void UpdateLogic()
    {
        if (!_enemySM.navMeshAgent.pathPending && _enemySM.navMeshAgent.remainingDistance < 0.1f && !_enemySM.isWaiting)
        {
            _enemySM.isWaiting = true;
            SetWaypoint();
        }

        //_enemySM.timerCurrent -= Time.deltaTime;
        //if (_enemySM.timerCurrent < 0)
        //{
        //    FieldOfViewCheck();
        //    _enemySM.timerCurrent = _enemySM.timerMax;
        //}
    }

    void SetWaypoint()
    {
        if (_enemySM.waypoints.Count == 0)
        {
            Debug.LogWarning("No hay waypoints asignados.");
            return;
        }

        int randomIndex = Random.Range(0, _enemySM.waypoints.Count);
        Vector3 randomDestination = _enemySM.waypoints[randomIndex].position;

        _enemySM.navMeshAgent.SetDestination(randomDestination);

        _enemySM.isWaiting = false;
        _enemySM.timerCurrent = _enemySM.timerMax;
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(_enemySM.transform.position, _enemySM.radius, _enemySM.targetMask);

        if (rangeChecks.Length != 0)
        {
            foreach (Collider col in rangeChecks)
            {
                Transform target = col.transform;
                Vector3 directionToTarget = (target.position - _enemySM.transform.position).normalized;

                if (Vector3.Angle(_enemySM.transform.forward, directionToTarget) < _enemySM.angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(_enemySM.transform.position, target.position);

                    if (!Physics.Raycast(_enemySM.transform.position, directionToTarget, distanceToTarget, _enemySM.obstructionMask))
                    {
                        _enemySM.canSeePlayer = true;
                        _enemySM.ChangeState(_enemySM.pursuitState);
                        return;
                    }
                }
            }
        }

        _enemySM.canSeePlayer = false;
    }

    public override void Exit()
    {
        //Nada
    }
}
