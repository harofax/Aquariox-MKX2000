using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishManager : MonoBehaviour
{
    // Yes I know... Fish is plural but I want to make it easier to distinguish 
    [SerializeField]
    private FishBase[] fishies;

    [SerializeField]
    private Aquarium aquarium;

    // Start is called before the first frame update
    void Start()
    {
        int numStartFish = GameManager.Instance.startFish;

        for (int i = 0; i < numStartFish; i++)
        {
            int fishType = Random.Range(0, fishies.Length);

            Quaternion startRot = Quaternion.identity;

            // coinflip to rotate the other way
            startRot.eulerAngles = Random.Range(0, 2) == 0 ? 
                startRot.eulerAngles + 180f * Vector3.up : 
                startRot.eulerAngles;

            Vector3 pos = aquarium.GetRandomPosition();
            
            FishBase fish = Instantiate(fishies[fishType], pos, startRot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
