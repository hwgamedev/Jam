using UnityEngine;
using System.Collections;

public class Camara_lock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject shooter = GameObject.Find ("Shooter");
		transform.position= new Vector3(shooter.transform.position.x, shooter.transform.position.y,-20 );
	}
}
