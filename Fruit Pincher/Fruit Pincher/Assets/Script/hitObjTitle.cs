using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitObjTitle : MonoBehaviour
{

	public List<GameObject> ButtonList = new List<GameObject>();
	public pushManagerTitle pm;

	void OnTriggerEnter(Collider collision)
	{
		if(pm != null)
		{
			if(collision.gameObject.tag == "Button")
			{
				ButtonList.Add(collision.gameObject);
			}
		}
	}

	void OnTriggerStay(Collider collision)
	{
		if(pm != null)
		{
			pm.DestroyFruitNow = true;
			
		}
	}


}
