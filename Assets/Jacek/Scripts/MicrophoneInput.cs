using UnityEngine;
using System.Collections;
using System;

public class MicrophoneInput : MonoBehaviour {


	bool audioExists = false;

	float startTime; // declare this outside any function


	void Start() {
		foreach (string device in Microphone.devices) {
				Debug.Log("Name: " + device);
			}
	}

	void Update () {
		if (Input.GetKey("1")){
			// key pressed: save the current time
			AudioSource aud = GetComponent<AudioSource>();
			aud.clip = Microphone.Start("null", false, 2, 44100);
			Debug.Log("recording!!!!");
			aud.Play();
		}
	
	}
}
