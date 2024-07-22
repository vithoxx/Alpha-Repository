using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CajonSimple : MonoBehaviour
{
    [Header("Settings")]
    public float tiempo;
    public AnimationCurve curve;

    public float cuandoAbre;

    [Header("informativo")]
    public float difCajonAbierto;

    public bool abierto = false;
    public bool abriendo = false;
    public Vector3 posCerrado;

    public Vector3 posAbierto;

    private void Start()
    {
        posCerrado = transform.localPosition;
        posAbierto = new Vector3(transform.localPosition.x - cuandoAbre, transform.localPosition.y, transform.localPosition.z);
    }

    //private void Update()
    //{
    //    if (abriendo)
    //    {
    //        transform.localPosition = Vector3.MoveTowards(transform.localPosition, posAbierto, Time.deltaTime * velocidad);
    //        if (Vector3.Distance(transform.localPosition, posAbierto) < 0.1f)
    //        {
    //            abierto = true;
    //            abriendo = false;
    //            cerrado = false;
    //        }
    //        else
    //        {
    //            abriendo = true;
    //        }
    //    }

    //    if (cerrado)
    //    {
    //        transform.localPosition = Vector3.MoveTowards(transform.localPosition, posCerrado, Time.deltaTime * velocidad);
    //        if (Vector3.Distance(transform.localPosition, posCerrado) < 0.00001f)
    //        {
    //            abierto = false;
    //            abriendo = false;
    //            cerrado = false;
    //        }
    //        else
    //        {
    //            abriendo = true;
    //        }
    //    }
    //}

    public void Open()
    {
        if (abierto) return;
        if (abriendo) return;
        abierto = true;

        StartCoroutine(OpenClose(posAbierto));
    }
    public void Close()
    {
        if (!abierto) return;
        if (abriendo) return;
        abierto = false;

        StartCoroutine(OpenClose(posCerrado));
    }

    private IEnumerator OpenClose(Vector3 position)
    {
        abriendo = true;
        Vector3 actualPosition = transform.localPosition;

        for (float i = 0; i < tiempo; i += Time.deltaTime)
        {
            transform.localPosition = Vector3.LerpUnclamped(actualPosition, position, curve.Evaluate( i / tiempo));
            yield return null;
        }

        transform.localPosition = position;
        abriendo = false;
    }

}
