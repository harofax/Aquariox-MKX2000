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

public abstract class FishBase : MonoBehaviour
{
    private FishState currentState;

    private protected float HungerRate { get; private set; }
    private protected float HappinessModifier { get; private set; }
    private protected float Aggression { get; private set; }
    private protected float MoneyRate { get; private set; }

    private protected int MoneyAmount { get; private set; }


    protected internal void ChangeState(FishState newState)
    {
        currentState = newState;
    }

    private protected abstract void Execute();
    

    protected internal FishState GetState()
    {
        return currentState;
    }

    protected internal void SetFishData(FishData fishData)
    {
        HungerRate = fishData.hungerRate;
        HappinessModifier = fishData.happinessModifier;
        Aggression = fishData.docileness;
        MoneyRate = fishData.moneyRate;
        MoneyAmount = fishData.moneyAmount;
    }
}
