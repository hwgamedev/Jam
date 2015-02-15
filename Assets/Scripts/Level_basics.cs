using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_basics : MonoBehaviour
{
	
	public Transform prefab;
	public int numberOfObjects;
	public float lengthOffset;
	public float recycleOffset;
	public Vector3 startPosition;
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	void Start ()
	{
		objectQueue = new Queue<Transform> (numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++) {
			objectQueue.Enqueue ((Transform)Instantiate (prefab));
		}
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) {
			Recycle ();
		}
	}
	
	void Update ()
	{
		try {
			if (objectQueue.Peek ().localPosition.z + recycleOffset < PlayerControl.distanceTraveled) {
				Recycle ();
			}
		} finally {

		}
	}
	
	private void Recycle ()
	{
		Vector3 scale = new Vector3(
			1,
			1,
			lengthOffset);
		
		Vector3 position = nextPosition;
		
		
		Transform o = objectQueue.Dequeue ();
		o.localScale = scale;
		o.localPosition = position;
		objectQueue.Enqueue (o);
		
		nextPosition += new Vector3 (
			0,
			0,
			lengthOffset);
		
	}
}