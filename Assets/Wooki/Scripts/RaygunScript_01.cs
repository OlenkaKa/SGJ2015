using UnityEngine;
using System.Collections;

public class RaygunScript_01 : MonoBehaviour 
{
	public float MAX_RELOAD_TIME;
	public float MAX_RANGE;
	private float currentReloadTime;
	public int DAMAGE;

	private Collider currentTarget;
	public AudioSource shootSound;  
	private Ray shootRay;
	private RaycastHit shootHit;

	// Use this for initialization
	void Start () 
	{
		currentTarget = null;
		currentReloadTime = 0;
		shootSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Reload ();
		if (CheckTargetValidity (currentTarget)) 
		{
			RotateTurret ();
			Fire ();
		}
		else 
		{
			currentTarget = ScanForTarget();
		}
	}

	//Reloads weapon
	private void Reload()
	{
		if (currentReloadTime > 0) 
		{
			currentReloadTime--;
			if (currentReloadTime < 0) 
			{
				currentReloadTime = 0;
			}
		}
	}

	//Checks if given target is valid for shooting
	private bool CheckTargetValidity(Collider target)
	{
		if (target == null) 
		{
			return false;
		}
	
		HealthScript_01 healthScript = target.GetComponent<HealthScript_01> ();
		if (healthScript == null) 
		{
			return false;
		}
		if (healthScript.IsAlive()) 
		{
			if(RayTargetCheck(target))
			{
				if(target.name == "PlayerBall" || target.name == "Civilian")
				{
					return true;
				}
				return false;
			}
			else
			{
				return false;
			}
		}
		return false;
	}

	//Scans for new target from ones in range
	private Collider ScanForTarget()
	{
		Collider[] collidersInRange = Physics.OverlapSphere(transform.position, MAX_RANGE);
		for (var i = 0; i < collidersInRange.Length; i++) 
		{
			if (CheckTargetValidity(collidersInRange[i]))
			{
				return collidersInRange[i];
			}
		}
		return null;
	}

	//Checks if we can hit the target
	private bool RayTargetCheck(Collider target)
	{
		shootRay.origin = transform.position;
		Vector3 direction = target.transform.position - transform.position;
		shootRay.direction = direction.normalized;

		if(Physics.Raycast (shootRay, out shootHit, MAX_RANGE))
		{
			return true;
		}
		return false;
	}

	public void RotateTurret()
	{

	}

	public void Fire()
	{
		if (currentReloadTime == 0) 
		{
			shootRay.origin = transform.position;
			Vector3 direction = currentTarget.transform.position - transform.position;
			shootRay.direction = direction.normalized;

			if(Physics.Raycast (shootRay, out shootHit, MAX_RANGE))
			{
				HealthScript_01 healthScript = shootHit.collider.GetComponent <HealthScript_01> ();
				if(healthScript != null)
				{
					healthScript.TakeDamage (DAMAGE);
					shootSound.Play();
					currentReloadTime = MAX_RELOAD_TIME;
				}
			}
		}
	}
}
