using UnityEngine;
using System.Collections;

public class PlayerControl : TouchLogic {

	public Transform player;
	public float moveSpeed = 10f;
	public float jumpForce = 100f;
	public float laneSwap = 10f;
	public int playerHealth = 3;

	void Awake()
	{
		player = this.transform;
	}
	// Use this for initialization
	void Start () {
		Debug.Log ("Working");
	}


	public override void OnSwipeLeft()
	{
		Vector3 newPosition = new Vector3 (player.position.x - laneSwap, player.position.y, player.position.z);
		player.position = Vector3.Lerp(player.position, newPosition, 5);
	}

	public override void OnSwipeRight()
	{
		Debug.Log ("Swipe Right");

		Vector3 newPosition = new Vector3 (player.position.x + laneSwap, player.position.y, player.position.z);
		player.position = Vector3.Lerp(player.position, newPosition, 1 );
	}
}
