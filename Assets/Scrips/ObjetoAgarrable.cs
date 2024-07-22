using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ObjetoAgarrable : MonoBehaviour
{
    [Header("tomadoObjeto")]
    public GameObject objectToPickuUp;
    public GameObject pickedObject;
    public Transform interactionZone;
    [Header("lanzarObjeto")]
    public float distanciaObjeto = 2f;
    public float fuerzaObjeto = 250f;
    AgarrarObjeto agarrarObjeto;
   

    void Update()
    {
        if (objectToPickuUp != null && objectToPickuUp.GetComponent<AgarrarObjeto>().isPickable == true&& pickedObject == null)
        {
            if(Input.GetKeyUp(KeyCode.F))
            {
                pickedObject = objectToPickuUp;
                pickedObject.GetComponent<AgarrarObjeto>().isPickable = false;
                pickedObject.transform.SetParent(interactionZone);
                pickedObject.transform.rotation = gameObject.transform.rotation;
                pickedObject.transform.position = interactionZone.position;
                pickedObject.GetComponent<Rigidbody>().useGravity = false;
                pickedObject.GetComponent<Rigidbody>().isKinematic = true;

                
            }
        }
        else if (objectToPickuUp != null)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                pickedObject.GetComponent<AgarrarObjeto>().isPickable = true;

                pickedObject.transform.SetParent(null);
                pickedObject.transform.position = interactionZone.position;
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                LanzarObjeto();
                pickedObject = null;
            }
        }
        
    }

    private void LanzarObjeto()
    {
        if(pickedObject != null)
        {
            Rigidbody rbToLaunch = pickedObject.GetComponent<Rigidbody>();
            rbToLaunch.AddRelativeForce(new Vector3(0, 0, 1) * 10f, ForceMode.Impulse);
            rbToLaunch.AddRelativeForce(new Vector3(0, 1, 0) * 5f, ForceMode.Impulse);
        }
    }
}
