using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonStatus : MonoBehaviour
{
	public bool pressed;
	public bool colorBlink = false;
	public int buttonType;
    public GameObject titleLogo;
    public GameObject bg;
	private int cntFrame = 0;
    private AudioSource sound01;
    public bool stop = true;
    public bool fly = false;
	public float flySpeed;
	public bool bgDown = false;
    void Start()
    {
        //AudioSourceコンポーネントを取得し、変数に格納
        sound01 = GetComponent<AudioSource>();
    }
	void FixedUpdate()
	{
        if (fly)
        {
            if (buttonType == 1)
            {
                this.transform.position += new Vector3(0.0f, flySpeed, 0.0f);
            }
            else
            {
                this.transform.position -= new Vector3(0.0f, flySpeed, 0.0f);
            }

            titleLogo.transform.position += new Vector3(0.0f, flySpeed, 0.0f);
        }

        if (bgDown)
        {
			bg.GetComponent<bgStatus>().move = true;
		}
        
		if (colorBlink)
		{
           
            if (stop == true)
            {
                // MENU ITEM音声
                sound01.PlayOneShot(sound01.clip);
                stop = false;
            }
            //==============
			if (cntFrame % 5 == 0)
			{
				this.gameObject.GetComponent<Renderer>().material.color = Color.red;

			}
			else
			{
				this.gameObject.GetComponent<Renderer>().material.color = Color.white;
				
			}

			if(cntFrame >= 60)
			{
				cntFrame = 0;
				pressed = true;
			}
			cntFrame++;

		}
	}
	
}
