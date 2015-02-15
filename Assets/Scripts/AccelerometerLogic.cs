using UnityEngine;
using System.Collections;

public class AccelerometerLogic : TouchLogic {

	public float moveThreshold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		if (Input.acceleration.x > moveThreshold) 
		{
			OnTiltRight(Input.acceleration.x);
		}

		if (Input.acceleration.x < -moveThreshold) 
		{
			OnTiltLeft(Input.acceleration.x);
		}
	}

	public virtual void OnTiltRight (float value)
	{

	}
	public virtual void OnTiltLeft (float value)
	{
		
	}
}
