using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public Transform player;
	public float moveSpeed = 5f;
	public float maxSpeed = 10;
	public float jumpForce = 10f;
	public float laneSwap = 10f;
	public int playerHealth = 3;
	public static float distanceTraveled = 0;
	public float moveThreshold = 0.2f;
	public int i_comfort = 0;
	public int angle = 30;
	public Slider slider;
	public Animator anim;
	public CapsuleCollider[] col;
	Vector3 colSize;
	bool collided = false;
	Vector2 v2_current_position;
	Vector2 v2_previous_position;
	float touchDelta;
	int currTouch = 0;


	void Awake()
	{
		player = this.transform;
		anim = GetComponent<Animator> ();
		col = gameObject.GetComponents<CapsuleCollider> ();

	}
	// Use this for initialization
	void Start () {
		Debug.Log ("Working");
	}

	void Update()
	{
		slider.value = moveSpeed;
		if (Input.acceleration.x > moveThreshold) {

			OnTiltRight (Input.acceleration.x);
		} else if (Input.acceleration.x <= -moveThreshold) {
			OnTiltLeft (Input.acceleration.x);
		} else
			OriginalRotation ();
		if (Input.touchCount == 1) 
		{
			if (Input.touchCount == 1 && Input.GetTouch (currTouch).phase == TouchPhase.Began) {
				v2_previous_position = Input.GetTouch (currTouch).position;
			}
			if (Input.touchCount == 1 && Input.GetTouch (currTouch).phase == TouchPhase.Ended) 
			{
				v2_current_position = Input.GetTouch (currTouch).position;
			
				touchDelta = v2_current_position.magnitude - v2_previous_position.magnitude; 
				if (Mathf.Abs (touchDelta) > i_comfort) 
				{
					if (touchDelta > 0) {
						if ((v2_current_position.x - v2_previous_position.x) < (v2_current_position.y - v2_previous_position.y)) {
							Debug.Log ("Swipe Up");
							OnSwipeUp ();
						}
					}
					if (touchDelta < 0) {
						if ((v2_current_position.x - v2_previous_position.x) > (v2_current_position.y - v2_previous_position.y)) {
							Debug.Log ("Swipe Down");
							OnSwipeDown ();
						}
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider obj)
	{
		if (obj.tag == "Obstacle") 
		{
			Hit (3);
			collided = true;
		}
	}
	void OnTriggerExit(Collider obj)
	{
		if (obj.tag == "Obstacle") 
		{
			collided = false;
		}
	}
	void OnTiltRight(float value)
	{

		player.eulerAngles = new Vector3(0f, 180 + angle, 0f);
		player.Translate(laneSwap * -value * Time.deltaTime, 0, 0);
	}

	void OnTiltLeft(float value)
	{
		player.eulerAngles = new Vector3(0f, 180 - angle, 0f);
		player.Translate(laneSwap * -value * Time.deltaTime, 0, 0);
	}

	void OriginalRotation()
	{
		player.eulerAngles = new Vector3(0f, 180f, 0f);
	}

	void OnSwipeUp()
	{

		player.rigidbody.AddForce (new Vector3(0,jumpForce));
		anim.SetTrigger ("Jump");
	}

	void OnSwipeDown()
	{
		//play animation
		anim.SetTrigger ("Slide");
		foreach (CapsuleCollider c in col)
		{
			c.height -= 1;
			c.center = new Vector3(0, 1, 0);
		}
		StartCoroutine ("SlideFinish");
	}

	IEnumerator SlideFinish()
	{
		yield return new WaitForSeconds(1.3f);
		foreach (CapsuleCollider c in col)
		{
			c.height += 1;
			c.center = new Vector3(0, 1.5f, 0);
		}
	}

	void FixedUpdate()
	{
		distanceTraveled = transform.localPosition.z;
		increaseSpeed ();
		player.Translate (0, 0, -moveSpeed * Time.deltaTime);
	}

	void increaseSpeed()
	{

			if (moveSpeed > maxSpeed)
				moveSpeed = maxSpeed;
			else
			moveSpeed += Time.deltaTime;

	}
	public void Hit(int value)
	{
		moveSpeed -= value;
		if (moveSpeed <= 0)
			moveSpeed = 1;
	}



}

