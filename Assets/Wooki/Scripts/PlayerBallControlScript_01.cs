using UnityEngine;
using System.Collections;

public class PlayerBallControlScript_01 : MonoBehaviour 
{

	private float speed = 10;
	private Rigidbody rigidBody;

	private const int MAX_HP = 10;
	private int current_HP;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();

		current_HP = MAX_HP;
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rigidBody.AddForce (movement * speed);
	}

	void OnCollisionEnter(Collision collision) 
	{
	}

	public void TakeDamage(int damage)
	{
		current_HP = current_HP - damage;
		if (current_HP <= 0)
		{
			current_HP = 0;
			speed = 1;
		}
	}
}
