using UnityEngine;
using System.Collections;

public class RaygunScript_01 : MonoBehaviour 
{
	public float MAX_RELOAD_TIME;
	private float currentReloadTime;

	// Use this for initialization
	void Start () 
	{
		currentReloadTime = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Reload ();
		EnemySeek ();
	}

	private void Reload()
	{
		if (currentReloadTime > 0) 
		{
			currentReloadTime -= 10f;
			if (currentReloadTime < 0) 
			{
				currentReloadTime = 0;
			}
		}
	}

	private void EnemySeek()
	{
		//if przeciwnik w zasiego
		//{
		//	obroc sie do niego
		//	if linia strzalu
		//	{
		//		Fire();
		//	}
		//}
		//else
		//{
		//	obracaj sie
		//}
	}

	public void Fire()
	{
		if (currentReloadTime == 0) 
		{

			currentReloadTime = MAX_RELOAD_TIME;
		}
	}
}
