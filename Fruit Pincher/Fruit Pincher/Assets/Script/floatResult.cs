using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatResult : MonoBehaviour {
	private int countTime;
	private float speed;
	private Vector3 oldPos;
    private int nFrameCount = 0;
	Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
	// Use this for initialization
	void Start () {
		oldPos = this.transform.position;
		countTime = Random.Range(55, 60);
		speed = Random.Range(0.0005f, 0.0006f);
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = oldPos + pos;

            if (nFrameCount <= countTime)
            {
                pos += new Vector3(0.0f, -speed, 0.0f);
                speed += 0.0001f;
            }

            else if (nFrameCount >= countTime && nFrameCount <= countTime * 2)
            {
                pos += new Vector3(0.0f, speed, 0.0f);
                speed -= 0.0001f;

            }
            else
            {
                nFrameCount = 0;
            }

            nFrameCount++;
	}
}
