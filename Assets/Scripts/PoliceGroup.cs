using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class PoliceGroup : MonoBehaviour {

	public int policeAmount;
	public Object policePrefab;

	// policemen properties
	public float maxDistanceFromHome;
	public float attackDistance;
	public float rayRange;
	public int damage;
	public float reloadTime;
	public float shootRange;

	private List<Policeman> police;
	private static Mutex mut = new Mutex();
	
	void Start ()
	{
		police = new List<Policeman> ();
		for (int i = 0; i < policeAmount; ++i)
		{
			Vector3 startPos = transform.position + new Vector3 (Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f));
			GameObject policemanObj = (GameObject)Instantiate (policePrefab, startPos, Quaternion.identity);

			Policeman policeman = policemanObj.GetComponent<Policeman>();
			policeman.group = this;
			policeman.state = Policeman.PoliceState.Waiting;
			policeman.home = startPos;

			// policemen properties
			policeman.maxDistanceFromHome = maxDistanceFromHome;
			policeman.attackDistance = attackDistance;
			policeman.rayRange = rayRange;

			RaygunScript_00 raygun = policemanObj.GetComponent<RaygunScript_00>();
			raygun.DAMAGE = damage;
			raygun.MAX_RELOAD_TIME = reloadTime;
			raygun.MAX_RANGE = shootRange;

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
