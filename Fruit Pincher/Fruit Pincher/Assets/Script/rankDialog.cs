using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rankDialog : MonoBehaviour {
	public bool move = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (move == true)
		{
			this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(0.0f, 0.0f, 0.0f), 0.6f);
		}
		if (move == false)
		{
			this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(0.0f, 14.0f, 0.0f), 0.6f);
		}
	}

	
	public void DialogClick()
	{
		move = true;
	}
	public void DialogNoClick()
	{
		move = false;
	}
}
