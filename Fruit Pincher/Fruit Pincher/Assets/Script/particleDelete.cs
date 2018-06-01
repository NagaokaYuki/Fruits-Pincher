using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDelete : MonoBehaviour {
    private int nCountDelete = 0;
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale -= new Vector3(speed, speed, speed);
		if (nCountDelete > 60 || this.transform.localScale.x <= 0)
        {
            nCountDelete = 0;
            DestroyImmediate(this.gameObject);
        }
        nCountDelete++;
	}
}
