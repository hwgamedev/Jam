using UnityEngine;
using System.Collections.Generic;

public class Obstacles_control : MonoBehaviour {
	public Transform[] obstacles;

	public float min_distance;
	public int max_obstacles;

	public float recycleOffset;

	public Vector3 startPosition;
	private Vector3 nextPosition;

	private Queue<Transform> objectQueue;
	

	// Use this for initialization
	private Transform get_random_obstacle(){
		int i = Random.Range(0, obstacles.Length);

		return obstacles[i];
	}

	void Start () {
		objectQueue = new Queue<Transform>(max_obstacles);
		for(int i = 0; i < max_obstacles; i++){
			objectQueue.Enqueue((Transform)Instantiate(get_random_obstacle()));
		}
		nextPosition = startPosition;
		for(int i = 0; i < max_obstacles; i++){
			Recycle();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(objectQueue.Peek().localPosition.z + recycleOffset < PlayerControl.distanceTraveled){
			Recycle();
		}
	}

	private void Recycle () {
		

		Vector3 position = nextPosition;
		

		Transform o = objectQueue.Dequeue();
		Destroy (o.gameObject);
		o =(Transform) Instantiate(get_random_obstacle ());
		//o.localScale = scale;
		o.localPosition = new Vector3(o.localPosition.x, o.localPosition.y, position.z);
		Debug.Log (o.position.x);
		objectQueue.Enqueue(o);
		
		nextPosition += new Vector3(
			0,
			0,
			min_distance);
		
	}
}
