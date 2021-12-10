using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(FishBase))]
public class FishBaby : MonoBehaviour
{
    [SerializeField]
    private float growTime;
    
    private FishBase attachedFish;
    private FishData babyStats;

    private Vector3 grownScale;

    public void InitBaby()
    {
        attachedFish = GetComponent<FishBase>();
        FishData parentData = attachedFish.FishStats;
        babyStats = Instantiate(parentData);
        babyStats.RandomizeStats(1.5f);
        
        attachedFish.SetFishData(babyStats);
        attachedFish.FishStats = babyStats;

        growTime = Random.Range(growTime / 2, growTime * 2);

        Vector3 localScale = transform.localScale;
        grownScale = localScale + localScale * Random.value;
        localScale *= 0.5f;
        transform.localScale = localScale;

        StartCoroutine(GrowUpTimer());
    }

    IEnumerator GrowUpTimer()
    {
        float elapsedTime = 0f;
        Vector3 currentScale = transform.localScale;
        while (elapsedTime < growTime)
        {
            transform.localScale = Vector3.Lerp(currentScale, grownScale, (elapsedTime / growTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localScale = grownScale;
        
        Destroy(this);
        
        yield return null;
    }
}
