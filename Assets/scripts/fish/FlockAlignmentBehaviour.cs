using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fish/Flocking/Alignment Behaviour")]

public class FlockAlignmentBehaviour : FlockBehaviour
{
    public override Vector3 CalculateNextMove(FishBase fish, Transform[] flock)
    {
        if (flock.Length <= 0) return fish.transform.forward;
        
        Vector3 flockDirection = Vector3.zero;
        
        foreach (var neighbour in flock)
        {
            flockDirection += neighbour.forward;
        }
        
        flockDirection /= flock.Length;

        return flockDirection;
    }
}
