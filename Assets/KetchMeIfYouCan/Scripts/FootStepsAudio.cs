using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsAudio : MonoBehaviour {

    [SerializeField] AudioSource myAudio;
	bool walking;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (walking)
			walkingSteps();
    }

	public void walkingSteps()
	{
		if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
		{
			myAudio.Play();
			//Debug.Log("Down Horizontal: Playing footsteps");
		}

		if (Input.GetButtonDown("Vertical") && Input.GetButtonDown("Vertical"))
		{
			myAudio.Play();
			//Debug.Log("Down Horizontal and Vertical: Playing footsteps");
		}

		if (Input.GetButtonUp("Horizontal") && !Input.GetButton("Vertical"))
		{
			myAudio.Stop();
			//Debug.Log("Stop Steps");
		}

		if (Input.GetButtonUp("Vertical") && !Input.GetButton("Horizontal"))
		{
			myAudio.Stop();
			//Debug.Log("Stop Steps");
		}
		walking = false;
	}
}
