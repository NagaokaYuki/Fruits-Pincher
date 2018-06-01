using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juiceChange : MonoBehaviour {

    public int type;
    public Sprite[] textureCup;
    public int textureNo;
    //public Vector3 parentSize;
    //public Vector3 size;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<SpriteRenderer>().sprite = textureCup[textureNo];
        //this.transform.localScale = new Vector3(parentSize.x + size.x, parentSize.y + size.y, parentSize.z + size.z);

        if (type == 0)
        {
            this.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
        }
        else if (type == 1)
        {
            this.GetComponent<Renderer>().material.color = new Color(1.0f, 0.4f, 0.75f);
        }
        else if (type == 2)
        {
            this.GetComponent<Renderer>().material.color = new Color32(200, 113, 255, 255);
        }
        else if (type == 3)
        {
            this.GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0.0f);
        }
        else if (type == 4)
        {
            this.GetComponent<Renderer>().material.color = new Color32(186, 255, 51, 255);
        }
        else if (type == 5)
        {
            this.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        }
	}
}
