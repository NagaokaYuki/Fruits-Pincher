using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchManager : MonoBehaviour
{

	public GameObject touchA;
	public GameObject touchB;
	private int cntFrame;
	public int distance;
	public float moveSpeed;
	private Vector3 oldA;
	private Vector3 oldB;
	private int start = 60;
	public bool play = true;

	// Use this for initialization
	void Start()
	{
		oldA = touchA.GetComponent<Transform>().position;
		oldB = touchB.GetComponent<Transform>().position;
	}

	// Update is called once per frame
	void Update()
	{
		if (play)
		{
			if (cntFrame == start)
			{
				touchA.SetActive(true);
				touchB.SetActive(true);
			}

			if (cntFrame >= start + 60)
			{
				touchA.GetComponent<Transform>().position -= new Vector3(0, moveSpeed, 0);
				touchB.GetComponent<Transform>().position += new Vector3(0, moveSpeed, 0);
			}

			if (cntFrame >= start + 60 + distance)
			{
				touchA.SetActive(false);
				touchB.SetActive(false);
				touchA.GetComponent<Transform>().position = oldA;
				touchB.GetComponent<Transform>().position = oldB;
				cntFrame = 0;
				start = 180;
			}
		}
		else
		{
			touchA.SetActive(false);
			touchB.SetActive(false);
		}
		cntFrame++;
	}
}
