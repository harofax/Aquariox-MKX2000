using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
struct FishDropTable
{
    [SerializeField]
    private FishType fishType;
    public FishType Type => fishType;
    
    [SerializeField]
    private DropTable fishDropTable;
    public DropTable DropTable => fishDropTable;

    public FishDropTable(FishType type, DropTable dropTable)
    {
        fishType = type;
        fishDropTable = dropTable;
    }
}
public class LootManager : MonoBehaviour
{
   [SerializeField]
    private FishDropTable[] fishDropTableData;

    [SerializeField]
    private Aquarium aquarium;

    [SerializeField]
    private FoodData storeFood;
    
    [SerializeField]
    public int buyFoodCost = 30;

    private Dictionary<FishType, DropTable> fishDropTables;

    private static LootManager _instance;
    public static LootManager Instance => _instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        fishDropTables = new Dictionary<FishType, DropTable>();

        foreach (FishDropTable tableData in fishDropTableData)
        {
            fishDropTables[tableData.Type] = tableData.DropTable;
        }
    }
    

    public void SpawnDrop(FishBase fish)
    {
        DropTable dropTable = fishDropTables[fish.fishType];

        LootData dropData = dropTable.GetRandomDrop();

        var drop = LootTaxi.Instance.GetPooledLootOfType(dropData);
        
        if (drop == null) return;
        
        drop.transform.position = fish.transform.position;
        drop.transform.rotation = Random.rotation;

        drop.SetLootData(dropData);

        drop.Initiate();
    }

    public void BuyFood()
    {
        if (GameManager.Instance.Money < buyFoodCost) return;
        // :^)
        GameManager.Instance.addMoney(-buyFoodCost);
        var food = LootTaxi.Instance.GetPooledFood();
        food.SetLootData(storeFood);

        food.transform.position = aquarium.GetRandomPosition();
        food.transform.rotation = Random.rotation;
        
        food.Initiate();
    }
}
