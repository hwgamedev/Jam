using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScirpt : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		
	}

	public void Pause()
	{
		if(Time.timeScale > 0)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
	public void Restart()
	{
		Application.LoadLevel (Application.loadedLevel);
		Time.timeScale = 1;
	}
}
