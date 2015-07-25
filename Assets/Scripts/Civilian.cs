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

	public bool isInCrowd = false;
	public float distanceToPlayer;
	public Vector3 offset;
	public float rayRange;
	public bool isAlive = true;

	void Start ()
	{
		state = CivilState.Waiting;
		home = transform.position;

		nav = GetComponent <NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		crowdManager = GameObject.FindGameObjectWithTag ("CrowdManager").GetComponent<CrowdManager>();
		moraleManager = GameObject.FindGameObjectWithTag ("moraleManager").GetComponent<MoraleManager_00>();
	}

	void Update ()
	{
		if(isAlive)
		{
			HealthScript_00 healthScript = GetComponent<HealthScript_00> ();
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
						if(moraleManager.getOrder() == "Panic")
						{
							state = CivilState.Panicking;
						}
						else if(moraleManager.getOrder() == "Retreat")
						{
							state = CivilState.Returning;
						}
						else if(moraleManager.getOrder() == "Follow")
						{
							state = CivilState.FollowingPlayer;
						}
						else
						{
							//TODO
						}
					}
					else if (state == CivilState.Panicking)
					{
						nav.SetDestination (home);
						isInCrowd = false;
					}
					else if (state == CivilState.Returning)
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
				Debug.Log ("Obstacle detected");
				return true;
			}
			
		}
		
		return false;
	}

	void Death ()
	{
		isAlive = false;
		spawnPoint.spawnCounter--;
		crowdManager.removeCivilian (this);
		Destroy (gameObject);
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, player.position);
	}
}
