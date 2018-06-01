using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodKill : MonoBehaviour
{

	private int cntFrame = 0;
    public int countFlag;
    public int type;
    public Color color;
    bool changeFlag = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        if (type == 3)
        {
            if (cntFrame % countFlag == 0)
            {
                if (changeFlag == false)
                {
                    this.gameObject.GetComponent<Renderer>().material.color = color;
                    changeFlag = true;
                }
                else
                {
                    this.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    changeFlag = false;
                }

            }
        }
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
