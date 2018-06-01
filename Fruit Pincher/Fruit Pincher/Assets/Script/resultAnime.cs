using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultAnime : MonoBehaviour {

    public Vector3 goalSize;
    Vector3 sizeSpeed = new Vector3(0.01f , 0.01f,0.01f);
    bool bUse;
    public bool bAnime;
	// Use this for initialization
	void Start () {
        if (bAnime == true)
        {
            this.transform.localScale = new Vector3(0, 0, 0);
        }
        bUse = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (bAnime == true)
        {
            if (this.transform.localScale.x <= 0 && this.transform.localScale.y <= 0 && this.transform.localScale.y <= 0)
            {
                bUse = true;
            }

            if (bUse == true)
            {
                this.transform.localScale += sizeSpeed;

                if (this.transform.localScale.x >= goalSize.x && this.transform.localScale.y >= goalSize.y && this.transform.localScale.z >= goalSize.z)
                {
                    this.transform.localScale = goalSize;
                }
            }
        }
	}
}
