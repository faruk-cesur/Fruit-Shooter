using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance => _instance ??= FindObjectOfType<ObjectPool>();

    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> PooledObject;
        public GameObject ObjectPrefab;
        public int PoolSize;
    }

    public Pool[] Pools = null;

    private void Awake()
    {
        MakeSingleton();

        for (int i = 0; i < Pools.Length; i++)
        {
            Pools[i].PooledObject = new Queue<GameObject>();
            for (int j = 0; j < Pools[i].PoolSize; j++)
            {
                GameObject obj = Instantiate(Pools[i].ObjectPrefab);
                obj.SetActive(false);
                Pools[i].PooledObject.Enqueue(obj);
            }
        }
    }

    private void MakeSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPooledObject(int objectType)
    {
        if (objectType >= Pools.Length) return null;
        if (Pools[objectType].PooledObject.Count == 0)
            AddSizePool(5, objectType);
        GameObject obj = Pools[objectType].PooledObject.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void SetPooledObject(GameObject pooledObject, int objectType)
    {
        if (objectType >= Pools.Length) return;
        Pools[objectType].PooledObject.Enqueue(pooledObject);
        pooledObject.SetActive(false);
    }

    public void AddSizePool(float amount, int objectType)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(Pools[objectType].ObjectPrefab);
            obj.SetActive(false);
            Pools[objectType].PooledObject.Enqueue(obj);
        }
    }
}