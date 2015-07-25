using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	
	/*public Texture2D fadingScreen;
	public float fadeSpeed= 0.8f;
	
	private int fadeDirect=1;
	private int drawDepth = -1000;
	private float alpha=1.0f;
	
	void OnGUI()
	{
		alpha += fadeDirect * fadeSpeed * Time.deltaTime;
		alpha=Mathf.Clamp01(alpha);
		
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadingScreen);
	}
	
	public float beginFade(int direction)
	{
		fadeDirect = direction;
		return (fadeSpeed);
	}
	
	void OnLevelWasLoaded()
	{
		beginFade (1);
	}
	*/
	public void ChangeToScene (string SceneToChangeTo) {
		Application.LoadLevel (SceneToChangeTo);
	}

	public void ChangeToScene2 (int SceneToChangeTo2) {
		Application.LoadLevel (SceneToChangeTo2);
	}

}
