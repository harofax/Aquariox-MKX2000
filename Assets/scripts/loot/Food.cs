using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Loot
{
    [SerializeField]
    private FoodData foodData;

    private MeshFilter foodMesh;
    private float happinessBonus;
    private float babyProgress;
    
    // Start is called before the first frame update
    void Start()
    {
        foodMesh = GetComponent<MeshFilter>();
    }

    public override void Initiate()
    {
        foodMesh.mesh = foodData.foodMesh;
        happinessBonus = foodData.happinessBonus;
        babyProgress = foodData.babyProgress;
    }
}
