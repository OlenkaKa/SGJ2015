using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdManager : MonoBehaviour {

	public List<List<Civilian>> crowd = new List<List<Civilian>>();
	public Transform player;
	public int maxInCircle = 3; //Maksymalna liczba cywili w kręgu
	public float baseDistance = 3;

	private float rayRange = 5;


	// Use this for initialization
	void Start () 
	{
		crowd.Add (new List<Civilian> ());
		//StartCoroutine (UpdateTarget ());
	}

	public void AddCivilian (Civilian civilian)
	{
		if(!civilian.isInCrowd)
		{
			if(crowd [crowd.Count - 1].Count >= maxInCircle) //Jeśli krąg jest zapełniony dodaj, dodaj nowy krąg
				crowd.Add (new List<Civilian> ());

			List<Civilian> currentCircle = crowd [crowd.Count - 1];

			currentCircle.Add (civilian);
			civilian.placeInCrowd = new Vector2 (crowd.Count - 1, currentCircle.Count - 1);
			civilian.distanceToPlayer = baseDistance * crowd.Count;
			civilian.state = Civilian.CivilState.FollowingPlayer;
			civilian.offset = new Vector3(Random.Range (-baseDistance * crowd.Count, baseDistance * crowd.Count), 0, Random.Range (-baseDistance * crowd.Count, baseDistance * crowd.Count));
			civilian.isInCrowd = true;

			Debug.Log (crowd.Count);
		}

	}

	/*IEnumerator UpdateTarget ()
	{
		while (true)
		{
			foreach(List<Civilian> circle in crowd)
			{
				foreach(Civilian civ in circle)
				{
					if(Vector3.Distance (player.position, civ.transform.position) > civ.distanceToPlayer || !ObstacleDetection (civ))
					{
						civ.GetComponent<NavMeshAgent>().destination = player.position + civ.ofset;
					}
					else
						civ.GetComponent<NavMeshAgent>().destination = civ.transform.position;
				}
			}

			yield return new WaitForSeconds (0.5f);
		}
	}*/

	bool ObstacleDetection (Civilian civilian)
	{
		Vector3 direction = civilian.GetComponent<NavMeshAgent>().destination - civilian.transform.position;
		Ray ray = new Ray (civilian.transform.position, direction.normalized);
		RaycastHit hit;
		
		if(Physics.Raycast (ray, out hit, rayRange))
		{
			if(hit.collider.gameObject.tag == "Obstacle")
			{
				return true;
			}
			
		}

		return false;
	}
}
