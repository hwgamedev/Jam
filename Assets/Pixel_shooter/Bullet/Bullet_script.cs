using UnityEngine;
using System.Collections;

public class Bullet_script : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		transform.Translate (0, 1, 0);
	}
	// Update is called once per frame
	void OnBecameInvisible() {  
		// Destroy the bullet 
		Shooter_control sc = Shooter_control.current;
		sc.tryDestroy (gameObject);
		sc.bulletCount--;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name.Contains ("Shooter") == false) {
			Enemy_control.current.tryDestroy (gameObject);
		}
	}

}
