using UnityEngine;
using System.Collections;

public class HealthScript_01 : MonoBehaviour 
{
	public float MAX_HEALTH;
	public float REGENERATION;
	private float currentHealth;

	// Use this for initialization
	void Start () 
	{
		currentHealth = MAX_HEALTH;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (REGENERATION != 0 && currentHealth < MAX_HEALTH) 
		{
			currentHealth += REGENERATION;
			if(currentHealth > MAX_HEALTH)
			{
				currentHealth = MAX_HEALTH;
			}
		}
	}

	public void TakeDamage(float damage)
	{
		if (currentHealth > 0) 
		{
			currentHealth -= damage;
			if (currentHealth < 0) 
			{
				currentHealth = 0;
			}
		}
	}

	public bool IsAlive()
	{
		if (currentHealth > 0) 
		{
			return true;
		} 
		else 
		{
			return false;
		}
	}
}
