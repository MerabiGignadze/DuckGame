using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpots : MonoBehaviour
{
	[SerializeField]
	List<Transform> spots;

	private void OnDrawGizmos()
	{
		if (transform.childCount != spots.Count) { CollectSpots(); }
	}
	void CollectSpots()
	{
		int id = 0;
		spots = new List<Transform>();
		foreach (Transform spot in transform)
		{
			id++;
			spots.Add(spot);
			spot.name = "SpawnerSpot" + id;
		}
	}
}
