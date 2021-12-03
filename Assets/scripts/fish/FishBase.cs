using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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

    // ------ AI stuff ------------
    private protected float HungerRate { get; private set; }
    private protected float HappinessModifier { get; private set; }
    private protected float Aggression { get; private set; }
    private protected float MoneyRate { get; private set; }
    private protected int MoneyAmount { get; private set; }
    
    // ------ movement -----------
    private protected float MaxSpeed { get; private set; }
    private protected float MoveSpeed { get; private set; }
    private protected float TurningSpeed { get; private set; }
    
    // ------ flocking -----------
    private protected float SightRange { get; private set; }
    private protected int MaxSchoolSize { get; private set; }
    private protected float Intimacy { get; private set; }

    private protected Transform CurrentTarget { get; private set; }
    public float SquaredIntimacyDistance => Intimacy * Intimacy;
    private float SquaredMaxSpeed => MoveSpeed * MoveSpeed;

    private FishState currentState;
    
    private FlockBehaviour[] behaviours;
    
    protected List<Transform> neighbours = new List<Transform>();

    protected Rigidbody rb;
    private Collider selfCollider;

    private void Awake()
    {
        SetFishData(fishData);
        behaviours = FishManager.Instance.behaviours;
        rb = GetComponent<Rigidbody>();
        selfCollider = GetComponent<Collider>();
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

    public void SetTarget(Transform newTarget)
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
        
        if (hits <= 1) {return;}
        
        for (int i = 0; i < hits; i++)
        {
            if (neighbours.Count >= MaxSchoolSize) break;
            if (ReferenceEquals(neighbourFish[i], selfCollider))
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
            Move(transform.forward * MoveSpeed);
            return;
        }
        Vector3 movement = Vector3.zero;
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector3 moveCalc = behaviours[i].CalculateNextMove(this, neighbours);
            moveCalc *= MoveSpeed;

            if (movement.sqrMagnitude > SquaredMaxSpeed)
            {
                movement = movement.normalized * MaxSpeed;
            }

            movement += moveCalc;
        }

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
            Quaternion.LookRotation(movement, Vector3.up),
            TurningSpeed * Time.fixedDeltaTime);
        //
        rb.MoveRotation(rot);
        Vector3 moveDelta = Vector3.MoveTowards(transform.position, transform.position + movement, Time.fixedDeltaTime);
        rb.MovePosition(moveDelta);

        
        // if (rb.velocity.sqrMagnitude <= SquaredMaxSpeed)
        // {
        //     rb.AddForce(transform.forward, ForceMode.Impulse);
        //     // rb.velocity = rb.velocity.normalized*MaxSpeed;
        // }
    }

    protected void SetFishData(FishData inputData)
    {
        HungerRate = inputData.hungerRate;
        HappinessModifier = inputData.happinessModifier;
        Aggression = inputData.docileness;
        MoneyRate = inputData.moneyRate;
        MoneyAmount = inputData.moneyAmount;
        MoveSpeed = Random.Range(1f, inputData.moveSpeed);
        MaxSpeed = Random.Range(1f, inputData.maxSpeed);
        TurningSpeed = Random.Range(1f, inputData.turnSpeed);
        MaxSchoolSize = inputData.maxSchoolSize;
        SightRange = inputData.sightRange;
        Intimacy = inputData.intimacy;
    }

    protected virtual void FixedUpdate()
    {
        Flock();
    }
}
