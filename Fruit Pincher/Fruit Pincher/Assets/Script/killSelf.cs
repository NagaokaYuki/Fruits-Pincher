using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killSelf : MonoBehaviour {

	private int cntFrame = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
        if (cntFrame >= 30)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(2.77f, 6.08f, -3.0f), 0.5f);
            //DestroyImmediate(this.gameObject);
        }
		else
		{
			cntFrame++;
			
		}
        if (cntFrame >= 30 && this.gameObject.transform.position.y >= 6.07f && this.gameObject.transform.position.x >= 2.76f)
        {
            DestroyImmediate(this.gameObject);

        }
	}
}
