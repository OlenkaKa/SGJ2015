#pragma strict
 
 
 var qSamples: int = 1024;  // array size
 var refValue: float = 0.1; // RMS value for 0 dB
 var rmsValue: float;   // sound level - RMS
 var dbValue: float;    // sound level - dB
 var volume: float = 2; // set how much the scale will vary
 var success: boolean;
 var winText: Texture;
 var toggleGUI : boolean;
 var script : DeciCheck;
 
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
     var buffValue: float = 0; 
     GetVolume();
     if (rmsValue >= 0.9)
     {
     	success = true;
     	Debug.Log ("Success!");
     	Motivated();
     	buffValue = 9;
     } 
     else if(rmsValue >= 0.6)
     {
     	buffValue = 6;
     }
     
     else if(rmsValue >= 0.3)
     {
     	buffValue = 3;
     }
     
     else 
     {
     	Debug.Log ("Failure");
     }
     
     }
function OnGUI () {
	
	if (toggleGUI == true) {
	GUI.DrawTexture(new Rect(Screen.width/2 - 127, Screen.height - 32, 256, 32), winText);
	
	} 
}

function Motivated() {
	 toggleGUI = true;
     yield WaitForSeconds(2.0);
     toggleGUI = false;
     script = GetComponent(DeciCheck);
     script.enabled = false;
     
}