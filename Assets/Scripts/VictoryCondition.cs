 using UnityEngine;
using System.Collections;

public class VictoryCondition : MonoBehaviour {

	public int crowdSize = 5;
	public CrowdManager crowdManager;
	public PoliceGroup policeGroup;

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(Application.loadedLevelName == "level0")
			{
				if(crowdManager.CalculateCrowd () >= crowdSize)
				{
					//Victory ();
					Application.LoadLevel ("level1");
				}
			}
			else if(Application.loadedLevelName == "level1" && policeGroup != null)
			{
				if(policeGroup.GetPolicemanCount () == 0)
				{
					Application.LoadLevel ("MainMenu");
				}
			}
		}
	}

	void Victory ()
	{
		Debug.Log ("Victory");
		Time.timeScale = 0;
	}
}
