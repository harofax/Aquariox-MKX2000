using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fish/Flocking/Grouping Behaviour")]
public class FlockGroupingBehaviour : FlockBehaviour
{
    public override Vector3 CalculateNextMove(FishBase fish, List<Transform> flock)
    {
        if (flock.Count == 0) return Vector3.zero;
        
        Vector3 flockCenter = Vector3.zero;

        foreach (Transform neighbour in flock)
        {
            flockCenter += neighbour.position;
        }

        flockCenter /= flock.Count;

        flockCenter -= fish.transform.position;

        return flockCenter;
    }
}
