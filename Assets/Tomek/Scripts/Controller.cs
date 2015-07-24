using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float speed = 5;
	public float range = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//PORUSZANIE
		if (Input.GetKey ("w"))
			transform.Translate (Vector3.forward * speed * Time.deltaTime);

		if (Input.GetKey ("s"))
			transform.Translate (-Vector3.forward * speed * Time.deltaTime);

		if (Input.GetKey ("a"))
			transform.Translate (Vector3.left * speed * Time.deltaTime);

		if (Input.GetKey ("d"))
			transform.Translate (Vector3.right * speed * Time.deltaTime);

		//WYKRYWANIE PRZESZKODY
		Ray ray = new Ray (transform.position, Vector3.forward);
		RaycastHit hit;

		if(Physics.Raycast (ray, out hit, range))
		{
			if(hit.collider.gameObject.tag == "Obstacle")
			{
				hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
			}
		}
	}
}
