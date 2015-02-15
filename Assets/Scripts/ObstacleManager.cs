using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {

	public static ObstacleManager current;
	public ObjectPooler[] pools;
	public Vector3 startPosition;
	public Vector3 nextPosition;
	public int maxObstacles = 10;
	public Transform firstObstacle;
	public Transform lastObstacle;
	public LinkedList<Transform> obstacles = new LinkedList<Transform>();
	public int obstacleCount = 0;
	int type;
	bool start = true;

	void Awake()
	{
		current = this;
	}
	// Use this for initialization
	void Start () {

	}
	
	void Update () {

		if (start)
		{
			for(int i = 0; i < maxObstacles; i++)
			{
				type = getType ();
				Transform obstacle = pools[type].GetPooledObject(startPosition + nextPosition, Quaternion.identity, true).transform;
				nextPosition.z += 10f;
				obstacles.AddLast(obstacle);
				obstacleCount ++;
			}
			start = false;
		}
		firstObstacle = obstacles.First.Value;
		lastObstacle = obstacles.Last.Value;
		if(obstacleCount < maxObstacles)
		{
			Debug.Log("count ");
			type = getType ();
			Transform obstacle = pools[type].GetPooledObject(startPosition + nextPosition, Quaternion.identity, true).transform;
			nextPosition.z += 10f;
			obstacles.AddLast(obstacle);
			obstacleCount++;
		}
		
		
	}

	int getType()
	{
		return Random.Range (0, pools.Length);
	}
}
