using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fish/Flocking/Keep Centered Behaviour")]
public class FlockKeepCentered : FlockBehaviour
{
    public Vector3 swimAreaCenter;
    public float maxDistance;
 
    public override Vector3 CalculateNextMove(FishBase fish, List<Transform> flock)
    {
        Vector3 offset = swimAreaCenter - fish.transform.position;
        float distance = offset.sqrMagnitude / (maxDistance * maxDistance);

        if (distance < 0.9)
        {
            return Vector3.zero;
        }

        return offset * distance;// * distance;

    }
}
