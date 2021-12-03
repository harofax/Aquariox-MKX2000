using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    
    [SerializeField]
    private float distance = 2f;

    [SerializeField]
    private float currentDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;
        var prevCol = Gizmos.color;
        var position = transform.position;
        var targetPosition = target.position;
        currentDistance = Vector3.Distance(position, targetPosition);
        Gizmos.color =  currentDistance <= distance ? Color.blue : Color.red;
        
        Gizmos.DrawLine(position, targetPosition);
    }
}
