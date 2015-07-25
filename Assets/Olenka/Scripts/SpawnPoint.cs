using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
	public Object spawnPrefab;
	public int spawnAmount;

	void Start ()
	{
		for(int i = 0; i < spawnAmount; ++i)
		{
			Vector3 offset = new Vector3 (Random.Range(1f, 5f), 0.25f, Random.Range(1f, 5f));
			Instantiate (spawnPrefab, transform.position + offset, Quaternion.identity);
		}
	}
}
