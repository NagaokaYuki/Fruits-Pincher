using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class result : MonoBehaviour
{

	public Sprite[] texture;
	public GameObject pointObj;

	//public GameObject percentObj;

	GameObject[] scoreData = new GameObject[5];
	//GameObject[] juiceData = new GameObject[5];
	GameObject[] perfectData = new GameObject[5];
	GameObject[] goodData = new GameObject[5];
	GameObject[] okData = new GameObject[5];
	GameObject[] badData = new GameObject[5];
	//GameObject[] percentData = new GameObject[5];
	//GameObject[] totalData = new GameObject[5];
	//GameObject[] correctData = new GameObject[5];

	public Vector3 scoreObjPos;
	//public Vector3 juiceObjPos;
	public Vector3 perfectObjPos;
	public Vector3 goodObjPos;
	public Vector3 okObjPos;
	public Vector3 badObjPos;
	//public Vector3 totalObjPos;
	//public Vector3 correctObjPos;

	//public Vector3 percentObjPos;

	public Vector3 fontSize;

	public float pointSpace;

	static int nScoreNow = 0;        //現在のスコア

	public Sprite[] fruitTex;
	public GameObject targetObj;
	GameObject targetObjData;
	public Vector3 targetPos;
	public Vector3 targetSize;

	public GameObject plusObj;
	public GameObject minusObj;

	public Vector3 markPos;
	public Vector3 markSize;

	GameObject[] markData = new GameObject[4];

	public GameObject resultBubble;
	GameObject[] resultBubbleData = new GameObject[4];
	public Vector3[] resultBubblePos;
	public Vector3 resultBubbleSize;

	public int perfectPoint;
	GameObject[] perfectPointData = new GameObject[5];

	public int goodPoint;
	GameObject[] goodPointData = new GameObject[5];

	public int okPoint;
	GameObject[] okPointData = new GameObject[5];

	public int badPoint;
	GameObject[] badPointData = new GameObject[5];


	public Vector3 pointSize;
	public float pointInterval;
	private int finalScore = 0;
	int nCnt = 0;

	// Use this for initialization
	void Start()
	{
		targetObj.GetComponent<SpriteRenderer>().sprite = fruitTex[juiceManager.getType()];

		targetObjData = Instantiate(targetObj, new Vector3(targetPos.x, targetPos.y, targetPos.z), Quaternion.identity);

		targetObjData.transform.localScale = new Vector3(1 * targetSize.x, 1 * targetSize.y, 1 * targetSize.z);

		//int totalFruit = targetData.GetScoreNumList(0);
		//int correctFruit = targetData.GetScoreNumList(1);
		//float percent;
		//if (totalFruit != 0)
		//{
		//    percent = (float)correctFruit / (float)totalFruit * 100;
		//}
		//else
		//{
		//    percent = 0;
		//}

		int nNum, nValue;

		//int juiceNum = targetData.getJuiceList(0);

		//nValue = juiceNum;

		//for (int RankX = 0; RankX < 5; RankX++)
		//{
		//    nNum = nValue % 10;

		//    nValue /= 10;

		//    pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

		//    juiceData[RankX] = Instantiate(pointObj, new Vector3(juiceObjPos.x - (RankX * pointSpace), juiceObjPos.y, juiceObjPos.z), Quaternion.identity);

		//    juiceData[RankX].transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);

		//    if (nValue <= 0)
		//    {
		//        break;
		//    }
		//}

		//-------------------------------------------
		//perfectの数
		//-------------------------------------------
		int perfectNum = targetData.getJuiceList(4);

		nValue = perfectNum;

		for (int RankX = 0; RankX < 5; RankX++)
		{
			nNum = nValue % 10;

			nValue /= 10;

			pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			perfectData[RankX] = Instantiate(pointObj, new Vector3(perfectObjPos.x - (RankX * pointSpace), perfectObjPos.y, perfectObjPos.z), Quaternion.identity);

			perfectData[RankX].transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);

			if (nValue <= 0)
			{
				break;
			}
		}

		//-------------------------------------------
		//goodの数
		//-------------------------------------------
		int goodNum = targetData.getJuiceList(3);

		nValue = goodNum;

		for (int RankX = 0; RankX < 5; RankX++)
		{
			nNum = nValue % 10;

			nValue /= 10;

			pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			goodData[RankX] = Instantiate(pointObj, new Vector3(goodObjPos.x - (RankX * pointSpace), goodObjPos.y, goodObjPos.z), Quaternion.identity);

			goodData[RankX].transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);

			if (nValue <= 0)
			{
				break;
			}
		}

		//-------------------------------------------
		//okの数
		//-------------------------------------------
		int okNum = targetData.getJuiceList(2);

		nValue = okNum;

		for (int RankX = 0; RankX < 5; RankX++)
		{
			nNum = nValue % 10;

			nValue /= 10;

			pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			okData[RankX] = Instantiate(pointObj, new Vector3(okObjPos.x - (RankX * pointSpace), okObjPos.y, okObjPos.z), Quaternion.identity);

			okData[RankX].transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);

			if (nValue <= 0)
			{
				break;
			}
		}

		//-------------------------------------------
		//badの数
		//-------------------------------------------
		int badNum = targetData.getJuiceList(1);

		nValue = badNum;

		for (int RankX = 0; RankX < 5; RankX++)
		{
			nNum = nValue % 10;

			nValue /= 10;

			pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			badData[RankX] = Instantiate(pointObj, new Vector3(badObjPos.x - (RankX * pointSpace), badObjPos.y, badObjPos.z), Quaternion.identity);

			badData[RankX].transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);

			if (nValue <= 0)
			{
				break;
			}
		}

		//-----------------------------------
		//吹き出し
		//-----------------------------------
		for (int i = 0; i < 4; i++)
		{
			resultBubbleData[i] = Instantiate(resultBubble, resultBubblePos[i], Quaternion.identity);
			resultBubbleData[i].transform.localScale = resultBubbleSize;
			//resultBubbleData[i].transform.localScale = new Vector3(0, 0, 0);
			resultBubbleData[i].GetComponent<resultAnime>().goalSize = resultBubbleSize;
			resultBubbleData[i].GetComponent<resultAnime>().bAnime = true;
		}

		int pointtype = 0;

		//-------------------------------------------
		//perfectPoint表示
		//-------------------------------------------
		nValue = perfectPoint * perfectNum;

		if (nValue >= 0)
		{
			markData[pointtype] = Instantiate(plusObj, new Vector3(resultBubbleData[pointtype].transform.position.x + (markPos.x * resultBubbleData[pointtype].transform.localScale.x), resultBubbleData[pointtype].transform.position.y + markPos.y, resultBubbleData[pointtype].transform.position.z + markPos.z), Quaternion.identity);
			markData[pointtype].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * markSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * markSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * markSize.z);
			//markData[pointtype].transform.localScale = new Vector3(0, 0, 0);
			markData[pointtype].GetComponent<resultAnime>().goalSize = markData[pointtype].transform.localScale;
			markData[pointtype].GetComponent<resultAnime>().bAnime = true;
		}
		else
		{
			markData[pointtype] = Instantiate(minusObj, new Vector3(resultBubbleData[pointtype].transform.position.x + (markPos.x * resultBubbleData[pointtype].transform.localScale.x), resultBubbleData[pointtype].transform.position.y + markPos.y, resultBubbleData[pointtype].transform.position.z + markPos.z), Quaternion.identity);
			markData[pointtype].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * markSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * markSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * markSize.z);
			//markData[pointtype].transform.localScale = new Vector3(0, 0, 0);
			markData[pointtype].GetComponent<resultAnime>().goalSize = markData[pointtype].transform.localScale;
			markData[pointtype].GetComponent<resultAnime>().bAnime = true;

			nValue = nValue * -1;
		}

		for (nCnt = 0; nCnt < 5;)
		{
			nValue /= 10;
			nCnt++;
			if (nValue <= 0)
			{
				break;
			}
		}

		nValue = perfectPoint * perfectNum;

		if (nValue < 0)
		{
			nValue = nValue * -1;
		}

		for (int RankX = nCnt; RankX > 0; RankX--)
		{
			nNum = nValue % 10;

			nValue /= 10;

			pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			perfectPointData[nCnt - RankX] = Instantiate(pointObj, new Vector3(markData[pointtype].transform.position.x + (RankX * (pointInterval * resultBubbleSize.x)), markData[pointtype].transform.position.y, markData[pointtype].transform.position.z), Quaternion.identity);

			perfectPointData[nCnt - RankX].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * pointSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * pointSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * pointSize.z);

			//perfectPointData[nCnt - RankX].transform.localScale = new Vector3(0, 0, 0);

			perfectPointData[nCnt - RankX].GetComponent<resultAnime>().goalSize = perfectPointData[nCnt - RankX].transform.localScale;

			perfectPointData[nCnt - RankX].GetComponent<resultAnime>().bAnime = true;

			if (nValue <= 0)
			{
				break;
			}
		}

		pointtype = 1;

		//-------------------------------------------
		//goodPoint表示
		//-------------------------------------------
		nValue = goodPoint * goodNum;

		if (nValue >= 0)
		{
			markData[pointtype] = Instantiate(plusObj, new Vector3(resultBubbleData[pointtype].transform.position.x + (markPos.x * resultBubbleData[pointtype].transform.localScale.x), resultBubbleData[pointtype].transform.position.y + markPos.y, resultBubbleData[pointtype].transform.position.z + markPos.z), Quaternion.identity);
			markData[pointtype].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * markSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * markSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * markSize.z);
			//markData[pointtype].transform.localScale = new Vector3(0, 0, 0);
			markData[pointtype].GetComponent<resultAnime>().goalSize = markData[pointtype].transform.localScale;
			markData[pointtype].GetComponent<resultAnime>().bAnime = true;
		}
		else
		{
			markData[pointtype] = Instantiate(minusObj, new Vector3(resultBubbleData[pointtype].transform.position.x + (markPos.x * resultBubbleData[pointtype].transform.localScale.x), resultBubbleData[pointtype].transform.position.y + markPos.y, resultBubbleData[pointtype].transform.position.z + markPos.z), Quaternion.identity);
			markData[pointtype].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * markSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * markSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * markSize.z);
			//markData[pointtype].transform.localScale = new Vector3(0, 0, 0);
			markData[pointtype].GetComponent<resultAnime>().goalSize = markData[pointtype].transform.localScale;
			markData[pointtype].GetComponent<resultAnime>().bAnime = true;

			nValue = nValue * -1;
		}

		for (nCnt = 0; nCnt < 5;)
		{
			nValue /= 10;
			nCnt++;
			if (nValue <= 0)
			{
				break;
			}
		}

		nValue = goodPoint * goodNum;

		if (nValue < 0)
		{
			nValue = nValue * -1;
		}

		for (int RankX = nCnt; RankX > 0; RankX--)
		{
			nNum = nValue % 10;

			nValue /= 10;

			pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			goodPointData[nCnt - RankX] = Instantiate(pointObj, new Vector3(markData[pointtype].transform.position.x + (RankX * (pointInterval * resultBubbleSize.x)), markData[pointtype].transform.position.y, markData[pointtype].transform.position.z), Quaternion.identity);

			goodPointData[nCnt - RankX].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * pointSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * pointSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * pointSize.z);

			//goodPointData[nCnt - RankX].transform.localScale = new Vector3(0, 0, 0);

			goodPointData[nCnt - RankX].GetComponent<resultAnime>().goalSize = goodPointData[nCnt - RankX].transform.localScale;

			goodPointData[nCnt - RankX].GetComponent<resultAnime>().bAnime = true;

			if (nValue <= 0)
			{
				break;
			}
		}

		pointtype = 2;

		//-------------------------------------------
		//okPoint表示
		//-------------------------------------------
		nValue = okPoint * okNum;

		if (nValue >= 0)
		{
			markData[pointtype] = Instantiate(plusObj, new Vector3(resultBubbleData[pointtype].transform.position.x + (markPos.x * resultBubbleData[pointtype].transform.localScale.x), resultBubbleData[pointtype].transform.position.y + markPos.y, resultBubbleData[pointtype].transform.position.z + markPos.z), Quaternion.identity);
			markData[pointtype].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * markSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * markSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * markSize.z);
			//markData[pointtype].transform.localScale = new Vector3(0, 0, 0);
			markData[pointtype].GetComponent<resultAnime>().goalSize = markData[pointtype].transform.localScale;
			markData[pointtype].GetComponent<resultAnime>().bAnime = true;
		}
		else
		{
			markData[pointtype] = Instantiate(minusObj, new Vector3(resultBubbleData[pointtype].transform.position.x + (markPos.x * resultBubbleData[pointtype].transform.localScale.x), resultBubbleData[pointtype].transform.position.y + markPos.y, resultBubbleData[pointtype].transform.position.z + markPos.z), Quaternion.identity);
			markData[pointtype].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * markSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * markSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * markSize.z);
			//markData[pointtype].transform.localScale = new Vector3(0, 0, 0);
			markData[pointtype].GetComponent<resultAnime>().goalSize = markData[pointtype].transform.localScale;
			markData[pointtype].GetComponent<resultAnime>().bAnime = true;

			nValue = nValue * -1;
		}

		for (nCnt = 0; nCnt < 5;)
		{
			nValue /= 10;
			nCnt++;
			if (nValue <= 0)
			{
				break;
			}
		}

		nValue = okPoint * okNum;

		if (nValue < 0)
		{
			nValue = nValue * -1;
		}

		for (int RankX = nCnt; RankX > 0; RankX--)
		{
			nNum = nValue % 10;

			nValue /= 10;

			pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			okPointData[nCnt - RankX] = Instantiate(pointObj, new Vector3(markData[pointtype].transform.position.x + (RankX * (pointInterval * resultBubbleSize.x)), markData[pointtype].transform.position.y, markData[pointtype].transform.position.z), Quaternion.identity);

			okPointData[nCnt - RankX].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * pointSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * pointSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * pointSize.z);

			//okPointData[nCnt - RankX].transform.localScale = new Vector3(0, 0, 0);

			okPointData[nCnt - RankX].GetComponent<resultAnime>().goalSize = okPointData[nCnt - RankX].transform.localScale;

			okPointData[nCnt - RankX].GetComponent<resultAnime>().bAnime = true;

			if (nValue <= 0)
			{
				break;
			}
		}

		pointtype = 3;

		//-------------------------------------------
		//badPoint表示
		//-------------------------------------------
		nValue = badPoint * badNum;

		markData[pointtype] = Instantiate(minusObj, new Vector3(resultBubbleData[pointtype].transform.position.x + (markPos.x * resultBubbleData[pointtype].transform.localScale.x), resultBubbleData[pointtype].transform.position.y + markPos.y, resultBubbleData[pointtype].transform.position.z + markPos.z), Quaternion.identity);
		markData[pointtype].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * markSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * markSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * markSize.z);
		//markData[pointtype].transform.localScale = new Vector3(0, 0, 0);
		markData[pointtype].GetComponent<resultAnime>().goalSize = markData[pointtype].transform.localScale;
		markData[pointtype].GetComponent<resultAnime>().bAnime = true;
		nValue = nValue * -1;

		for (nCnt = 0; nCnt < 5;)
		{
			nValue /= 10;
			nCnt++;
			if (nValue <= 0)
			{
				break;
			}
		}

		nValue = badPoint * badNum;

		if (nValue < 0)
		{
			nValue = nValue * -1;
		}

		for (int RankX = nCnt; RankX > 0; RankX--)
		{
			nNum = nValue % 10;

			nValue /= 10;

			pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			badPointData[nCnt - RankX] = Instantiate(pointObj, new Vector3(markData[pointtype].transform.position.x + (RankX * (pointInterval * resultBubbleSize.x)), markData[pointtype].transform.position.y, markData[pointtype].transform.position.z), Quaternion.identity);

			badPointData[nCnt - RankX].transform.localScale = new Vector3(1 * resultBubbleData[pointtype].transform.localScale.x * pointSize.x, 1 * resultBubbleData[pointtype].transform.localScale.y * pointSize.y, 1 * resultBubbleData[pointtype].transform.localScale.z * pointSize.z);
			//badPointData[nCnt - RankX].transform.localScale = new Vector3(0, 0, 0);
			badPointData[nCnt - RankX].GetComponent<resultAnime>().goalSize = badPointData[nCnt - RankX].transform.localScale;

			badPointData[nCnt - RankX].GetComponent<resultAnime>().bAnime = true;


			if (nValue <= 0)
			{
				break;
			}
		}

		int perfectScore = perfectNum * perfectPoint;
		int goodScore = goodNum * goodPoint;
		int okScore = okNum * okPoint;
		int badScore = badNum * badPoint;

		//-----------------------------------
		//スコア最終結果
		//-----------------------------------
		nValue = nScoreNow + perfectScore + goodScore + okScore + badScore;
		finalScore = nValue;

		for (int RankX = 0; RankX < 5; RankX++)
		{
			nNum = nValue % 10;

			nValue /= 10;

			pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			scoreData[RankX] = Instantiate(pointObj, new Vector3(scoreObjPos.x - (RankX * pointSpace), scoreObjPos.y, scoreObjPos.z), Quaternion.identity);

			scoreData[RankX].transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);
		}

		//nValue = totalFruit;

		//for (int RankX = 0; RankX < 5; RankX++)
		//{
		//    nNum = nValue % 10;

		//    nValue /= 10;

		//    pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

		//    totalData[RankX] = Instantiate(pointObj, new Vector3(totalObjPos.x - (RankX * pointSpace), totalObjPos.y, totalObjPos.z), Quaternion.identity);

		//    totalData[RankX].transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);

		//    if (nValue <= 0)
		//    {
		//        break;
		//    }
		//}

		//nValue = correctFruit;

		//for (int RankX = 0; RankX < 5; RankX++)
		//{
		//    nNum = nValue % 10;

		//    nValue /= 10;

		//    pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

		//    correctData[RankX] = Instantiate(pointObj, new Vector3(correctObjPos.x - (RankX * pointSpace), correctObjPos.y, correctObjPos.z), Quaternion.identity);

		//    correctData[RankX].transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);

		//    if (nValue <= 0)
		//    {
		//        break;
		//    }
		//}

		//nValue = (int)percent;

		////float fValue = percent;
		//for (int RankX = 0; RankX < 5; RankX++)
		//{
		//    nNum = nValue % 10;

		//    nValue /= 10;

		//    pointObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

		//    percentData[RankX] = Instantiate(pointObj, new Vector3(percentObjPos.x - (RankX * pointSpace), percentObjPos.y, percentObjPos.z), Quaternion.identity);

		//    percentData[RankX].transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);

		//    if (nValue <= 0)
		//    {
		//        break;
		//    }
		//}

		//GameObject percentFont = Instantiate(percentObj, new Vector3(percentData[0].transform.position.x - (-1 * pointSpace), percentData[0].transform.position.y, percentData[0].transform.position.z), Quaternion.identity);
		//percentFont.transform.localScale = new Vector3(1 * fontSize.x, 1 * fontSize.y, 1 * fontSize.z);

		ranking.SetRanking(finalScore);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ButtonPress()
	{
		SceneManager.LoadScene("ranking");
	}

	public static void SetScore(int score)
	{
		//Debug.Log(score);
		nScoreNow = score;
	}
}
