using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startAnimation : MonoBehaviour {
	public int countTime;
	public float speed;
	private Vector3 oldScale;
    private int nFrameCount = 0;
	Vector3 scale = new Vector3(0.0f, 0.0f, 0.0f);
	// Use this for initialization
	void Start () {
		oldScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.localScale = oldScale + scale;

        if (nFrameCount <= countTime)
        {
            scale += new Vector3(-speed, -speed, 0.0f);
        }

        else if (nFrameCount >= countTime && nFrameCount <= countTime * 2)
        {
            scale += new Vector3(speed, speed, 0.0f);
        }
		else
		{
			nFrameCount = 0;
		}

        nFrameCount++;
	}
}
