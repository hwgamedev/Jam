using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public static float score;        // The player's score.
	public Text text;               // Reference to the Text component.
	
	void Awake ()
	{
		// Set up the reference.
		//text = GetComponent <Text> ();
		// Reset the score.
		score = 0;
	}
	
	
	void Update ()
	{
		int temp = Mathf.FloorToInt(Time.deltaTime * 100);
		score += temp;
		// Set the displayed text to be the word "Score" followed by the score value.
		text.text = "Score: " + score;
	}
	
	public void increaseScore(int value)
	{
		score += value;
	}
}