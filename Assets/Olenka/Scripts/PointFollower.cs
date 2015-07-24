using UnityEngine;
using System.Collections;

public class PointFollower : MonoBehaviour
{
	private Transform player;
	private NavMeshAgent nav;
	
	void Awake ()
	{
		nav = GetComponent <NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		nav.SetDestination (FindTarget());
	}

	Vector3 FindTarget()
	{
		return player.position;
	}
}
