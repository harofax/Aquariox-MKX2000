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
    
    [Range(0f, 100f), Tooltip("How often the fish takes action")]
    public float docileness;
    
    [Range(0f, 100f)]
    public float moneyRate;

    [Range(1, 10)]
    public int moneyAmount;

    [Range(0.1f, 4f)]
    public float moveSpeed;

    [Range(4f, 10f)]
    public float maxSpeed;
    
    [Range(1f, 10f)]
    public float turnSpeed;

    [Range(1, 20)]
    public int maxSchoolSize;

    [Range(1f, 5f)]
    public float sightRange;

    [Range(0.4f, 8f)]
    public float intimacy;

}