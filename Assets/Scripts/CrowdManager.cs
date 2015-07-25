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

	public void RemoveCivilian (Civilian civ)
	{
		Debug.Log (crowd [(int)civ.placeInCrowd.x] [(int)civ.placeInCrowd.y]);
		crowd [(int)civ.placeInCrowd.x].Remove (crowd [(int)civ.placeInCrowd.x] [(int)civ.placeInCrowd.y]);

		for(int i = 0; i < crowd.Count - 1; i++)
		{
			if(i+1 < crowd.Count)
			{
				while(crowd[i].Count < maxInCircle && crowd[i+1].Count > 0)
				{
					Civilian currCivilian = crowd[i+1][crowd[i+1].Count - 1];

					currCivilian.placeInCrowd = new Vector2 (i, crowd[i].Count - 1);
					currCivilian.distanceToPlayer = baseDistance * i;
					currCivilian.offset = new Vector3(Random.Range (-baseDistance * i, baseDistance * i), 0, Random.Range (-baseDistance * i, baseDistance * i));

					crowd[i].Add (currCivilian);
					crowd[i+1].Remove (currCivilian);
				}
			}
		}
	}

	public int CalculateCrowd ()
	{
		int crowdSize = 0;
		
		foreach (List<Civilian> circle in crowd)
			crowdSize += circle.Count;

		return crowdSize;
	}
}
