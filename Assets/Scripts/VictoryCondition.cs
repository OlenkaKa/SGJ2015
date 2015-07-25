using UnityEngine;
using System.Collections;

public class VictoryCondition : MonoBehaviour {

	public int crowdSize = 5;
	public CrowdManager crowdManager;

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(crowdManager.CalculateCrowd () >= crowdSize)
			{
				Debug.Log ("Victory");
				Time.timeScale = 0;
			}
		}
	}
	
}
