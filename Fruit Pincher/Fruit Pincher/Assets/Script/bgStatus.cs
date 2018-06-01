using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bgStatus : MonoBehaviour
{

	public bool move = false;
	public bool load = false;
	public float downSpeed;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (move)
		{
			this.transform.position -= new Vector3(0.0f, downSpeed, 0.0f);
			if (this.transform.position.y <= 0)
			{
				this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
				move = false;
				SceneManager.LoadScene("countDown");
			}
		}
	}
}
