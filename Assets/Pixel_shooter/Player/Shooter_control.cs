using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooter_control : MonoBehaviour
{
	public static Shooter_control current;
	public int bullet_number;
	public ObjectPooler[] pools;
	public LinkedList<Transform> bullets = new LinkedList<Transform> ();
	public int bulletCount = 0;
	public int bullet_type;
	bool start = true;
	bool canShoot = true;
	float lastshoot_time;
	bool switch_enemy = true;
	float last_enemy_switch_time;
	// Use this for initialization
	void Start ()
	{
		current = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float dx = 0;
		float dy = 0;
		if (Input.GetKey (KeyCode.UpArrow)) {
			//move up
			dy = 5;
			//rigidbody.velocity =new Vector3(0,5,0);
			//transform.Translate(0,5f * Time.deltaTime,0);

		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			//rigidbody.velocity =new Vector3(0,-5,0);
			//transform.Translate(0,-5f * Time.deltaTime,0);
			dy = -5;

			//move down
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			dx = 5;
			//rigidbody.velocity =new Vector3(5,0,0);
			//transform.Translate(5f * Time.deltaTime,0,0);

			//move right
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//move left
			dx = -5;
			//rigidbody.velocity =new Vector3(-5,0,0);
			//transform.Translate(-5f * Time.deltaTime,0,0);

		}
		rigidbody.velocity = new Vector3 (dx, dy, 0);
		//faceToVec (new Vector3 (0, 0, 0));
		tryFaceEnemy ();
		GameObject shooter = GameObject.Find ("Shooter");
		if (start) {

			for (int i = 0; i < 1; i++) {
				//type = getType ();
				Transform bullet = pools [bullet_type].GetPooledObject (
					shooter.transform.position, 
					Quaternion.identity, 
					true
				).transform;
				//nextPosition.z += 10f;
				bullet.transform.localEulerAngles = shooter.transform.localEulerAngles;
				bullet.Translate (0, 1, 0);
				bullets.AddLast (bullet);
				bulletCount ++;
			}
			start = false;
		}
		if (Time.realtimeSinceStartup - lastshoot_time > 0.4) {
			canShoot = true;
		}
		if (bulletCount < bullet_number && canShoot) {
			lastshoot_time = Time.realtimeSinceStartup;
			Transform bullet = pools [bullet_type].GetPooledObject (
				shooter.transform.position, 
				Quaternion.identity, 
				true
			).transform;
			//nextPosition.z += 10f;
			bullet.transform.localEulerAngles = shooter.transform.localEulerAngles;
			bullet.Translate (0, 1, 0);
			bullets.AddLast (bullet);
			bulletCount ++;
			canShoot = false;
		}


	}

	void tryFaceEnemy ()
	{



		try {

			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Respawn");
			GameObject nearest = null;
			GameObject shooter = GameObject.Find ("Shooter");
			float min_distance = float.MaxValue;
			//Debug.Log(enemies.Length);
			foreach (GameObject e in enemies) {
				Enemy_control.current.chasePlayer (e);
				float dist = distance(shooter.transform.position, e.transform.position);
				if (dist < min_distance && e.activeInHierarchy) {
					min_distance = dist;
					nearest = e;
				}
			}
			if (Time.realtimeSinceStartup - last_enemy_switch_time > 0.3) {
				switch_enemy = true;
			}
			if (switch_enemy) {
				if (nearest != null) {
					//Debug.Log(nearest.transform.position);
					//Debug.Log(nearest.name);

					faceToVec (nearest.transform.position);
					last_enemy_switch_time = Time.realtimeSinceStartup;
					//switch_enemy = false;
				}
			}

		} catch (UnityException excpt) {

		}
		
	}

	float distance(Vector3 vec1 , Vector3 vec2){
		float dx = vec1.x - vec2.x;
		float dy = vec1.y - vec2.y;
		return Mathf.Sqrt (dx * dx + dy * dy);
	}

	void faceToVec (Vector3 vec)
	{
		GameObject shooter = GameObject.Find ("Shooter");
		float x1 = shooter.transform.position.x;
		float y1 = shooter.transform.position.y;
		float x2 = vec.x;
		float y2 = vec.y;
		//Debug.Log (getAngle (x1,y1,x2,y2));
		GameObject.Find ("Shooter").transform.rotation = Quaternion.Euler (new Vector3 (0, 0, getAngle (x1, y1, x2, y2)));
	}

	float getAngle (float x1, float y1, float x2, float  y2)
	{
		float dx = x1 - x2;
		float dy = y1 - y2;
		float distance = Mathf.Sqrt (dx * dx + dy * dy);


		if (distance == 0) {
			return 0;
		}

		float angle = 0;
		
		if (dx > 0 && dy > 0) {
			angle = Mathf.Acos (dx / distance) / Mathf.PI * 180 + 90;
		}
		
		if (dx > 0 && dy < 0) {
			angle = Mathf.Acos (-dy / distance) / Mathf.PI * 180;
		}
		
		if (dx < 0 && dy > 0) {
			angle = Mathf.Acos (dy / distance) / Mathf.PI * 180 + 180;
		}
		
		if (dx < 0 && dy < 0) {
			angle = Mathf.Acos (dy / distance) / Mathf.PI * 180 + 180;
		}

		return angle;
	}

	public void tryDestroy (GameObject obj)
	{
		int l = pools.Length;
		for (int i=0; i<l; i++) {
			pools [i].DestroyObject (obj);
		}
	}

}
