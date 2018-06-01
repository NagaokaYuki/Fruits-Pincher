using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetData : MonoBehaviour
{

    public int type;
    public int point;

    public Sprite[] texture;
    public Sprite[] textureShadow;
    //public GameObject pointObj;

    //static int noPoint;

    //共通のサイズ
    public Vector3 parentSize;

    //GameObject obj;
    //public Vector3 objPos;
    //public Vector3 objSize;
    //GameObject objShadow;
	public int maxObj;
    public Sprite[] textureCup;
    public GameObject cupObjData;
    GameObject[] cupObj = new GameObject[6];
    public Vector3 cupObjPos;
    public Vector3 cupObjSize;

    //[0]がゲーム中に消した果物の総数、[1]がターゲットと一致した数
    static int[] scoreNumList = new int[2];

    public GameObject perfectObj;
    public Sprite[] perfectTex;
    public Vector3 perfectPos;

    static int[] juiceList = new int[5];

    // Use this for initialization
	void Start()
    {
        //noPoint = maxObj - point;

        //pointObj.GetComponent<SpriteRenderer>().sprite = texture[noPoint];

        //obj = Instantiate(pointObj, new Vector3(this.transform.position.x + objPos.x, this.transform.position.y + objPos.y, this.transform.position.z + objPos.z), Quaternion.identity);
        //obj.transform.localScale = new Vector3(parentSize.x * objSize.x, parentSize.y * objSize.y, parentSize.z * objSize.z);
        //objShadow = Instantiate(pointObj, new Vector3(this.transform.position.x + objPos.x + 0.02f, this.transform.position.y + objPos.y - 0.02f, this.transform.position.z + objPos.z), Quaternion.identity);
        //objShadow.GetComponent<Renderer>().material.color = Color.black;
        //objShadow.GetComponent<SpriteRenderer>().sortingOrder = 160;
        //objShadow.transform.localScale = new Vector3(parentSize.x * objSize.x, parentSize.y * objSize.y, parentSize.z * objSize.z);

        for (int nCnt = 0; nCnt < maxObj; nCnt++)
        {
            //cupObjData.GetComponent<SpriteRenderer>().sprite = textureCup[nCnt + 1];
            cupObj[nCnt] = Instantiate(cupObjData, new Vector3(this.transform.position.x + cupObjPos.x, this.transform.position.y + cupObjPos.y, this.transform.position.z + cupObjPos.z), Quaternion.identity);
            cupObj[nCnt].GetComponent<SpriteRenderer>().sortingOrder = 155 - (nCnt * 1);
            cupObj[nCnt].GetComponent<juiceChange>().type = 5;
            cupObj[nCnt].GetComponent<juiceChange>().textureNo = 0;
            //cupObj[nCnt].GetComponent<juiceChange>().parentSize = parentSize;
            cupObj[nCnt].transform.localScale = new Vector3(this.transform.localScale.x * cupObjSize.x, this.transform.localScale.y * cupObjSize.y, this.transform.localScale.z * cupObjSize.z);
        }

        //Debug.Log(parentSize);

        scoreNumList[0] = 0;
        scoreNumList[1] = 0;

        for (int i = 0; i < 5; i++)
        {
            juiceList[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (point == 0)
        {
            this.GetComponent<Renderer>().material.color = Color.white;
        }

        //noPoint = maxObj - point;

        //obj.GetComponent<SpriteRenderer>().sprite = texture[noPoint];
        //objShadow.GetComponent<SpriteRenderer>().sprite = texture[noPoint];
        ////Debug.Log(point + "||" + noPoint);
        ////cupObj.GetComponent<SpriteRenderer>().sprite = textureCup[point];

        //obj.transform.position = new Vector3(this.transform.position.x + objPos.x, this.transform.position.y + objPos.y, this.transform.position.z + objPos.z);
        //if (obj.transform.position.y >= (this.transform.position.y + objPos.y + 0.01f))
        //{
        //    obj.GetComponent<SpriteRenderer>().sortingOrder = 0;
        //}
        //else
        //{
        //    obj.GetComponent<SpriteRenderer>().sortingOrder = 160;
        //}
        //objShadow.transform.position = new Vector3(this.transform.position.x + objPos.x + 0.02f, this.transform.position.y + objPos.y - 0.02f, this.transform.position.z + objPos.z);
        //if (objShadow.transform.position.y >= (this.transform.position.y + objPos.y - 0.02f + 0.01f))
        //{
        //    objShadow.GetComponent<SpriteRenderer>().sortingOrder = 0;
        //}
        //else
        //{
        //    objShadow.GetComponent<SpriteRenderer>().sortingOrder = 155;
        //}

        for (int nCnt = 0; nCnt < maxObj; nCnt++)
        {
            cupObj[nCnt].transform.position = new Vector3(this.transform.position.x + cupObjPos.x, this.transform.position.y + cupObjPos.y, this.transform.position.z + cupObjPos.z);
            if (cupObj[nCnt].transform.position.y >= (this.transform.position.y + cupObjPos.y + 0.01f))
            {
                cupObj[nCnt].GetComponent<SpriteRenderer>().sortingOrder = 0;
                
            }
            else
            {
                cupObj[nCnt].GetComponent<SpriteRenderer>().sortingOrder = 155 - (nCnt * 1);
            }
        }

        
    }

    public void setJuiceType(int type)
    {
        for (int nCnt = 0; nCnt < maxObj; nCnt++)
        {
            if (cupObj[nCnt].GetComponent<juiceChange>().type == 5)
            {
                cupObj[nCnt].GetComponent<juiceChange>().type = type;
                cupObj[nCnt].GetComponent<juiceChange>().textureNo = nCnt + 1;
                break;
            }
        }
    }

    public void resetJuice()
    {
        for (int nCnt = 0; nCnt < maxObj; nCnt++)
        {
            cupObj[nCnt].GetComponent<juiceChange>().type = 5;
            cupObj[nCnt].GetComponent<juiceChange>().textureNo = 0;
        }
    }

    public static int GetScoreNumList(int no)
    {
        return scoreNumList[no];
    }

    public int getPercentType()
    {
        int fruitCnt = 0;
        int type;
        for (int nCnt = 0; nCnt < maxObj; nCnt++)
        {
            scoreNumList[0]++;
            if (juiceManager.getType() == cupObj[nCnt].GetComponent<juiceChange>().type)
            {
                fruitCnt++;
                scoreNumList[1]++;
            }
        }

        juiceList[0]++;

        if (fruitCnt == maxObj)
        {
            type = 3;
            juiceList[4]++;
        }
        else if (fruitCnt == 0)
        {
            type = 0;
            juiceList[1]++;
        }
        else if (fruitCnt >= 1 && fruitCnt <= 3)
        {
            type = 1;
            juiceList[2]++;
        }
        else
        {
            type = 2;
            juiceList[3]++;
        }

        return type;
    }

    public static int getJuiceList(int no)
    {
        return juiceList[no];
    }
}
