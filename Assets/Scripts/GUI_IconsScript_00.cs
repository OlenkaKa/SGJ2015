﻿using UnityEngine;
using System.Collections;

public class GUI_IconsScript_00 : MonoBehaviour {
	
	public Texture shoutText;
	public Texture followText;
	public Texture attackText;
	
	void OnGUI() {
		
		GUI.DrawTexture(new Rect(10, Screen.height - 65, 60, 60), followText);
		GUI.DrawTexture(new Rect(80, Screen.height - 65, 60, 60), attackText);
		GUI.DrawTexture(new Rect(150, Screen.height - 65, 60, 60), shoutText);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

