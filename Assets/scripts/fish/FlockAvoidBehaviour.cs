using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fish/Flocking/Avoidance Behaviour")]

public class FlockAvoidBehaviour : FlockBehaviour
{
    public override Vector3 CalculateNextMove(FishBase fish, Transform[] flock)
    {
        if (flock.Length <= 0) return Vector3.zero;
        throw new System.NotImplementedException();
    }
}
