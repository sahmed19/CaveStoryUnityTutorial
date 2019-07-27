using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

    [System.Serializable]
	public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPoolManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            GameObject parent = new GameObject(pool.tag.ToUpper());
            parent.transform.SetParent(transform);

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.name = pool.tag + " ID " + i;
                obj.SetActive(false);
                obj.transform.SetParent(parent.transform);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);

        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, bool ignoreIfActive = false)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Tag [" + tag + "] not found!");
            return null;
        }

        if(poolDictionary[tag].Peek().activeSelf && ignoreIfActive)
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
        
    }

}
