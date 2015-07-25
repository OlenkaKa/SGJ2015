using System.Collections;
using UnityEngine;

public class Jumping : MonoBehaviour {

	public float JumpSpeed = 100.0f;
	public float jumpHeight=1;
	
	private bool isFalling=false;



	public void Jump() { 
		if (isFalling == false) 
		{
			//rigidbody.AddForce(Vector3.up *JumpSpeed);


			//gameObject.rigidBody.velocity += new Vector3(0,10,0);
			transform.Translate (Vector3.up * jumpHeight * Time.deltaTime, Space.World);
			isFalling=true;
		}
		/*float rotation = Input.GetAxis ("Horizontal") * JumpSpeed;
		rotation *= Time.deltaTime;
		rigidbody.AddRelativeTorque (Vector3.up * rotation); */


		//animation.Play("jump_pose"); 
		//rigidbody.AddForce(Vector3.up *JumpSpeed);
	}

	void OnCollisionStay()
	{
		isFalling = false;
	}
}
