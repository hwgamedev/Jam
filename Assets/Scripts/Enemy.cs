using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Vector2 scale;
	Vector2 max;
	Vector2 min;
	public Slider s;
	public RectTransform i;
	float value;
	// Use this for initialization
	void Start () {
		max = new Vector2(43.5f, 5.8f);
		min = new Vector2 (1,1);
	}
	float x;
	float y;
	// Update is called once per frame
	void Update () {
		//i.sizeDelta = scale;
		if (s.value > 1)
			i.localScale = new Vector3 (s.value, s.value);
		else
			i.localScale = new Vector3 (min.x, min.y);
			
		if (s.value < min.x)
			x = min.x;
		if (s.value > max.x)
			x = max.x;
		if(s.value < min.y)
			y = min.y;
		if(s.value > max.y)
			y = max.y;
		if (PlayerControl.current.moveSpeed <= 1.3f)
		{
			PlayerControl.current.slider.value = 0;
			Time.timeScale = 0;
		}
	}


}
