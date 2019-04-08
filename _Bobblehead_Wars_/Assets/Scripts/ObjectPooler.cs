using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//generic object pooler
[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;//number of objects to pool
    public bool shouldExpand;//whether the pool should be expanded at runtime
    public GameObject objectToPool;//type og object to pool
}

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;//allows other scripts to access this
    //on awake set the instance
    private void Awake()
    {
        SharedInstance = this;
    }
    //want a list of the type of objects in the pool
    public List<ObjectPoolItem> itemsToPool;
    //and a list of the actual pooled objects
    public List<GameObject> pooledObjects;

    // Use this for initialization
    void Start () {
        //setup the pool
        pooledObjects = new List<GameObject>();
        //setup for each type of item
        foreach(ObjectPoolItem item in itemsToPool)
        {
            for(int i = 0; i < item.amountToPool; ++i)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }
	//need to be able to get item from the pool
    public GameObject GetPooledObject(string tag)
    {
        //the pool is NOT SORTED so you have to loop through the whole thing
        for (int i = 0; i < pooledObjects.Count; ++i)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        //if no objects are available see if we can expand the pool
        foreach(ObjectPoolItem item in itemsToPool)
        {
            if(item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    //we can expand
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
                else
                {
                    return null;
                }
            }
        }
        return null;//default case
    }

    // Update is called once per frame
    void Update () {
		
	}
}
