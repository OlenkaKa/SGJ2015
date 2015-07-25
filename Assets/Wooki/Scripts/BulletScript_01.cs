using UnityEngine;
using System.Collections;

public class BulletScript_01 : MonoBehaviour 
{

	private int damage = 50;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "PlayerBall")
		{
			HealthScript_01 healthScript = collision.gameObject.GetComponent<HealthScript_01>();
			if(healthScript != null)
			{
				healthScript.TakeDamage(damage);
			}
			Destroy(gameObject);
		}
		/*else if (collision.gameObject.name == "Civilian")
		{
			CivilianControlScript_01 healthScript = collision.gameObject.GetComponent<CivilianControlScript_01>();
			if(healthScript != null)
			{
				healthScript.TakeDamage(damage);
			}
			Destroy(gameObject);
		}
		else if(collision.gameObject.name == "Cop")
		{
			CopControlScript_01 healthScript = collision.gameObject.GetComponent<CopControlScript_01>();
			if(healthScript != null)
			{
				healthScript.TakeDamage(damage);
			}
			Destroy(gameObject);
		}*/
		else 
		{
			//Destroy(gameObject);
			Destroy(gameObject);
		}
	}
}
