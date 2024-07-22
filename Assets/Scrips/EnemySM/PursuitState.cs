using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : BaseState
{
    EnemySM _enemySM;
    public PursuitState(EnemySM enemySM) : base(enemySM) { _enemySM = enemySM; }

    public override void Enter()
    {
        _enemySM.navMeshAgent.SetDestination(GameManager.Instance._playerRef.transform.position);
    }

    public override void UpdateLogic()
    {
        _enemySM.timerCurrent -= Time.deltaTime;

        if (_enemySM.timerCurrent < 0)
        {
            FieldOfViewCheck();
            _enemySM.timerCurrent = _enemySM.timerMax;
        }
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
                        _enemySM.navMeshAgent.SetDestination(GameManager.Instance._playerRef.transform.position);
                        return;
                    }
                }
            }
        }
        _enemySM.ChangeState(_enemySM.huntingState);
    }
}
