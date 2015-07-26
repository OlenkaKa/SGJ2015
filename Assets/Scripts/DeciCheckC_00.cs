using UnityEngine;
using System.Collections;

public class DeciCheckC_00 : MonoBehaviour {
	
	
	public int qSamples = 1024;  // array size
	public float refValue = 0.1f; // RMS value for 0 dB
	public float rmsValue;   // sound level - RMS
	public float dbValue;    // sound level - dB
	public float volume = 2; // set how much the scale will vary
	bool success;
	public Texture winText;
	bool toggleGUI;
	private MoraleManager_00 moraleManager;
	
	private AudioSource source; 
	private float[] samples; // audio samples
	
	// Use this for initialization
	void Start () 
	{
		samples = new float[qSamples];
		success = false;
		moraleManager = GameObject.FindGameObjectWithTag ("MoraleManager").GetComponent<MoraleManager_00>();
	}
	
	// Update is called once per frame
	void Update () {
		GetVolume();
		if (rmsValue >= 0.009)
		{
			success = true;
			Debug.Log ("Success!");
			Motivated();
			moraleManager.increaseMorale(9f);
		} 
		else if(rmsValue >= 0.6)
		{
			moraleManager.increaseMorale(6f);
		}
		
		else if(rmsValue >= 0.3)
		{
			moraleManager.increaseMorale(3f);
		}
		
		else 
		{
			Debug.Log ("Failure");
		}
	}
	
	void GetVolume() {
		AudioSource recording = GetComponent<AudioSource>();
		recording.GetOutputData(samples, 0); // fill array with samples
		int i;
		float sum = 0;
		for (i=0; i < qSamples; i++){
			sum += samples[i]*samples[i]; // sum squared samples
		}
		rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
		dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
		if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
	}
	
	void OnGUI () {
		
		if (toggleGUI == true) {
			GUI.DrawTexture(new Rect(Screen.width/2 - 127, Screen.height - 32, 256, 32), winText);
			
		} 
	}
	
	void Motivated() {
		//toggleGUI = true;
		//yield return new WaitForSeconds(2);
		//toggleGUI = false;
		//script = GetComponent(DeciCheck);
		//script.enabled = false;
		
	}
}