using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// a simple stopwatch GUI
// made by McFunkypants for Ketchmeifyoucan
// a http://gamkedo.club game

public class stopwatchGUI : MonoBehaviour {

	public Image stopwatchBG;
	public Text stopwatchTXT;

	public float startTime;
	public float totalTime;

	void Start () {

		startTime = Time.deltaTime;
		totalTime = 0;

	}

	void Update () {

		totalTime = Time.time;

		//Debug.Log("stopwatch time is " + totalTime);

		int m;
		int s;
		m = (int)(totalTime / 60);
		s = (int)(totalTime - (m * 60));

		//Debug.Log("stopwatch time is " + totalTime);
		//Debug.Log("m:s " + m+":"+s);

		if (stopwatchTXT != null)
			stopwatchTXT.text = m + ":" + (s<10?"0":"") + s;

	}
}
