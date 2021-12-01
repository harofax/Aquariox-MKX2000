using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    
    private protected float TurningSpeed { get; private set; }
    private protected float SightRange { get; private set; }
    private protected int MaxSchoolSize { get; private set; }
    private protected float Intimacy { get; private set; }

    private protected Transform CurrentTarget { get; private set; }
    public float SquaredIntimacyDistance => Intimacy * Intimacy;

    private FishState currentState;
    
    private FlockBehaviour[] behaviours;
    
    protected List<Transform> neighbours = new List<Transform>();

    protected Rigidbody rb;

    private void Awake()
    {
        SetFishData(fishData);
        behaviours = FishManager.Instance.behaviours;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        GameManager.OnSchoolingTick += GetNeighbouringFish;
        //GameManager.OnTick += Flock;
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
        neighbours.Clear();
        Collider[] neighbourFish = new Collider[MaxSchoolSize];
        int hits = Physics.OverlapSphereNonAlloc(
            transform.position, 
            SightRange, neighbourFish,
        fishLayer,
            QueryTriggerInteraction.Collide);
        for (int i = 0; i < hits; i++)
        {
            if (neighbourFish[i].transform == this.transform)
            {
                continue;
            }
            Debug.DrawLine(transform.position, neighbourFish[i].transform.position, Color.yellow);
            neighbours.Add(neighbourFish[i].transform);
        }
    }

    private protected abstract void Execute();

    private protected virtual void Flock()
    {
        if (behaviours.Length == 0)
        {
            Debug.LogError("Fish behaviour is missing", this);
        }

        if (neighbours.Count == 0)
        {
            Move(transform.forward);
            return;
        }
        Vector3 movement = Vector3.zero;
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector3 moveCalc = behaviours[i].CalculateNextMove(this, neighbours);

            movement += moveCalc;
        }

        movement = movement.normalized;
        
        Move(movement);
    }
    

    protected FishState GetState()
    {
        return currentState;
    }

    private protected void Move(Vector3 movement)
    {
        Quaternion rot = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(movement),
            TurningSpeed * Time.fixedDeltaTime);
        
        rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
        rb.MoveRotation(rot);
        // transform.rotation = Quaternion.LookRotation(movement);
        // transform.Translate( 0, 0, MoveSpeed * Time.deltaTime);
    }

    protected void SetFishData(FishData inputData)
    {
        HungerRate = inputData.hungerRate;
        HappinessModifier = inputData.happinessModifier;
        Aggression = inputData.docileness;
        MoneyRate = inputData.moneyRate;
        MoneyAmount = inputData.moneyAmount;
        MoveSpeed = inputData.moveSpeed;
        TurningSpeed = inputData.turnSpeed;
        MaxSchoolSize = inputData.maxSchoolSize;
        SightRange = inputData.sightRange;
        Intimacy = inputData.intimacy;
    }

    private void FixedUpdate()
    {
        Flock();
        //rb.AddForce(transform.forward * (MoveSpeed * Time.fixedDeltaTime), ForceMode.VelocityChange);
    }
}
