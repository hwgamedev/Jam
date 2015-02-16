using UnityEngine;
using System.Collections;

public class hieght : MonoBehaviour {
	Transform t;
	// Use this for initialization
	void Start () {
		t = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		t.position = new Vector3(0, -4, t.position.z);
	}
}
