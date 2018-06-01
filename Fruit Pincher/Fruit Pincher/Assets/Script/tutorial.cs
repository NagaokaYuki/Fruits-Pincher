using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{

	public GameObject tutorialObj;      //チュートリアルオブジェクト
	const int nPageNum = 4;         //チュートリアルのページ数
	static int nPageNow = 0;        //現在のチュートリアルのページ
	public Sprite[] texture = new Sprite[nPageNum];        //チュートリアルのテクスチャ、ただし、sizeとnPageNumの数を同じにする
	GameObject[] tutorialObjData = new GameObject[nPageNum];     //チュートリアルオブジェクトのデータ
	float[] tutorialGoalData = new float[nPageNum];

	GameObject backData;
	GameObject nextData;
	GameObject startData;

	public bool prev = false;
	public bool next = false;
	public bool start = false;

	// Use this for initialization
	void Start()
	{
		nPageNow = 0;       //初期化

		for (int nCntX = 0; nCntX < nPageNum; nCntX++)
		{
			tutorialObj.GetComponent<SpriteRenderer>().sprite = texture[nCntX];     //テクスチャ設定
			tutorialObjData[nCntX] = Instantiate(tutorialObj, new Vector3(0 + (nCntX * 10), 0, 0), Quaternion.identity);        //生成
			tutorialGoalData[nCntX] = tutorialObjData[nCntX].transform.position.x;
			tutorialObjData[nCntX].GetComponent<tutorialData>().tutorialGoal = tutorialGoalData[nCntX];
			//Debug.Log(tutorialGoalData[nCntX]);
		}

		//ボタンの情報取得
		backData = GameObject.Find("Back");
		nextData = GameObject.Find("Next");
		startData = GameObject.Find("Start");


	}

	// Update is called once per frame
	void Update()
	{

		if (nPageNow > 0)
		{
			startData.gameObject.SetActive(false);
			backData.gameObject.SetActive(true);
			//前に戻す
			if (prev == true )
			{
				nPageNow--;
				prev = false;

                //Debug.Log("前" + nPageNow);

				for (int nCntX = 0; nCntX < nPageNum; nCntX++)
				{
					tutorialObjData[nCntX].GetComponent<tutorialData>().tutorialMoveFlg = true;
					tutorialObjData[nCntX].GetComponent<tutorialData>().tutorialGoal = tutorialObjData[nCntX].GetComponent<tutorialData>().tutorialGoal + 10;
				}
			}
		}
		else
		{
			startData.gameObject.SetActive(false);
			backData.gameObject.SetActive(false);
		}

		if (nPageNow < nPageNum - 1)
		{
			startData.gameObject.SetActive(false);
			nextData.gameObject.SetActive(true);

			//次へ
			if (next == true)
			{
				nPageNow++;
				next = false;

                //Debug.Log("後" + nPageNow);

				for (int nCntX = 0; nCntX < nPageNum; nCntX++)
				{
					tutorialObjData[nCntX].GetComponent<tutorialData>().tutorialMoveFlg = true;
					tutorialObjData[nCntX].GetComponent<tutorialData>().tutorialGoal = tutorialObjData[nCntX].GetComponent<tutorialData>().tutorialGoal - 10;
                    //Debug.Log(tutorialObjData[nCntX].transform.position.x - 10);
				}
			}
		}
		else
		{
			nextData.gameObject.SetActive(false);
			startData.gameObject.SetActive(true);

			if (start == true)
			{
				start = false;
				SceneManager.LoadScene("title");

			}
		}
	}

	public void prevPress()
	{
		prev = true;
	}

	public void nextPress()
	{
		next = true;
	}

	public void startPress()
	{
		start = true;
	}
}
