using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject poolObject;

    public List<GameObject> pool;
    public int poolSize;
    public bool expanding;

    // Start is called before the first frame update
    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject) Instantiate(poolObject);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        if (expanding)
        {
            GameObject obj = Instantiate(poolObject);
            obj.SetActive(false);
            pool.Add(obj);
            return obj;
        }

        return null;

    }
}
