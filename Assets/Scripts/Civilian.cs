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
	public Vector2 placeInCrowd; // x - numer kręgu, y - pozycja w kręgu
	public bool isInCrowd = false;
	public float distanceToPlayer;
	public Vector3 offset;
	public float rayRange;

	void Start ()
	{
		state = CivilState.Waiting;
		home = transform.position;

		nav = GetComponent <NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		crowdManager = GameObject.FindGameObjectWithTag ("CrowdManager").GetComponent<CrowdManager>();

		StartCoroutine (UpdateTarget ());
	}

	void Update ()
	{
		HealthScript_00 healthScript = GetComponent<HealthScript_00> ();
		if (healthScript != null) 
		{
			if(!healthScript.IsAlive())
			{
				Death ();
			}
		}
	}
	
	IEnumerator UpdateTarget ()
	{
		while (true)
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
			yield return new WaitForSeconds (0.5f);
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
		spawnPoint.spawnCounter--;
		Destroy (gameObject);
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, player.position);
	}
}
