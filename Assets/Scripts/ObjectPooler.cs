using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private GameManager m_gameManager;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }
    }

    private void Update()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is active, betting is true 
            if (pooledObjects[i].activeInHierarchy)
            {
                m_gameManager.isBetting = true;
            }
        }

        DeactivateOutOfBounds();
    }

    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // otherwise, return null   
        return null;
    }

    void DeactivateOutOfBounds()
    {
        // Iterate through list of pooled objects
        for (int i = 0; i < ObjectPooler.SharedInstance.pooledObjects.Count; i++)
        {
            GameObject obj = ObjectPooler.SharedInstance.pooledObjects[i];
            // Count all active objects in hierarchy
            if (obj.activeInHierarchy && obj.transform.position.y < -80)
            {
                obj.SetActive(false);
                m_gameManager.isBetting = false;
            }
        }
    }
}
