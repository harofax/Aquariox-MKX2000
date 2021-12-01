using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishState
{
    Idle,
    Hungry,
    Dying,
    Dead
}

public enum FishType
{
    Perch,
    Goldfish,
    Mullet
}

public abstract class FishBase : MonoBehaviour
{
    [SerializeField]
    private FishData fishData;

    [SerializeField]
    private FishType fishType;

    [SerializeField]
    private LayerMask fishLayer;

    private protected float HungerRate { get; private set; }
    private protected float HappinessModifier { get; private set; }
    private protected float Aggression { get; private set; }
    private protected float MoneyRate { get; private set; }
    private protected int MoneyAmount { get; private set; }
    private protected float MoveSpeed { get; private set; }
    private protected float SightRange { get; private set; }
    private protected int MaxSchoolSize { get; private set; }
    private protected float Intimacy { get; private set; }

    private protected Transform CurrentTarget { get; private set; }
    public float SquaredIntimacyDistance => Intimacy * Intimacy;

    private FishState currentState;
    
    private FlockBehaviour[] behaviours;
    protected Transform[] neighbours;

    private void Awake()
    {
        SetFishData(fishData);
        neighbours = new Transform[MaxSchoolSize];
        behaviours = FishManager.Instance.behaviours;
    }

    private void OnEnable()
    {
        GameManager.OnSchoolingTick += GetNeighbouringFish;
        GameManager.OnTick += Flock;
    }

    protected void ChangeState(FishState newState)
    {
        currentState = newState;
    }

    protected void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }

    protected void GetNeighbouringFish()
    {
        Collider[] neighbourFish = new Collider[MaxSchoolSize];
        int fishFound = Physics.OverlapSphereNonAlloc(transform.position, SightRange, neighbourFish,
        fishLayer);
        for (int i = 0; i < fishFound; i++)
        {
            neighbours[i] = neighbourFish[i].transform;
        }
    }

    private protected abstract void Execute();

    private protected virtual void Flock()
    {
    }
    

    protected FishState GetState()
    {
        return currentState;
    }

    private protected void Move(Vector3 movement)
    {
        transform.rotation = Quaternion.LookRotation(movement);
        transform.position = transform.forward * MoveSpeed * Time.deltaTime;
    }

    protected void SetFishData(FishData fishData)
    {
        HungerRate = fishData.hungerRate;
        HappinessModifier = fishData.happinessModifier;
        Aggression = fishData.docileness;
        MoneyRate = fishData.moneyRate;
        MoneyAmount = fishData.moneyAmount;
        MoveSpeed = fishData.moveSpeed;
        MaxSchoolSize = fishData.maxSchoolSize;
        SightRange = fishData.sightRange;
        Intimacy = fishData.intimacy;
    }
}
