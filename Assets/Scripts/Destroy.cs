﻿using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	Transform first;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider obj)
	{
		Debug.Log ("Destroy");
		foreach (ObjectPooler pool in ObstacleManager.current.pools) 
		{
			if(pool.isPooled(obj.gameObject))
			{
				if(obj.tag == "Obstacle")
				{
					Debug.Log("Obstacle");
					ObstacleManager.current.obstacles.RemoveFirst();
					pool.DestroyObject (obj.gameObject);
					ObstacleManager.current.obstacleCount--;
				}

			}else{
				//Destroy(obj.gameObject);
			}

		}
		if(obj.tag == "Coin")
		{
			Debug.Log("Coin");
			CoinManager.current.coins.RemoveFirst();
			CoinManager.current.pool.DestroyObject (obj.gameObject);
			CoinManager.current.coinCount--;
			if(this.gameObject.tag == "Player")
			ScoreManager.score += 50;
		}
		
		
	}
}
