﻿#pragma strict
 
 
 var qSamples: int = 1024;  // array size
 var refValue: float = 0.1; // RMS value for 0 dB
 var rmsValue: float;   // sound level - RMS
 var dbValue: float;    // sound level - dB
 var volume: float = 2; // set how much the scale will vary
 var success: boolean;
 
public var source: AudioSource; 
 private var samples: float[]; // audio samples
 
 function Start () {
     samples = new float[qSamples];
     success = false;
 }
 
 function GetVolume(){
     source.GetOutputData(samples, 0); // fill array with samples
     var i: int;
     var sum: float = 0;
     for (i=0; i < qSamples; i++){
         sum += samples[i]*samples[i]; // sum squared samples
     }
     rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
     dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
     if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
 }
 
 function Update () {
     GetVolume();
     if (rmsValue >= 0.8){
     success = true;
     Debug.Log ("Success!");
     
      } 
     if (rmsValue <= 0.8 && rmsValue != 0.0) {
     Debug.Log ("Failure");
     }
     }
function OnGUI () {
	if (success == true) {
	GUI.TextField (Rect (Screen.width/2,Screen.height/2,110,20), "MOTIVATIONAL");
	}
}