using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoliceGroup : MonoBehaviour {

	public int policeAmount;
	public Object policePrefab;

	private List<Policeman> police;
	
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
			policeman.maxDistanceFromHome = 10;
			policeman.distanceToTarget = 5;
			policeman.rayRange = 5;

			police.Add(policeman);
		}
		Debug.Log (police.Count);
	}

	public void RemovePoliceman(Policeman p)
	{
		police.Remove (p);
	}

	public void BroadcastTarget(Transform target)
	{
		for (int i = 0; i < police.Count; ++i)
		{
			if(police[i].state != Policeman.PoliceState.Following)
				police[i].SetTarget(target);
		}
	}
}
