using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juiceManager : MonoBehaviour {

    //共通の座標とサイズ
    public Vector3 parentPos;
    public Vector3 parentSize;

    //ターゲット用
    public GameObject targetFrame;      //目標物を囲うもの
    GameObject targetFrameData;
    public Vector3 framePos;
    public Vector3 frameSize;

    public Vector3 pointPos;
    public Vector3 pointSize;

    public GameObject FruitTag;     //目標物
    public Sprite[] FruitTex;       //目標物のテクスチャ
    public GameObject targetData;        //目標物のデータ
    public Vector3 targetPos;
    public Vector3 targetSize;

    //吹き出し
    public GameObject bubbleMiddle;
    GameObject bubbleMiddleData;
    public Vector3 bubblePos;
    public Vector3 bubbleSize;

    public int maxObj;

    GameObject scoreObj;
    scoreManager scoreData;

    static int hitCount = 0;

    static int nRandomFruit;

    public float moveSpeed;
    public float bubbleSpeed;

    public bool randomUse;
    
    int type = 0;

	// Use this for initialization
	void Start () {
        nRandomFruit = Random.Range(0, 5);

        targetFrameData = Instantiate(targetFrame, new Vector3(parentPos.x + framePos.x, parentPos.y + framePos.y, parentPos.z + framePos.z), Quaternion.identity);       //目標物を囲うものを生成
        targetFrameData.GetComponent<targetCupMove>().moveSpeed = moveSpeed;
        targetFrameData.GetComponent<targetData>().point = 0;
        targetFrameData.GetComponent<targetData>().type = nRandomFruit;
        targetFrameData.GetComponent<targetData>().maxObj = maxObj;
        //targetFrameData.GetComponent<targetData>().objPos = pointPos;
        //targetFrameData.GetComponent<targetData>().objSize = pointSize;
        targetFrameData.GetComponent<targetData>().parentSize = parentSize + frameSize;

        //スコアのターゲット設定
        scoreObj = GameObject.Find("scoreManager");
        scoreData = scoreObj.GetComponent<scoreManager>();
        scoreData.SetType(nRandomFruit);

        //ターゲット設定
        FruitTag.GetComponent<SpriteRenderer>().sprite = FruitTex[nRandomFruit];      //テクスチャの設定
        targetData = Instantiate(FruitTag, new Vector3(parentPos.x + targetPos.x, parentPos.y + targetPos.y, parentPos.z + targetPos.z), Quaternion.identity);       //ターゲットの生成
        //targetData.GetComponent<targetData>().point = 0;
        //targetData.GetComponent<targetData>().type = nRandomFruit;
        //targetData.GetComponent<targetData>().maxObj = maxObj;
        //targetData.GetComponent<targetData>().parentSize = parentSize + frameSize;
        targetData.GetComponent<targetCupMove>().moveSpeed = moveSpeed;

        //吹き出し設定
        //bubbleMiddleData = Instantiate(bubbleMiddle, new Vector3(parentPos.x + bubblePos.x, parentPos.y + bubblePos.y, parentPos.z + bubblePos.z), Quaternion.identity);       //吹き出し（中央）の生成
        //bubbleMiddleData.GetComponent<bubbleMoveKill>().type = targetData.GetComponent<targetData>().type;
        //bubbleMiddleData.GetComponent<bubbleMoveKill>().type = targetFrameData.GetComponent<targetData>().type;
        //bubbleMiddleData.GetComponent<bubbleMoveKill>().moveSpeed = bubbleSpeed;

        //サイズ設定
        targetFrameData.transform.localScale = new Vector3(parentSize.x * frameSize.x, parentSize.y * frameSize.y, parentSize.z * frameSize.z);
        targetData.transform.localScale = new Vector3(parentSize.x * targetSize.x, parentSize.y * targetSize.y, parentSize.z * targetSize.z);
        //bubbleMiddleData.transform.localScale = new Vector3(parentSize.x * bubbleSize.x, parentSize.y * bubbleSize.y, parentSize.z * bubbleSize.z);

	}
	
	// Update is called once per frame
	void Update () {

        

        //targetData.GetComponent<targetData>().point = hitCount;

        //if (targetData.GetComponent<targetData>().point >= maxObj)
        //{
        //    targetData.GetComponent<targetData>().point = maxObj;
        //}
        targetFrameData.GetComponent<targetData>().point = hitCount;

        if (targetFrameData.GetComponent<targetData>().point >= maxObj)
        {
            targetFrameData.GetComponent<targetData>().point = maxObj;
        }

        //ターゲット変更
        if (hitCount >= maxObj)
        {
            if (randomUse == true)
            {
                nRandomFruit = Random.Range(0, 5);
                //do
                //{
                //    nRandomFruit = Random.Range(0, 5);
                //} while (targetData.GetComponent<targetData>().type == nRandomFruit);
                do
                {
                    nRandomFruit = Random.Range(0, 5);
                } while (targetFrameData.GetComponent<targetData>().type == nRandomFruit);
            }
            //Debug.Log(targetData.GetComponent<targetData>().type + "||" + nRandomFruit);

            GameObject target = GameObject.Find("targetObj(Clone)");
            targetData targetDataCS = target.GetComponent<targetData>();
            type = targetDataCS.getPercentType();

            bubbleMiddleData = Instantiate(bubbleMiddle, new Vector3(parentPos.x + bubblePos.x, parentPos.y + bubblePos.y, parentPos.z + bubblePos.z), Quaternion.identity);       //吹き出し（中央）の生成
            bubbleMiddleData.transform.localScale = new Vector3(parentSize.x * bubbleSize.x, parentSize.y * bubbleSize.y, parentSize.z * bubbleSize.z);
            bubbleMiddleData.GetComponent<bubbleMoveKill>().type = type;
            bubbleMiddleData.GetComponent<bubbleMoveKill>().moveSpeed = bubbleSpeed;

            scoreData.SetType(nRandomFruit);
            targetFrameData.GetComponent<targetCupMove>().moveFlag = true;
            targetFrameData.GetComponent<targetCupMove>().type = 1;
            targetData.GetComponent<SpriteRenderer>().sprite = FruitTex[nRandomFruit];      //テクスチャの設定
            //targetData.GetComponent<targetData>().point = 0;
            targetFrameData.GetComponent<targetData>().point = 0;

            //targetData.GetComponent<targetData>().type = nRandomFruit;
            targetFrameData.GetComponent<targetData>().type = nRandomFruit;
            targetData.GetComponent<targetCupMove>().moveFlag = true;
            targetData.GetComponent<targetCupMove>().type = 0;
            hitCount = 0;
        }
	}

    public static void setPoint(int point)
    {
        hitCount += point;
    }

    public static int getType()
    {
        return nRandomFruit;
    }
}
