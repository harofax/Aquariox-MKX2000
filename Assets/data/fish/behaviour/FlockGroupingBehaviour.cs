using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fish/Flocking/Grouping Behaviour")]
public class FlockGroupingBehaviour : FlockBehaviour
{
    public float smoothTime = 0.1f;
    private Vector3 currentVelocity;
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

        flockCenter = Vector3.SmoothDamp(fish.transform.forward, flockCenter, ref currentVelocity, smoothTime);

        return flockCenter;
    }
}
