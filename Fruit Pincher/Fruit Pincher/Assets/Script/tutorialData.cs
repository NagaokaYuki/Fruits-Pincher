using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialData : MonoBehaviour {

    public float fSpeed = 1.0f;
    public bool tutorialMoveFlg = false;
    public float tutorialGoal = 0;      //目標地点

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (tutorialMoveFlg == true)
        {
            if (this.transform.position.x < tutorialGoal)
            {
                this.transform.position = new Vector3(this.transform.position.x + fSpeed, this.transform.position.y, this.transform.position.z);

                if (this.transform.position.x >= tutorialGoal)
                {
                    this.transform.position = new Vector3(tutorialGoal, this.transform.position.y, this.transform.position.z);

                    tutorialMoveFlg = false;
                }
            }
            
            if (this.transform.position.x > tutorialGoal)
            {
                this.transform.position = new Vector3(this.transform.position.x - fSpeed, this.transform.position.y, this.transform.position.z);

                if (this.transform.position.x <= tutorialGoal)
                {
                    this.transform.position = new Vector3(tutorialGoal, this.transform.position.y, this.transform.position.z);

                    tutorialMoveFlg = false;
                }
            }
        }
	}
}
