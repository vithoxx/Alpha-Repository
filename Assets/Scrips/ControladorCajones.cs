using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCajones : MonoBehaviour
{

    public List<CajonSimple> cajones = new();
    private int intCajon = -1;

    private void Start()
    {
        intCajon = -1;
    }

    public void Toggle()
    {
        intCajon++;
        if (intCajon >= cajones.Count) intCajon = -1;

        for(int i = 0; i< cajones.Count; i++)
        {
            bool abrir = i == intCajon;
            if (abrir) cajones[i].Open();
            else cajones[i].Close();
        }
    }
}
