using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cordura : MonoBehaviour
{
    public float maxSanity = 100f;
    public float minSanity = 0f;
    public float tiempoEsperaBajar, tiempoEsperaSubir;
    public float tiempoPasado = 0f;
    public float actualSanity;

    public bool tieneLampara;
    public bool lamparaEncendida;
    public bool seReinicio;

    bool beganCor;


    private void Start()
    {
        actualSanity = maxSanity;
    }

    private void Update()
    {
        if (!CheckLamp()) return;

        CheckSanity();

        if (IsLampOn() && !beganCor)
        {
            StartCoroutine(UpSanity());
        }
        else if (!beganCor)
        {
            StartCoroutine(LowerSanity());
        }
    }

    //Aumenta la sanidad
    IEnumerator UpSanity()
    {
        beganCor = true;
        while (IsLampOn())
        {
            yield return new WaitForSeconds(tiempoEsperaSubir);
            actualSanity++;
            actualSanity = Mathf.Min(maxSanity, actualSanity);
        }
        beganCor = false;
    }

    //Baja la sanidad
    IEnumerator LowerSanity()
    {
        beganCor = true;
        while (!IsLampOn())
        {
            yield return new WaitForSeconds(tiempoEsperaBajar);
            actualSanity--;
            actualSanity = Mathf.Max(minSanity, actualSanity);
        }
        beganCor = false;
    }

    //Hace algo si cordura 0
    void CheckSanity()
    {
        if(actualSanity <= 0)
        {
            StopAllCoroutines();
            //Hace Algo
        }
    }


    //Sirve para chequear la sanidad
    public float GetSanity()
    {
        return actualSanity;
    }

    //Chequea si el player a prendido la lampara
    bool IsLampOn()
    {
        return GameManager.Instance.lamparaEncendida;
    }

    //Chequea si el player tiene la lampara
    bool CheckLamp()
    {
        return GameManager.Instance.tieneLampara;
    }
}