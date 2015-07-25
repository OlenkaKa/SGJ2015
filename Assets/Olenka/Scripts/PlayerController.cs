using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;

	private Rigidbody rb;

	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);
		movement = movement.normalized * speed * Time.deltaTime;
		rb.MovePosition (transform.position + movement);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Civil")
		{
			CilvilController civil = other.gameObject.GetComponent <CilvilController>();
			civil.JoinPlayer ();
		}
	}
}
