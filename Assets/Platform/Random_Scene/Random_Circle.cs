using UnityEngine;
using System.Collections;

public class Random_Circle : MonoBehaviour {

	public float spin_speed=20f;
	public float min_scale=1f;
	public float max_scale=2f;
	// Use this for initialization
	void Start () {
		float r = get_scale ();
		this.transform.localScale = new Vector3 (r, 0.1f, r);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (0,spin_speed * Time.deltaTime, 0);

	}

		float get_scale(){
			return Random.Range (this.min_scale, this.max_scale);
		}
}
