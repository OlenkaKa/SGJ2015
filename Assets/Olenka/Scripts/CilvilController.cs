using UnityEngine;
using System.Collections;

public class CilvilController : MonoBehaviour
{
	private enum CivilState
	{
		Waiting, FollowingPlayer, Returning
	}

	private CivilState state;
	private Vector3 home;

	private NavMeshAgent nav;
	private Transform player;
	private Vector3 offset;
	//private Manager civilsManager;

	void Awake ()
	{
		state = CivilState.Waiting;
		home = transform.position;

		nav = GetComponent <NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		offset = new Vector3 (1f, 0f, 1f);
		//offset = civilsManager.Register()
	}

	void Update ()
	{
		if (state == CivilState.FollowingPlayer)
			nav.SetDestination (player.position + offset);

		else if (state == CivilState.Returning)
		{
			if(transform.position == home)
				state = CivilState.Waiting;
			else
				nav.SetDestination (home);
		}
	}

	public void JoinPlayer()
	{
		state = CivilState.FollowingPlayer;
	}
}
