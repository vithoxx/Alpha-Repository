using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public GameObject apagarObjeto;

    public void ApagarLinterna()
    {
        apagarObjeto.SetActive(false);
    }
    public void EncenderLinterna()
    {
        apagarObjeto.SetActive(true);
    }
}
