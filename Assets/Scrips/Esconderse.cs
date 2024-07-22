using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esconderse : MonoBehaviour
{
    public enum State { None, Moviendose, Escondido }

    public Transform jugador;
    public float distanciaEsconderse = 2f;

    public float VelocidadDesplazamientoEscondite;
    public LayerMask whatIsHidespot;

    public LamparaAceite LamparaAceite;

    public static State Estado { get; private set; }

    public Animator animator;

    public GameObject player;

    private LugarParaEsconderme lugarActual;

    public move playerMove;

    private string hidingPlaceName;

    public Animator animatorPlayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Estado == State.None)
            {

                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distanciaEsconderse, whatIsHidespot))
                {
                    Debug.Log("Hitting");

                    if (hit.collider.TryGetComponent(out LugarParaEsconderme lugar))
                    {
                        lugarActual = lugar;
                        hidingPlaceName = lugar.placeName;
                        Escondido(hidingPlaceName);
                    }
                }
            }

            else if (Estado == State.Escondido && lugarActual != null)
            {
                SalirEscondite(hidingPlaceName);
            }
        }
    }

    void Escondido(string placeName)
    {
        if (Estado == State.Moviendose) return;

        Estado = State.Moviendose;
        
        LamparaAceite.luz.enabled = false;
        LamparaAceite.enabled = false;
        StartCoroutine(MoverJugadorAPosicion(lugarActual.puntoInicial.position, State.Escondido));

        switch (placeName)
        {
            case "Armario":
                player.transform.Rotate(0, -180, 0, Space.World);
                animator.Play("PuertasArmarioAbrir");
                //animatorPlayer.Play("AbrirCloset");

                break;
            case "Cama":
                animatorPlayer.Play("BajoCama");
                //player.transform.Rotate(-90, 90, 0, Space.World);
                break;
            default:
                break;
        }
        playerMove.animator.SetFloat("Speed",0);
        playerMove.enabled = false;
       
        Debug.Log("El jugador se ha escondido.");
        

    }

    void SalirEscondite(string placeName)
    {
        if (Estado == State.Moviendose) return;

        Estado = State.Moviendose;

        StartCoroutine(contadorLuz(1.5f));
       
        StartCoroutine(MoverJugadorAPosicion(lugarActual.puntoSalida.position, State.None));

        switch (placeName)
        {
            case "Armario":
                player.transform.Rotate(0, -180, 0, Space.World);
                animator.Play("PuertasArmarioSalir");

                break;
            case "Cama":
                animatorPlayer.Play("SalirCama");
                //player.transform.Rotate(0, 0, 0, Space.World);
                break;
            default:
                break;
        }
        playerMove.enabled = true;
      
        Debug.Log("El jugador ha salido de su escondite.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * distanciaEsconderse);
    }

    IEnumerator MoverJugadorAPosicion(Vector3 targetPosition, State newState)
    {

        Vector3 startingPosition = jugador.position;

        for (float i = 0; i < VelocidadDesplazamientoEscondite; i += Time.deltaTime)
        {
            jugador.position = Vector3.Lerp(startingPosition, targetPosition, i / VelocidadDesplazamientoEscondite);
            yield return null;
        }

        jugador.position = targetPosition;
        Estado = newState;
    }
    IEnumerator contadorLuz(float secons)
    {
        yield return new WaitForSeconds(secons);
        LamparaAceite.luz.enabled = false;
        LamparaAceite.enabled = true;
    }
    public void AnimacionMueble()
    {

    }
       
}
