using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class CrowdManager : MonoBehaviour {

	private static Mutex mut = new Mutex();

	public List<List<Civilian>> crowd = new List<List<Civilian>>();
	public Transform player;
	public int maxInCircle = 6; //Maksymalna liczba cywili w kręgu
	public float baseDistance = 2;

	private int frameCounter;
	private int FRAME_LIMIT = 10;
	private float rayRange = 5;
	private int ghostCiviliansNumber;
	private const int GHOST_LIMIT = 5;

	// Use this for initialization
	void Start () 
	{
		frameCounter = 0;
		crowd.Add (new List<Civilian> ());
	}

	void Update ()
	{
		frameCounter++;
		if (frameCounter >= FRAME_LIMIT) 
		{
			frameCounter = 0;
			if(ghostCiviliansNumber >= GHOST_LIMIT)
			{
				cleanCrowd();
				ghostCiviliansNumber = 0;
			}
		}
	}

	public void AddCivilian (Civilian civilian)
	{
		mut.WaitOne();
		if(!civilian.isInCrowd && civilian.isAlive)
		{
			if(crowd.Count <= 0)
			{
				crowd.Add (new List<Civilian> ());
			}
			if(crowd [crowd.Count - 1].Count >= maxInCircle) //Jeśli krąg jest zapełniony dodaj, dodaj nowy krąg
				crowd.Add (new List<Civilian> ());

			List<Civilian> currentCircle = crowd [crowd.Count - 1];

			currentCircle.Add (civilian);
			civilian.distanceToPlayer = baseDistance * crowd.Count;
			civilian.state = Civilian.CivilState.FollowingPlayer;
			civilian.offset = new Vector3(Random.Range (-baseDistance * crowd.Count, baseDistance * crowd.Count), 0, Random.Range (-baseDistance * crowd.Count, baseDistance * crowd.Count));
			civilian.isInCrowd = true;
		}
		mut.ReleaseMutex ();
	}

	public void NewGHhostCivilian()
	{
		ghostCiviliansNumber++;
	}

	public int CalculateCrowd ()
	{
		int crowdSize = 0;
		
		foreach (List<Civilian> circle in crowd)
			crowdSize += circle.Count;

		return crowdSize;
	}

	//Cleans crowd from dead civilians
	private void cleanCrowd()
	{
		mut.WaitOne();
		List<List<Civilian>> oldCrowd = crowd;
		crowd = new List<List<Civilian>>();

		foreach (List<Civilian> circle in oldCrowd) 
		{
			foreach (Civilian person in circle)
			{
				if(person.isAlive)
				{
					person.isInCrowd = false;
					AddCivilian(person);		
				}
			}
		}
		mut.ReleaseMutex();
	}	

	public void removeCivilian(Civilian civilian)
	{
		mut.WaitOne();
		foreach (List<Civilian> circle in crowd) 
		{
			foreach (Civilian person in circle)
			{
				if(person.isAlive == civilian)
				{
					circle.Remove(civilian);
					NewGHhostCivilian ();
					return;
				}
			}
		}
		mut.ReleaseMutex ();
	}
}