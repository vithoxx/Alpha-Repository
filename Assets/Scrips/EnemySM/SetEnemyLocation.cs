using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyLocation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy in location");
            GameManager.Instance._enemyCurrentArea = gameObject;
        }
    }
}
