using UnityEngine;
using System.Collections;

public class Pathfinder : MonoBehaviour {

	public float range = 5;
	public Transform leader;
	public Vector3 ofset;
	public float maxOfset = 5;
	private NavMeshAgent agent;


	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent> ();
		//agent.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!ObstacleDetection() && leader)
		{
			//agent.enabled = true;
			agent.destination = leader.position;
			Debug.Log ("Path set");

		}
		else
		{
			//agent.enabled = false;
		}
	}

	bool ObstacleDetection ()
	{
		if (leader)
		{
			Vector3 direction = (leader.position + ofset) - transform.position;
			Ray ray = new Ray (transform.position, direction);
			RaycastHit hit;
			
			if(Physics.Raycast (ray, out hit, range))
			{
				if(hit.collider.gameObject.tag == "Obstacle")
				{
					return true;
				}
			}
		}

		return false;
	}
}
