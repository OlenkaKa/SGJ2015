﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour {

	public Text porazka;
	public HealthScript_00 healthScript;

	public float speed = 5;
	//public float range = 5;
	public CrowdManager crowdManager;

	//private Vector3 oldPosition;
	//private Vector3 currPosition;

	// Use this for initialization
	void Start () 
	{
		//currPosition = oldPosition = transform.position;
		healthScript = GetComponent<HealthScript_00> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (!healthScript.IsAlive())
		{
			porazka.text = "Game over!";
			return;
		}
		//PORUSZANIE
		if (Input.GetKey ("w"))
			transform.Translate (Vector3.forward * speed * Time.deltaTime);

		if (Input.GetKey ("s"))
			transform.Translate (-Vector3.forward * speed * Time.deltaTime);

		if (Input.GetKey ("a"))
			transform.Translate (Vector3.left * speed * Time.deltaTime);

		if (Input.GetKey ("d"))
			transform.Translate (Vector3.right * speed * Time.deltaTime);

		//currPosition = transform.position;

		//Vector3 direction = currPosition - oldPosition;

		//if(direction.magnitude > 0)
		//{
			//WYKRYWANIE PRZESZKODY
			//Ray ray = new Ray (transform.position, direction.normalized);
			//RaycastHit hit;

			//if(Physics.Raycast (ray, out hit, range))
			//{
				//if(hit.collider.gameObject.tag == "Obstacle")
				//{
					//hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
				//}

			//}
		//}

		//oldPosition = currPosition;
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.GetComponent<Civilian>())
		{
			crowdManager.AddCivilian(other.GetComponent<Civilian>());
		}
	}
}
