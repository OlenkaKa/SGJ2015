using UnityEngine;
using System.Collections;

public class PointFollower : MonoBehaviour
{
	private NavMeshAgent nav;
	private Transform player;
	private Vector3 offset;
	
	void Awake ()
	{
		nav = GetComponent <NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		offset = new Vector3 (0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		nav.SetDestination (FindTarget());
	}

	Vector3 FindTarget()
	{
		return player.position + offset;
	}
}
