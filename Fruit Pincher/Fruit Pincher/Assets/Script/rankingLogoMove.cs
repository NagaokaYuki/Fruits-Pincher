using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rankingLogoMove : MonoBehaviour
{

    private float rankingLogoGoal;      //ランキングの目標地点
    // Use this for initialization
    void Start()
    {
        rankingLogoGoal = 7;       //目標地点設定
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > rankingLogoGoal)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, rankingLogoGoal, this.transform.position.z);
        }
    }
}
