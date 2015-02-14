using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform target;
	public float distance = 5f;


	// Use this for initialization
	void Start ()
	{

	}
	
	void FixedUpdate ()
	{		
		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z - distance);
	}
}
