using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour {
	
	public static CoinManager current;
	public ObjectPooler pool;
	public Transform prefab;
	public int maxCoin = 2;
	public int coinCount;
	public Vector3 startPosition;
	public Vector3 nextPosition;
	public Transform firstCoin;
	public Transform lastCoin;
	public LinkedList<Transform> coins = new LinkedList<Transform>();
	bool start = true;
	
	void Awake(){
		current = this;
	}
	// Use this for initialization
	void Start () {
		//startPosition = new Vector3 (.5f,-1f,6.5f);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(start)
		{
			for(int i = 0; i < maxCoin; i++)
			{
				nextPosition.x = Random.Range(-3, 3);
				for(int j = 0; j < 5; i++)
				{
					nextPosition.x = Random.Range(-3, 3);
					Transform c = pool.GetPooledObject(startPosition + nextPosition, Quaternion.identity, true).transform;
					nextPosition.z += 10f;
					nextPosition.y = -1.5f;
					coins.AddLast(c);
					start = false;
					coinCount ++;
				}
			}
		}
		firstCoin = coins.First.Value;
		lastCoin = coins.Last.Value;
		if(!start && coinCount < maxCoin)
		{
			nextPosition.x = Random.Range(-3, 3);
			Transform c = pool.GetPooledObject(startPosition + nextPosition, Quaternion.identity, true).transform;
			nextPosition.z += 10f;
			nextPosition.y = -1.5f;

			coins.AddLast(c);
			coinCount++;
		}
		
		
	}
	
}
