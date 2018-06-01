using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerKill : MonoBehaviour {

    private int cntFrame = 0;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + speed, this.transform.position.z);
        if (cntFrame >= 30)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            cntFrame++;
        }
	}
}
