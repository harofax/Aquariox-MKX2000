using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Aquarium : MonoBehaviour
{
    [SerializeField]
    private Bounds spawnZone;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Start()
    {
        
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawWireSphere(spawnZone.center, 0.1f);
        Gizmos.DrawWireCube(spawnZone.center, spawnZone.extents);
        
        Gizmos.color = Color.red;
        Gizmos.DrawCube(spawnZone.center - (spawnZone.extents / 2), Vector3.one * 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(spawnZone.center + (spawnZone.extents / 2), Vector3.one * 0.5f);
    }
}
