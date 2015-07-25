using UnityEngine;
using System.Collections;

public class RaygunScript_00 : MonoBehaviour 
{
	public int DAMAGE;
	public float MAX_RELOAD_TIME;
	public float MAX_RANGE;
	private float currentReloadTime;
	
	private Collider currentTarget;
	private Ray shootRay;
	private RaycastHit shootHit;
	public AudioSource shootSound;  
	
	private bool firing;
	
	// Use this for initialization
	void Start () 
	{
		firing = true;
		currentTarget = null;
		currentReloadTime = 0;
		shootSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Reload ();
		if (firing) 
		{
			if (CheckTargetValidity (currentTarget)) 
			{
				RotateTurret ();
				Fire ();
			} else 
			{
				currentTarget = ScanForTarget ();
			}
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
		
		HealthScript_00 healthScript = target.GetComponent<HealthScript_00> ();
		if (healthScript == null) 
		{
			return false;
		}
		if (healthScript.IsAlive()) 
		{
			if(RayTargetCheck(target))
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
			if(gameObject.tag == "Policeman" || gameObject.tag == "ArmouredCar")
			{
				if(shootHit.collider.gameObject.tag == "Player" || shootHit.collider.gameObject.tag == "Civilian")
				{
					return true;
				}
			}
			else if(gameObject.tag == "Player" || gameObject.tag == "Civilian")
			{
				if(shootHit.collider.gameObject.tag == "Policeman" || shootHit.collider.gameObject.tag == "ArmouredCar")
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}
	
	public void RotateTurret()
	{
		//gameObject.transform.ro
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
				HealthScript_00 healthScript = shootHit.collider.GetComponent <HealthScript_00> ();
				if(healthScript != null)
				{
					healthScript.TakeDamage (DAMAGE);
					shootSound.Play();
					currentReloadTime = MAX_RELOAD_TIME;
				}
			}
		}
	}
	
	public void setFiring(bool newFiring)
	{
		firing = newFiring;
	}
	
	public bool isFiring()
	{
		return firing;
	}
	
	public Collider getCurrentTarget()
	{
		return currentTarget;
	}
}
