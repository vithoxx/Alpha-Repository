using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public List<Transform> pointsOfInterest = new List<Transform>();

    public Transform GetRandomPointOfInterest()
    {
        if (pointsOfInterest.Count == 0)
            return null;

        int randomIndex = Random.Range(0, pointsOfInterest.Count);
        return pointsOfInterest[randomIndex];
    }
}
