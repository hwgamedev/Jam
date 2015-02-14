using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	Transform first;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider obj){

		if(FloorManager.current.pool.isPooled(obj.gameObject))
		{
				Debug.Log("Floor");
				FloorManager.current.floor.RemoveFirst();
				FloorManager.current.pool.DestroyObject (obj.gameObject);
				FloorManager.current.floorCount--;
		}else{
			Destroy(obj.gameObject);
		}

	}
}
