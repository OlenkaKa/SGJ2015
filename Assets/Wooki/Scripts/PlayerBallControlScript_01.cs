using UnityEngine;
using System.Collections;

public class PlayerBallControlScript_01 : MonoBehaviour {

	private float speed = 10;
	private Rigidbody rigidBody;
	
	void Start ()
	{
		rigidBody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rigidBody.AddForce (movement * speed);
	}
}
