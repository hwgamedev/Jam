using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Env_control : MonoBehaviour {

	public Vector3 offset ;
	public int number;

	public Vector3 RangeVec1;
	public Vector3 RangeVec2;

	public Transform prefab;
	private Queue<Transform> objectQueue;

	// Use this for initialization
	void Start () {
		objectQueue = new Queue<Transform> (number);
		for (int i = 0; i < number; i++) {
			Transform trs=(Transform)Instantiate (prefab);
			objectQueue.Enqueue (trs);
			trs.position=getRandomPos();
		}
	}

	Vector3 getRandomPos(){
		Vector3 result = new Vector3 (Random.Range(RangeVec1.x,RangeVec2.x),
		                              Random.Range(RangeVec1.y,RangeVec2.y),
		                              Random.Range(RangeVec1.z,RangeVec2.z));
		return result;
	}
	// Update is called once per frame
	void Update () {
		//this.transform.position=offset+new Vector3(0,0,  PlayerControl.distanceTraveled);
		if (objectQueue.Peek ().localPosition.z + 5 < PlayerControl.distanceTraveled) {
			Recycle ();
		}
	}

	
	private void Recycle ()
	{
	
		Vector3 position = getRandomPos()+ new Vector3(0,0,PlayerControl.distanceTraveled);
		
		
		Transform o = objectQueue.Dequeue ();

		o.localPosition = position;
		objectQueue.Enqueue (o);
		

		
	}
}
