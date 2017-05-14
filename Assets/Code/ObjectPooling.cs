using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour {


    

    // Type of Object we want to pooling
    public GameObject typeObject;

    [HideInInspector]public List<GameObject> pooledObjects;

    //The original amount of objects
    public int pooledAmount = 5;

    // if i let my object pooling to grow
    public bool willGrow = true;

    

    void Start () {

        //Instantiate the GameObjects
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++ )
        {
            GameObject obj = Instantiate(typeObject) as GameObject;
           
            obj.SetActive(false);
            
            
            // make pooledObjects[i] child of the parent GameObject ObjectPooling
            obj.transform.parent = this.gameObject.transform;
           

            pooledObjects.Add(obj);
        }
        

        
	}
	
    public virtual GameObject GetPooledObject()
    {
       
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = Instantiate(typeObject) as GameObject;
            obj.transform.parent = this.gameObject.transform;
            pooledObjects.Add(obj);

            return obj;
        }

        return null;
    }

}
