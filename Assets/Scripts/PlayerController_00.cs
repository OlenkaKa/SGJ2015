using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController_00 : MonoBehaviour {

	public Text porazka;
	public HealthScript_00 healthScript;

	public float speed = 5;
	public CrowdManager crowdManager;
	private MoraleManager_00 moraleManager;

	// Use this for initialization
	void Start () 
	{
		healthScript = GetComponent<HealthScript_00> ();
		moraleManager = GameObject.FindGameObjectWithTag ("MoraleManager").GetComponent<MoraleManager_00>();
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
	
	
		//KOMENDY
		if (Input.GetKey ("1"))
			moraleManager.setOrder("Follow");

		if (Input.GetKey ("2"))
			moraleManager.setOrder("Atack");
	}
	
	void OnTriggerEnter (Collider other)
	{
		if(other.GetComponent<Civilian>())
		{
			crowdManager.AddCivilian(other.GetComponent<Civilian>());
		}
	}
}
