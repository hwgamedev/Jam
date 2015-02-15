using UnityEngine;
using System.Collections;

public class RoomScript : MonoBehaviour {

	public GameObject room_prefab;
	public int level_number;
	public bool door_enabled;
	// Use this for initialization
	void Start () {
		int start_i = 0 - level_number +1;
		int end_i =0+level_number -1;
		int i;
		int j;
		for (i=start_i; i<=end_i; i++) {
			for (j=start_i; j<=end_i; j++) {
				Instantiate(room_prefab, new Vector3(i*30,j*30,0),Quaternion.identity);

			}
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
