using UnityEngine;
using System.Collections;

public class MoraleManager_00 : MonoBehaviour {
	
	public string currentOrder;	//Follow, Atack, Panic, Retreat
	public bool isPanicking;
	public bool isHeroic;
	private float currentMoraleValue;
	
	private float[] MORALE_RANGE = {-10, 0, 10, 20, 30};
	private float MORALE_DRIFT = 1/60;
	private float MULTIPLIER = 3;
	
	// Use this for initialization
	void Start () 
	{
		currentOrder = "Follow";
		currentMoraleValue = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		moraleDrift ();
	}
	
	//Drifts morale value towards neutrality
	private void moraleDrift()
	{
		if (currentMoraleValue < MORALE_RANGE [0]) 
		{
			//Below panic
			currentMoraleValue = MORALE_RANGE [0];
			isPanicking = true;
			isHeroic = false;
		}
		else if (currentMoraleValue < MORALE_RANGE [1]) 
		{
			//Panic
			currentMoraleValue += MORALE_DRIFT * MULTIPLIER;
			isPanicking = true;
			isHeroic = false;
			if (currentMoraleValue > MORALE_RANGE [1]) 
			{
				currentMoraleValue = MORALE_RANGE [1];
				isPanicking = false;
			}
		} 
		else if (currentMoraleValue < MORALE_RANGE [2]) 
		{
			//Neutral
			isPanicking = false;
			isHeroic = false;
		}
		else if (currentMoraleValue < MORALE_RANGE [3]) 
		{
			//Active
			currentMoraleValue -= MORALE_DRIFT;
			isPanicking = false;
			isHeroic = false;
			if (currentMoraleValue < MORALE_RANGE [2]) 
			{
				currentMoraleValue = MORALE_RANGE [2];
			}
		}
		else if (currentMoraleValue < MORALE_RANGE [4]) 
		{
			//Heroic
			currentMoraleValue -= MORALE_DRIFT * MULTIPLIER;
			isPanicking = false;
			isHeroic = true;
		}
		else
		{
			//Above Heroic
			currentMoraleValue = MORALE_RANGE [4];
			isPanicking = false;
			isHeroic = true;
		}
	}
	
	public string getOrder()
	{
		return currentOrder;
	}
}
