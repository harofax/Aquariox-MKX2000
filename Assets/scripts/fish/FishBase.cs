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

public abstract class FishBase : MonoBehaviour
{
    [SerializeField]
    private FishData fishStats;

    public FishData FishStats
    {
        get => fishStats;
        set => fishStats = value;
    }

    [SerializeField]
    internal FishType fishType;

    [SerializeField]
    private LayerMask fishLayer;

    // ------ AI stuff ------------
    private protected float HungerRate { get; private set; }
    private protected float HappinessModifier { get; private set; }
    private protected float Aggression { get; private set; }
    private protected float MoneyRate { get; private set; }
    private protected int MoneyAmount { get; private set; }
    
    // ------ internal stats -----------
    public float CurrentHunger { get; set; }
    public float CurrentHappiness { get; set; }

    // ------ movement -----------------
    private protected float MaxSpeed { get; private set; }
    private protected float MoveSpeed { get; private set; }
    private protected float TurningSpeed { get; private set; }
    
    // ------ flocking -----------
    private protected float SightRange { get; private set; }
    private protected int MaxSchoolSize { get; private set; }
    private protected float Intimacy { get; private set; }

    private protected Transform CurrentTarget { get; private set; }
    public float GetIntimacy => Intimacy;
    private float SquaredMaxSpeed => MoveSpeed * MoveSpeed;

    private FishState currentState;
    
    private FlockBehaviour[] behaviours;
    
    protected List<Transform> neighbours = new List<Transform>();

    protected Rigidbody fishbody;
    private Collider selfCollider;
    private Vector3 currentVelocity;
    [SerializeField]
    private float smooth;

    private Collider[] neighbourFish;

    private void Awake()
    {
        SetFishData(fishStats);
        behaviours = FishManager.Instance.behaviours;
        fishbody = GetComponent<Rigidbody>();
        selfCollider = GetComponent<Collider>();
        neighbourFish = new Collider[MaxSchoolSize];
    }

    private void OnEnable()
    {
        GameManager.OnSchoolingTick += GetNeighbouringFish;
        GameManager.OnTick += OnTickMove;
        GameManager.OnTick += Execute;
    }

    private void OnDisable()
    {
        GameManager.OnSchoolingTick -= GetNeighbouringFish;
        GameManager.OnTick -= OnTickMove;
        GameManager.OnTick -= Execute;

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
            //Debug.DrawLine(transform.position, neighbourFish[i].transform.position, Color.yellow);
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
        
        fishbody.MoveRotation(rot);
        Vector3 moveDelta = Vector3.SmoothDamp(transform.forward, movement, ref currentVelocity, smooth );

        if (fishbody.velocity.sqrMagnitude <= SquaredMaxSpeed)
        {
             fishbody.AddForce(moveDelta*Time.deltaTime, ForceMode.Impulse);
        }
    }

    public void SetFishData(FishData inputData)
    {
        HungerRate = inputData.hungerRate;
        HappinessModifier = inputData.happinessModifier;
        Aggression = inputData.docileness;
        MoneyRate = inputData.moneyRate;
        MoneyAmount = inputData.moneyAmount;
        MoveSpeed = Random.Range(MoveSpeed/2, inputData.moveSpeed);
        MaxSpeed = Random.Range(MoveSpeed, inputData.maxSpeed);
        TurningSpeed = Random.Range(1f, inputData.turnSpeed);
        MaxSchoolSize = inputData.maxSchoolSize;
        SightRange = inputData.sightRange;
        Intimacy = inputData.intimacy;
    }

    private void OnTickMove()
    {
        Move((transform.forward + Random.onUnitSphere) * MoveSpeed);
    }

    protected virtual void FixedUpdate()
    {
        Flock();
    }
}
