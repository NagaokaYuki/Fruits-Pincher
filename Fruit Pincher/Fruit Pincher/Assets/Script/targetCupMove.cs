using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetCupMove : MonoBehaviour {

    public bool moveFlag;
    int count;
    float startY;
    float goalY;

    public float moveSpeed;
    public int type;

	// Use this for initialization
	void Start () {
        moveFlag = false;
        count = 0;
        startY = this.gameObject.transform.position.y;
        goalY = this.gameObject.transform.position.y + 5;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (moveFlag == true)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + moveSpeed, this.gameObject.transform.position.z);
            count++;
            if (this.gameObject.transform.position.y >= goalY)
            {
                moveFlag = false;
                count = 0;

                GameObject target = GameObject.Find("targetObj(Clone)");
                targetData targetDataCS = target.GetComponent<targetData>();
                targetDataCS.resetJuice();
            }

            if (type == 0)
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }

        if (moveFlag == false)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - moveSpeed, this.gameObject.transform.position.z);
            
            if (type == 0)
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = 160;
            }

            if (this.gameObject.transform.position.y <= startY)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, startY, this.gameObject.transform.position.z);
            }
        }

    }
}
