using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CoinType
{
    Small,
    Medium,
    Large,
    Rare,
}
[System.Serializable]
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

        foreach (FishDropTable tableData in fishDropTableData)
        {
            fishDropTables[tableData.Type] = tableData.DropTable;
        }
    }
    

    public void SpawnDrop(FishBase fish)
    {
        DropTable dropTable = fishDropTables[fish.fishType];

        LootData dropData = dropTable.GetRandomDrop();

        var drop = LootTaxi.Instance.GetPooledObjectOfType(dropData).GetComponent<Loot<LootData>>();

        drop.transform.position = fish.transform.position;
        drop.transform.rotation = Random.rotation;

        drop.SetLootData(dropData);

        drop.Initiate();
    }
}
