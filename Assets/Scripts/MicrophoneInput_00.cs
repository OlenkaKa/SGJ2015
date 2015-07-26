using UnityEngine;
using System.Collections;
using System;

public class MicrophoneInput_00 : MonoBehaviour {
	
	
	bool audioExists;
	
	float startTime; // declare this outside any function
	
	
	void Start() {
		foreach (string device in Microphone.devices) {
			Debug.Log("Name: " + device);
			audioExists = false;
		}
	}
	
	void Update () {
		if (Input.GetKey("3") && audioExists == false){
			// key pressed: save the current time
			AudioSource aud = GetComponent<AudioSource>();
			aud.clip = Microphone.Start("Samson C01U", false, 2, 44100);
			Debug.Log("recording!!!!");
			StartCoroutine(AudioCutOff());
			aud.Play();
			audioExists = true;
		}
	}
	IEnumerator AudioCutOff() {
		yield return new WaitForSeconds(1.0f);
		Start ();
	}
	
}
