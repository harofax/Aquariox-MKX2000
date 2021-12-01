using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FishBase))]
public abstract class FlockBehaviour : ScriptableObject
{
    public abstract Vector3 CalculateNextMove(FishBase fish, Transform[] flock);
}
