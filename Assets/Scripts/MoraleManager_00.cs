using UnityEngine;
using System.Collections;

public class MoraleManager_00 : MonoBehaviour {
	
	public string currentOrder;	//Follow, Atack, Panic, Retreat
	public bool isPanicking;
	public bool isHeroic;
	private float currentMoraleValue;
	
	private float[] MORALE_RANGE = {-10f, 0f, 10f, 20f, 30f};
	private float MORALE_DRIFT = 0.01f;
	private float MULTIPLIER = 3f;
	
	// Use this for initialization
	void Start () 
	{
		currentOrder = "Follow";
		currentMoraleValue = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool prevIsPanicking = isPanicking;
		moraleDrift ();
		if(prevIsPanicking && !isPanicking)
		{
			currentOrder = "Follow";
		}
		else if(prevIsPanicking && isPanicking)
		{
			currentOrder = "Panic";
		}
		Debug.Log (currentMoraleValue + " Morale " + currentOrder + " P " + isPanicking + " H " + isHeroic);
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

	public float getBuff()
	{
		if (isHeroic) 
		{
			return 2f;
		}
		else if (isPanicking) 
		{
			return 0.75f;
		} 
		else 
		{
			return 1f;
		}
	}

	public void setOrder(string newOrder)
	{
		if (!isPanicking) 
		{
			currentOrder = newOrder;
		}
	}

	public void decreaseMorale(float effect)
	{
		currentMoraleValue -= effect;
	}

	public void increaseMorale(float effect)
	{
		currentMoraleValue += effect;
	}
}
