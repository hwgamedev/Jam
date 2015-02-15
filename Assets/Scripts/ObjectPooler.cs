using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler current;
	public GameObject pooledObject;
	public int pooledAmount = 5;
	public bool willGrow = false;


	List<GameObject> pooledObjects;

	void Awake(){
		current = this;
	}
	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject> ();
		for(int i = 0; i < pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject(Vector3 position, Quaternion rotation, bool setActive)
	{
		for(int i = 0; i < pooledObjects.Count; i++)
		{
			if(!pooledObjects[i].activeInHierarchy)
			{
				pooledObjects[i].transform.position = new Vector3(pooledObject.transform.position.x + position.x, position.y, position.z);
				pooledObjects[i].transform.rotation = rotation;
				pooledObjects[i].SetActive(setActive);
				return pooledObjects[i];
			}
		}
		if(willGrow)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			pooledObjects.Add(obj);
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			obj.SetActive(setActive);
			return obj;
		}
		return null;
	}

	public bool isPooled(GameObject obj)
	{
		foreach(GameObject pooled in pooledObjects)
		{
			if (pooled == obj)
				return true;
		}
		return false;
	}

	public void DestroyObject(GameObject obj)
	{
		obj.SetActive (false);
	}

	public int getCount(){
		return pooledObjects.Count;
	}

}
