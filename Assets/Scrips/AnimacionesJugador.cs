using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesJugador : MonoBehaviour
{
    
    public Animator animator;

  public void Caminar()
    {
         animator.SetFloat("Blend", 0.4f);
    }
    public void Correr()
    {
        animator.SetFloat("Blend", 0.7f);
    }
    public void Agacharse()
    {
        animator.SetFloat("Blend", 1);
    }

}
