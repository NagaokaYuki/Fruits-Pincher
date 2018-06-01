using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitObj : MonoBehaviour
{

	public GameObject[] FruitObj = new GameObject[30];
	public List<GameObject> FruitsList = new List<GameObject>();
	public pushManager pm;

	void OnTriggerEnter(Collider collision)
	{
		if (pm != null)
		{
			if (collision.gameObject.tag == "Player")
			{
				FruitsList.Add(collision.gameObject);
			}
		}
	}

	void OnTriggerStay(Collider collision)
	{
		if (pm != null)
		{
			pm.DestroyFruitNow = true;
			if(collision.gameObject.tag != "Player" && FruitsList.Count == 0)
			{
				pm.DestroyFruitNow = false;
			}
		}
	}


}
