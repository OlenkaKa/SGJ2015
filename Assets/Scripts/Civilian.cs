using UnityEngine;
using System.Collections;

public class Civilian : MonoBehaviour {

	public enum CivilState
	{
		Waiting, FollowingPlayer, Returning
	}

	public SpawnPoint spawnPoint;
	public CivilState state;
	private Vector3 home;

	private NavMeshAgent nav;
	private Transform player;
	private CrowdManager crowdManager;

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
					if (state == CivilState.FollowingPlayer && Vector3.Distance (player.position, transform.position) > distanceToPlayer)
						nav.SetDestination (ObstacleDetection () ? player.position : player.position + offset);
					
					else if (state == CivilState.Returning)
					{
						if(transform.position == home)
							state = CivilState.Waiting;
						else
							nav.SetDestination (home);
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
