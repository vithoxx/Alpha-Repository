using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarConHacha : MonoBehaviour
{
    public Animator Animator;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Animator.Play("Hachazo");
        }
    }
}
