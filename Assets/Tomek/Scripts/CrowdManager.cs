using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdManager : MonoBehaviour {

	public List<List<Civilian>> crowd = new List<List<Civilian>>();
	public int maxInCircle = 3; //Maksymalna liczba cywili w kręgu
	public float baseDistance = 3;


	// Use this for initialization
	void Start () 
	{
		crowd.Add (new List<Civilian> ());
	}
	
	// Update is called once per frame
	void Update () {
	
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
			civilian.ofset = new Vector3(Random.Range (-baseDistance * crowd.Count, baseDistance * crowd.Count), 0, Random.Range (-baseDistance * crowd.Count, baseDistance * crowd.Count));
			civilian.isInCrowd = true;

			Debug.Log (crowd.Count);
		}

	}
}
