using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rankingMove : MonoBehaviour {

    public float rankingGoal;      //ランキングの目標地点
    public int rankingID;      //識別番号
    public bool rankingMoveFlg = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (rankingMoveFlg == true)
        {
            if (this.transform.position.y < rankingGoal)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 2.0f, this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, rankingGoal, this.transform.position.z);
            }
        }
	}
}
