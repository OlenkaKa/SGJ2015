using UnityEngine;
using System.Collections;

public class Policeman : MonoBehaviour
{
	public enum PoliceState
	{
		Waiting, Following, Returning
	}

	public PoliceGroup group;
	public Vector3 home;

	public Transform target;
	public PoliceState state;

	public float attackDistance;
	public float rayRange;
	public float maxDistanceFromHome;

	private NavMeshAgent nav;
	
	void Start ()
	{
		state = PoliceState.Waiting;
		nav = GetComponent <NavMeshAgent> ();

		StartCoroutine (UpdateTarget ());
	}

	IEnumerator UpdateTarget ()
	{
		while (true)
		{
			if(target == null)
				state = PoliceState.Returning;

			if (state == PoliceState.Following)
			{
				if(!InPatrolArea())
					state = PoliceState.Returning;
				else if(Vector3.Distance (target.position, transform.position) > attackDistance)
					nav.SetDestination (ObstacleDetection () ? 
					                    target.position : target.position + new Vector3 (Random.Range(1f, 2f), 0.5f, Random.Range(1f, 2f)));
				else
					transform.forward = Vector3.Normalize(target.transform.position);//nav.SetDestination(target.position);
			}
			
			else if (state == PoliceState.Returning)
			{
				if(transform.position == home)
					state = PoliceState.Waiting;
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
		group.RemovePoliceman (this);
		Destroy (gameObject);
	}

	bool InPatrolArea()
	{
		if (Vector3.Distance (home, transform.position) > maxDistanceFromHome)
			return false;
		return true;
	}

	public void SetTarget(Transform t)
	{
		target = t;
		state = PoliceState.Following;
	}

	void OnTriggerStay (Collider other)
	{
		if(InPatrolArea() && other.tag == "Civilian" /*|| other.tag == "Player"*/)
		{
			SetTarget(other.transform);
			group.BroadcastTarget (target);
		}
	}
}
