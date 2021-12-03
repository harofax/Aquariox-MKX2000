using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fish/Flocking/Avoidance Behaviour")]
public class FlockAvoidBehaviour : FlockBehaviour
{
    public override Vector3 CalculateNextMove(FishBase fish, List<Transform> flock)
    {
        if (flock.Count == 0) return Vector3.zero;
        Vector3 avoidVector = Vector3.zero;
        foreach (Transform neighbourino in flock)
        {
            Vector3 distance = (fish.transform.position - neighbourino.position);
            
            if (distance.sqrMagnitude <= fish.SquaredIntimacyDistance)
            {
                avoidVector += distance;
            }
        }

        return avoidVector;
    }
}
