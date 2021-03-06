using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Aquarium : MonoBehaviour
{
    [SerializeField]
    private Bounds spawnZone;
    
    void Awake()
    {
        spawnZone.center = this.transform.position;
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 minPos = spawnZone.center - spawnZone.extents / 2;
        Vector3 maxPos = spawnZone.center + spawnZone.extents / 2;
        float x = Random.Range(minPos.x, maxPos.x);
        float y = Random.Range(minPos.y, maxPos.y);
        float z = Random.Range(minPos.z, maxPos.z);
    
        return new Vector3(x, y, z);
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 centerWorld = transform.position + spawnZone.center;
        
        Gizmos.DrawWireSphere(centerWorld, 0.1f);
        Gizmos.DrawWireCube(centerWorld, spawnZone.extents);
        
        Gizmos.color = Color.red;
        Gizmos.DrawCube(centerWorld - (spawnZone.extents / 2), Vector3.one * 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(centerWorld + (spawnZone.extents / 2), Vector3.one * 0.5f);
    }
    #endif
}
