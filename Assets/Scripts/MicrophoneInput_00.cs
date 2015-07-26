using UnityEngine;
using System.Collections;
using System;

public class MicrophoneInput_00 : MonoBehaviour {
	
	
	bool audioExists;
	
	float startTime; // declare this outside any function

	private MoraleManager_00 moraleManager;

	
	void Start() {

			audioExists = false;
			moraleManager = GameObject.FindGameObjectWithTag ("MoraleManager").GetComponent<MoraleManager_00>();
		

	}
	
	void Update () {
		if (Input.GetKey("3") && audioExists == false){
			// key pressed: save the current time
			AudioSource aud = GetComponent<AudioSource>();
			aud.clip = Microphone.Start("Samson C01U", true, 2, 44100);
			Debug.Log("recording!!!!");
			StartCoroutine(AudioCutOff());
			aud.Play();

		}
		if (audioExists == true) {
			AudioSource rec = GetComponent<AudioSource>();
			rec.Stop();
		}
	}
	IEnumerator AudioCutOff() {
		yield return new WaitForSeconds(3.0f);
		audioExists = true;
		Start ();
	}
	
}
