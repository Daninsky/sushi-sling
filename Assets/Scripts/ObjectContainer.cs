using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContainer : MonoBehaviour
{
    public GameObject poolers;

    private ObjectPooler activePooler;

    private void Start()
    {
        ChangeActivePooler();
    }


    public void ChangeActivePooler()
    {
        int randomNumber = Random.Range(0, poolers.GetComponentsInChildren<ObjectPooler>().Length);
        activePooler = poolers.GetComponentsInChildren<ObjectPooler>()[randomNumber];
        Debug.Log("Current active pooler: " + activePooler.objectToPool);
    }

    public GameObject GetActiveObject() 
    {
        return activePooler.GetPooledObject();
    }


}
