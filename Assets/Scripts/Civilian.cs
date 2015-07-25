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

	void Start ()
	{
		state = CivilState.Waiting;
		home = transform.position;

		nav = GetComponent <NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		crowdManager = GameObject.FindGameObjectWithTag ("CrowdManager").GetComponent<CrowdManager>();

		StartCoroutine (UpdateTarget ());
	}
	
	IEnumerator UpdateTarget ()
	{
		while (true)
		{
		if (state == CivilState.FollowingPlayer && Vector3.Distance (player.position, transform.position) > distanceToPlayer)
				nav.SetDestination (player.position + offset);
			
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

	void Death ()
	{
		spawnPoint.spawnCounter--;
		Destroy (gameObject);
	}
}
