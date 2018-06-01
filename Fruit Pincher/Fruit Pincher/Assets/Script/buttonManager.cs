using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{

    public GameObject startButton;
    public GameObject staffButton;
    public GameObject rankingButton;
    public GameObject titlePusher;
    public GameObject touchObj;
    
	// Use this for initialization
	void Start()
	{

	}

	
	// Update is called once per frame
	void Update()
	{

        // flying
        if (startButton.GetComponent<buttonStatus>().colorBlink == true)
		{
            rankingButton.GetComponent<buttonStatus>().fly = true;
            staffButton.GetComponent<buttonStatus>().fly = true;
            startButton.GetComponent<buttonStatus>().bgDown = true;
			titlePusher.SetActive(false);
			touchObj.GetComponent<touchManager>().play = false;

		}

		if (rankingButton.GetComponent<buttonStatus>().colorBlink == true)
		{
            startButton.GetComponent<buttonStatus>().fly = true;
            staffButton.GetComponent<buttonStatus>().fly = true;
			titlePusher.SetActive(false);
			touchObj.GetComponent<touchManager>().play = false;
		}

        if (staffButton.GetComponent<buttonStatus>().colorBlink == true)
        {
            startButton.GetComponent<buttonStatus>().fly = true;
            rankingButton.GetComponent<buttonStatus>().fly = true;
			titlePusher.SetActive(false);
			touchObj.GetComponent<touchManager>().play = false;
        }
	}
}
