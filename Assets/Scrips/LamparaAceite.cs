using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LamparaAceite : MonoBehaviour
{
    public bool encendida = false;
    public Light luz;
    public bool lamparaEnMano;
    public Cordura cordura;
    [Header("aceite")]
    public float CantidadAceiteActual = 100;
    public float CantidadAceiteMax = 100;
    public float velocidadConsumo;
    public float velocidadRecarga;
    [Header("interfas")]
    public Image barraBateria;

    public Animator animator;



    private void Start()
    {
        //apagarObjeto.SetActive(false);
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Q) && lamparaEnMano == true)
        {
            encendida = !encendida;
            if (encendida == true)
            {
                animator.SetBool("EstaSujetandoLampara", true);
                luz.enabled = true;
                GameManager.Instance.lamparaEncendida = true;
            }
            if (encendida == false)
            {
                animator.SetBool("EstaSujetandoLampara", false);
                luz.enabled = false;
                GameManager.Instance.lamparaEncendida = false;
            }
        }
        if (luz.enabled == true)
        {
            CantidadAceiteActual -= Time.deltaTime * velocidadConsumo;

            if (CantidadAceiteActual <= 0)
            {
                CantidadAceiteActual = 0;
                luz.enabled = false;
            }
        }
        else if (luz.enabled == false)
        {
            CantidadAceiteActual += Time.deltaTime * velocidadRecarga;
            if (CantidadAceiteActual >= CantidadAceiteMax)
            {
                CantidadAceiteActual = CantidadAceiteMax;
            }
        }


        barraBateria.fillAmount = CantidadAceiteActual / 100;
    }
   
}
