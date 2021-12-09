using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private T poolObject;

    public List<T> pool;
    public int poolSize;
    public bool expanding;

    // Start is called before the first frame update
    void Start()
    {
        pool = new List<T>();
        for (int i = 0; i < poolSize; i++)
        {
            T obj = (T) Instantiate(poolObject);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public T GetPooledObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }

        if (expanding)
        {
            T obj = Instantiate(poolObject);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
            return obj;
        }

        return null;
    }
}
