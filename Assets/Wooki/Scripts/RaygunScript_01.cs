using UnityEngine;
using System.Collections;

public class RaygunScript_01 : MonoBehaviour 
{
	public float MAX_RELOAD_TIME;
	public float MAX_RANGE;
	private float currentReloadTime;
	public int DAMAGE;

	public AudioSource shootSound;  
	private Ray shootRay;
	private RaycastHit shootHit;

	// Use this for initialization
	void Start () 
	{
		currentReloadTime = 0;
		shootSound = GetComponent<AudioSource> ();
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
			if(TargetCheck())
			{
				Fire();
			}
		//}
		//else
		//{
		//	obracaj sie dookola
		//}
	}

	private bool TargetCheck()
	{
		shootRay.origin = transform.position;
		shootRay.direction = transform.up;
		
		if(Physics.Raycast (shootRay, out shootHit, MAX_RANGE))
		{
			if (shootHit.collider.name == "Cop")
			{
				return false;
			}
			else if (shootHit.collider.name == "PlayerBall")
			{
				return true;
			}
			else if (shootHit.collider.name == "Civilian")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		return false;
	}

	public void Fire()
	{
		if (currentReloadTime == 0) 
		{
			shootRay.origin = transform.position;
			shootRay.direction = transform.up;

			if(Physics.Raycast (shootRay, out shootHit, MAX_RANGE))
			{
				PlayerBallControlScript_01 healthScript = shootHit.collider.GetComponent <PlayerBallControlScript_01> ();
				if(healthScript != null)
				{
					healthScript.TakeDamage (DAMAGE);
				}
			}
			shootSound.Play();
			currentReloadTime = MAX_RELOAD_TIME;
		}
	}
}
