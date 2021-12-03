using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantGoldFish : FishBase
{
    private protected override void Execute()
    {
        throw new System.NotImplementedException();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //Vector3 targetDirection = CurrentTarget.position - transform.position;
        //Move(targetDirection.normalized);
        
    }
}
