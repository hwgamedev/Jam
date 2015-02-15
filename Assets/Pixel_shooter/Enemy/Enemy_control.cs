using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_control : MonoBehaviour {
	public static Enemy_control current;
	//public Transform prefab;
	public int max_enemy_number;
	public ObjectPooler[] pools;
	public LinkedList<Transform> enemies = new LinkedList<Transform>();
	public int enemiesCount = 0;
	public int level_number;
	public float speed =0.3f;
	int type;
	bool start = true;
	// Use this for initialization
	void Start () {
		current = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (start)
		{
			for(int i = 0; i < max_enemy_number; i++)
			{
				type = getType ();
				Transform enemy = pools[type].GetPooledObject(getRandomPosition(), Quaternion.identity, true).transform;
				//nextPosition.z += 10f;
				enemy.gameObject.tag="Respawn";

				enemies.AddLast(enemy);
				enemiesCount ++;
			}
			start = false;
		}

		if(enemiesCount < max_enemy_number)
		{
			//Debug.Log("count ");
			type = getType ();
			Transform enemy = pools[type].GetPooledObject(getRandomPosition(), Quaternion.identity, true).transform;
			//nextPosition.z += 10f;

			enemy.gameObject.tag="Respawn";
			enemies.AddLast(enemy);
			enemiesCount++;
		}


	}

	public void chasePlayer(GameObject obj){
		
		GameObject shooter = GameObject.Find ("Shooter");
		faceToVec (obj, shooter.transform.position);
		obj.transform.Translate (0, speed* Time.deltaTime,0);
		obj.rigidbody.AddForce (0, speed * Time.deltaTime, 0);
	}

	int getType()
	{
		return Random.Range (0, pools.Length);
	}

	Vector3 getRandomPosition()
	{
		float x = Random.Range ((float)0.0,(float) (level_number) * 30)-(level_number) * 15;
		float y = Random.Range ((float)0.0,(float) (level_number) * 30)- (level_number) * 15;
		float z = 0;
		Vector3 result= new Vector3 (x, y, z);
		if (Vector3.Distance (result, Shooter_control.current.transform.position) > 3) {
			return result;
		} else {
			return getRandomPosition ();
		}
	}

	
	void faceToVec (GameObject obj,Vector3 vec)
	{

		float x1 = obj.transform.position.x;
		float y1 = obj.transform.position.y;
		float x2 = vec.x;
		float y2 = vec.y;
		//Debug.Log (getAngle (x1,y1,x2,y2));
		obj.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, getAngle (x1, y1, x2, y2)));
	}
	
	float getAngle (float x1, float y1, float x2, float  y2)
	{
		float dx = x1 - x2;
		float dy = y1 - y2;
		float distance = Mathf.Sqrt (dx * dx + dy * dy);
		
		
		if (distance == 0) {
			return 0;
		}
		
		float angle=0;
		
		if (dx > 0 && dy > 0) {
			angle = Mathf.Acos (dx / distance) / Mathf.PI * 180 +90;
		}
		
		if (dx > 0 && dy < 0) {
			angle = Mathf.Acos (-dy / distance) / Mathf.PI * 180;
		}
		
		if (dx < 0 && dy > 0) {
			angle = Mathf.Acos (dy / distance) / Mathf.PI * 180 +180;
		}
		
		if (dx < 0 && dy < 0) {
			angle = Mathf.Acos (dy/ distance) / Mathf.PI * 180 +180;
		}
		
		return angle;
	}

	public void tryDestroy(GameObject obj){
		int l = pools.Length;
		for (int i=0 ; i<l;i++){
			pools[i].DestroyObject(obj);
		}
	}
}

