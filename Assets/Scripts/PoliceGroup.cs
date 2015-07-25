using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class PoliceGroup : MonoBehaviour {

	public int policeAmount;
	public Object policePrefab;

	public float maxDistanceFromHome;
	public float attackDistance;
	public float rayRange;

	private List<Policeman> police;
	private static Mutex mut = new Mutex();
	
	void Start ()
	{
		police = new List<Policeman> ();
		for (int i = 0; i < policeAmount; ++i)
		{
			Vector3 startPos = transform.position + new Vector3 (Random.Range(1f, 5f), 0.5f, Random.Range(1f, 5f));
			GameObject policemanObj = (GameObject)Instantiate (policePrefab, startPos, Quaternion.identity);

			Policeman policeman = policemanObj.GetComponent<Policeman>();
			policeman.group = this;
			policeman.state = Policeman.PoliceState.Waiting;
			policeman.home = startPos;

			// policemen details
			policeman.maxDistanceFromHome = maxDistanceFromHome;
			policeman.attackDistance = attackDistance;
			policeman.rayRange = rayRange;

			police.Add(policeman);
		}
		Debug.Log (police.Count);
	}

	public void RemovePoliceman(Policeman p)
	{
		mut.WaitOne();
		police.Remove (p);
		mut.ReleaseMutex();
	}

	public void BroadcastTarget(Transform target)
	{
		mut.WaitOne();
		for (int i = 0; i < police.Count; ++i)
		{
			if(police[i].state != Policeman.PoliceState.Following)
				police[i].SetTarget(target);
		}
		mut.ReleaseMutex();
	}
}
