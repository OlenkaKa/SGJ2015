using UnityEngine;
using System.Collections;

public class PlayerBallControlScript_01 : MonoBehaviour 
{

	private float speed = 10;
	private Rigidbody rigidBody;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
	{
		Movement ();
		if (IsAlive ()) 
		{

		} 
		else 
		{
			speed = 2;
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
	}

	private void Movement()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rigidBody.AddForce (movement * speed);
	}

	private bool IsAlive()
	{
		HealthScript_01 healthScript = GetComponent <HealthScript_01>();
		if (healthScript != null) 
		{
			return healthScript.IsAlive();
		} 
		else 
		{
			return true;
		}
	}
}
