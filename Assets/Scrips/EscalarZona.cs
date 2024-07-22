using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalarZona : MonoBehaviour
{
    public float alcance;
    public Transform ZonaPlayer;

    private Vector3 velocity;
    public float velocidadNormal;
    public float gravedad;

    public move move;

    public CharacterController characterController;

    public Animator animator;

    private void Update()
    {
        Detectar();
    }
    public void Detectar()
    {
        if (Physics.Raycast(ZonaPlayer.position, transform.forward, out RaycastHit hit, alcance))
        {
            if (hit.transform.CompareTag("Subible"))
            {
                float entradaHorizontal = Input.GetAxis("Horizontal");
                float entradaVertical = Input.GetAxis("Vertical");

                Vector3 direction = new Vector3(entradaHorizontal, entradaVertical).normalized;
                direction = hit.transform.rotation * direction;
                animator.SetBool("Subiendo", true);

                velocity.y = direction.y * velocidadNormal;
                velocity.z = direction.z * velocidadNormal;

                characterController.Move(direction * velocidadNormal * Time.deltaTime);

                move.enabled = false;
                Debug.Log("tocando");
            }

        }
        else
        {
            animator.SetBool("Subiendo", false);
            move.enabled = true;
        }

    }
    private void Gravedad()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravedad * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ZonaPlayer.position, transform.forward * alcance);
    }
}
