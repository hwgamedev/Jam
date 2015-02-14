using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorManager : MonoBehaviour {

	public static FloorManager current;
	public ObjectPooler pool;
	public Transform prefab;
	public int maxFloor = 2;
	public int floorCount;
	public Vector3 startPosition;
	public Vector3 nextPosition;
	public Transform firstFloor;
	public Transform lastFloor;
	public LinkedList<Transform> floor = new LinkedList<Transform>();
	bool start = true;

	void Awake(){
		current = this;
	}
	// Use this for initialization
	void Start () {
		startPosition = new Vector3 (.5f,-1f,6.5f);

	}
	
	// Update is called once per frame
	void Update () {
		if(start)
		{
			for(int i = 0; i < maxFloor; i++)
			{
				Transform ice = pool.GetPooledObject(startPosition + nextPosition, Quaternion.identity, true).transform;
				nextPosition.z += 10f;
				floor.AddLast(ice);
				start = false;
				floorCount ++;
			}
		}
		firstFloor = floor.First.Value;
		lastFloor = floor.Last.Value;
		if(!start && floorCount < maxFloor)
		{
			Transform ice = pool.GetPooledObject(startPosition + nextPosition, Quaternion.identity, true).transform;
			nextPosition.z += 10f;
			floor.AddLast(ice);
			floorCount++;
		}

	
	}

}
