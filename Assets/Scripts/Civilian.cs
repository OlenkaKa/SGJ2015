using UnityEngine;
using System.Collections;

public class Civilian : MonoBehaviour {

	public enum CivilState
	{
		Waiting, FollowingPlayer, Returning, Atacking, Panicking
	}

	public SpawnPoint spawnPoint;
	public CivilState state;
	private Vector3 home;

	private NavMeshAgent nav;
	private Transform player;
	private CrowdManager crowdManager;
	private MoraleManager_00 moraleManager;
	private RaygunScript_00 weapon;
	private HealthScript_00 healthScript;

	public bool isInCrowd = false;
	public float distanceToPlayer;
	public Vector3 offset;
	public float rayRange;
	public bool isAlive = true;

	public const float MAX_SPEED = 15;
	public const float MIN_SPEED = 10;
	public const float MAX_ANGULAR_SPEED = 120;
	public const float MIN_ANGULAR_SPEED = 60;
	public const float MAX_ACCELERATION = 15;
	public const float MIN_ACCELERATION = 10;

	void Start ()
	{
		state = CivilState.Waiting;
		home = transform.position;

		nav = GetComponent <NavMeshAgent> ();
		weapon = GetComponent <RaygunScript_00> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		crowdManager = GameObject.FindGameObjectWithTag ("CrowdManager").GetComponent<CrowdManager>();
		moraleManager = GameObject.FindGameObjectWithTag ("MoraleManager").GetComponent<MoraleManager_00>();
		healthScript = GetComponent<HealthScript_00> ();
		//weapon.setFiring (false);


		nav.speed = Random.Range (MIN_SPEED, MAX_SPEED);
		nav.angularSpeed = Random.Range (MIN_ANGULAR_SPEED, MAX_ANGULAR_SPEED);
		nav.acceleration = Random.Range (MIN_ACCELERATION, MAX_ACCELERATION);
	}

	void Update ()
	{
		if(isAlive)
		{

			if (healthScript != null) 
			{
				if(!healthScript.IsAlive())
				{
					Death ();
				}
				else
				{
					if (state == CivilState.FollowingPlayer)
					{
						if(moraleManager.getOrder() == "Atack")
						{
							state = CivilState.Atacking;
						}
						else if(moraleManager.getOrder() == "Panic")
						{
							state = CivilState.Panicking;
						}
						else if(moraleManager.getOrder() == "Retreat")
						{
							state = CivilState.Returning;
						}
						else if(Vector3.Distance (player.position, transform.position) > distanceToPlayer)
						{
							nav.SetDestination (ObstacleDetection () ? player.position : player.position + offset);
						}
						else
						{
							nav.destination = transform.position;
						}
					}
					else if (state == CivilState.Atacking)
					{
						weapon.setFiring (true);
						if(moraleManager.getOrder() == "Panic")
						{
							weapon.setFiring (false);
							state = CivilState.Panicking;
						}
						else if(moraleManager.getOrder() == "Retreat")
						{
							weapon.setFiring (false);
							state = CivilState.Returning;
						}
						else if(moraleManager.getOrder() == "Follow")
						{
							weapon.setFiring (false);
							state = CivilState.FollowingPlayer;
						}
						//TODO wykrywanie i podazanie do policjantow
						else if(Vector3.Distance (player.position, transform.position) > distanceToPlayer)
						{
							nav.SetDestination (ObstacleDetection () ? player.position : player.position + offset);
						}
						else
						{
							nav.destination = transform.position;
						}
					}
					else if (state == CivilState.Panicking)
					{
						if(isInCrowd)
						{
							isInCrowd = false;
							crowdManager.removeCivilian (this);
						}
						if(moraleManager.getOrder() == "Panic")
						{
							nav.SetDestination (home);
						}
						else
						{
							state = CivilState.Waiting;
						}
					}
					else if (state == CivilState.Returning)
					{
						if(isInCrowd)
						{
							if(moraleManager.getOrder() == "Atack")
							{
								state = CivilState.Atacking;
							}
							else if(moraleManager.getOrder() == "Panic")
							{
								state = CivilState.Panicking;
							}
							else if(moraleManager.getOrder() == "Follow")
							{
								state = CivilState.FollowingPlayer;
							}
							else if(transform.position == home)
							{
								state = CivilState.Waiting;
								isInCrowd = false;
								crowdManager.removeCivilian (this);
							}
							else
							{
								nav.SetDestination (home);
							}
						}
						else if(transform.position == home)
						{
							state = CivilState.Waiting;
							crowdManager.removeCivilian (this);
							isInCrowd = false;
						}
						else
						{
							nav.SetDestination (home);
						}
					}
					else
					{
						nav.destination = transform.position;
					}
				}
			}
		}
	}

	bool ObstacleDetection ()
	{
		Vector3 direction = nav.destination - transform.position;
		Ray ray = new Ray (transform.position, direction.normalized);
		RaycastHit hit;
		
		if(Physics.Raycast (ray, out hit, rayRange))
		{
			if(hit.collider.gameObject.tag == "Obstacle")
			{
				//Debug.Log ("Obstacle detected");
				return true;
			}
			
		}
		
		return false;
	}

	void Death ()
	{
		isAlive = false;
		spawnPoint.spawnCounter--;
		moraleManager.decreaseMorale (2.5f);
		crowdManager.removeCivilian (this);
		Destroy (gameObject);
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, player.position);
	}
}
