using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingRemoveAudio : MonoBehaviour {
    [SerializeField] AudioSource myAudio;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Interact"))
        {
            myAudio.Play();
        }
        
    }
}
