using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSpawner : MonoBehaviour
{
    //define pool
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    //create a list of pools
    public List<Pool> pools;
    //create a dictionary of pools
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        //for each pool in pool list
        foreach (Pool pool in pools) {
            //create a list of game objects and add them to the pool
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) {
                GameObject obj= Instantiate(pool.prefab);
                obj.SetActive(true);
                objectPool.Enqueue(obj);
            }
            //add the pool to the dictionary
            poolDictionary.Add(pool.tag,objectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
