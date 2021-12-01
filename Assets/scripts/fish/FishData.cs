using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fish/Fish Data")]
public class FishData : ScriptableObject
{
    [Range(0.3f, 2.0f)]
    public float hungerRate;
    
    [Range(0.5f, 2.0f)]
    public float happinessModifier;
    
    [Range(0.8f, 3f)]
    public float docileness;
    
    [Range(60f, 240f)]
    public float moneyRate;

    [Range(1, 10)]
    public int moneyAmount;

    [Range(0.1f, 10f)]
    public float moveSpeed;

    [Range(1f, 10f)]
    public float turnSpeed;

    [Range(1, 20)]
    public int maxSchoolSize;

    [Range(1f, 10f)]
    public float sightRange;

    [Range(1f, 8f)]
    public float intimacy;
}
