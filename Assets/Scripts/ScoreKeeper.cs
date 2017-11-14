using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	public static int score = 0;
	private Text text;


	// Use this for initialization
	void Start ()
	{

		text = GetComponent<Text> ();
		Reset ();
	}

	public void Score (int points)
	{
		Debug.Log ("Scored points");
		score += points;
		text.text = score.ToString ();
	}
	public static void Reset ()
	{
		score = 0;

	}
}
