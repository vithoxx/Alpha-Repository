using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodigoRio : MonoBehaviour
{
    public float speed;
    public Vector3 direction;

    List<EmpujableRio> movibles;

    private void Awake()
    {
        movibles = new List<EmpujableRio>();
    }

    private void Update()
    {
        foreach (var movible in movibles)
        {
            movible.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EmpujableRio empujable))
        {
            empujable.rb.velocity = Vector3.zero;
            movibles.Add(empujable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EmpujableRio empujable))
        {
            if (movibles.Contains(empujable))
            {
                movibles.Remove(empujable);
            }
         }
    }
}
