using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CustomEditor(typeof(IASeguimiento))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        IASeguimiento fov =(IASeguimiento)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DireccionFromAngle(fov.transform.eulerAngles.y, -fov.angle/2);
        Vector3 viewAngle02 = DireccionFromAngle(fov.transform.eulerAngles.y, fov.angle/2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position+ viewAngle01*fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position+ viewAngle02*fov.radius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);

        }

    }
    private Vector3 DireccionFromAngle(float eulerY, float angleInDegress)
    {
        angleInDegress += eulerY;
        return new Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), 0 ,Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }
}
