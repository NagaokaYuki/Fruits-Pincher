using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ranking : MonoBehaviour
{
	public GameObject rankingLogo;      //ランキングロゴ
	public GameObject soundObj;
	const int MAX_DIGIT = 5;        //最大桁数
	const int MAX_NUM = 10;         //最大表示数
	int[] nRanking = new int[MAX_NUM];        //ランキング配列&初期化
	static int nScoreNow = 0;        //現在のスコア
	public Sprite[] texture;
	public GameObject number;
	private GameObject[,] RankingList = new GameObject[MAX_NUM, MAX_DIGIT];
	float[] Goal = { 5.0f, 3.2f, 1.7f, 0.5f, -0.5f, -1.5f, -2.5f, -3.5f, -4.5f, -5.5f };      //目標地点
	float[,] rankingGoal = new float[MAX_NUM, MAX_DIGIT];        //各目標地点
	static int nMoveCnt = 0;   //移動カウント
	static int nRankingCnt = 0;     //ランキングの識別

	//セーブデータ
	string[] seveData = { "ranking1", "ranking2", "ranking3", "ranking4", "ranking5", "ranking6", "ranking7", "ranking8", "ranking9", "ranking10" };

	// Use this for initialization
	void Start()
	{
		if (GameObject.Find("resultSound(Clone)") == null && GameObject.Find("resultSound") == null)
		{
			Instantiate(soundObj);
		}
		//ロゴ生成
		Instantiate(rankingLogo, new Vector3(0, 10, 2), Quaternion.identity);

		//nScoreNow = Random.Range(0, 200);

		for (int nCnt = 0; nCnt < MAX_NUM; nCnt++)
		{
			nRanking[nCnt] = PlayerPrefs.GetInt(seveData[nCnt], 0);
			//Debug.Log("データ出力" + ":" + nCnt + ":" + nRanking[nCnt]);

		}

		int nScoreMin = nRanking[MAX_NUM - 1];       //最低スコア
		int nMinCnt = 0;        //最低スコアの位置

		//現在あるランキングの中から最低スコアを取得
		for (int nCnt = 0; nCnt < MAX_NUM; nCnt++)
		{
			if (nRanking[nCnt] <= nScoreMin)
			{
				nScoreMin = nRanking[nCnt];

				nMinCnt = nCnt;
			}
		}

		//Debug.Log("最低スコア:" + nScoreMin + "番号:" + nMinCnt);

		//現在のスコアと最低スコアを比較
		if (nRanking[nMinCnt] < nScoreNow)
		{
			//Debug.Log("最低スコアと交換" + nScoreMin + ":" + nScoreNow);
			nRanking[nMinCnt] = nScoreNow;      //最低スコアと入れ替え
		}

		//並び替え
		int unMaxScore = 0;
		int nTempIndex = 0;
		for (int nSort = 0; nSort < MAX_NUM; nSort++)
		{
			for (int nSort2 = nSort; nSort2 < MAX_NUM; nSort2++)
			{
				if (nRanking[nSort2] > unMaxScore)
				{
					unMaxScore = nRanking[nSort2];

					nTempIndex = nSort2;

					//Debug.Log("最大値と番号" + nSort + ":" +  unMaxScore + ":" + nTempIndex);

				}
			}
			//Debug.Log("入れ替え1:" + nRanking[nTempIndex] + ":" + nTempIndex + "＝" + nRanking[nSort] + ":" + nSort);

			nRanking[nTempIndex] = nRanking[nSort];

			//Debug.Log("入れ替え2:" + nRanking[nSort] + ":" + nSort + "＝" + unMaxScore);

			nRanking[nSort] = unMaxScore;

			nTempIndex = nSort + 1;
			unMaxScore = 0;
		}

		//確認
		//for (int nCnt = 0; nCnt < MAX_NUM; nCnt++)
		//{
		//    Debug.Log(nCnt + ":" + Goal[nCnt]);
		//}

		//表示

		int nNum, nValue;

		for (int RankY = 0; RankY < MAX_NUM; RankY++)
		{
			nValue = nRanking[RankY];

			for (int RankX = 0; RankX < MAX_DIGIT; RankX++)
			{
				nNum = nValue % 10;

				//Debug.Log(RankX + RankY + ":" + nNum);

				nValue /= 10;

				number.GetComponent<SpriteRenderer>().sprite = texture[nNum];

				RankingList[RankY, RankX] = Instantiate(number, new Vector3(2.0f - (RankX * 1), -30 - (RankY * 1.3f), 2), Quaternion.identity);
			}
		}

		//色とサイズ変更
		for (int nSize = 0; nSize < 3; nSize++)
		{
			for (int RankX = 0; RankX < MAX_DIGIT; RankX++)
			{
				if (nSize == 0)     //赤
				{
					RankingList[nSize, RankX].GetComponent<Renderer>().material.color = new Color32(255, 210, 63, 255);
					RankingList[nSize, RankX].transform.localScale = new Vector3(0.55f, 0.55f, 1);
				}
				else if (nSize == 1)        //緑
				{
					RankingList[nSize, RankX].GetComponent<Renderer>().material.color = new Color32(193, 206, 213, 255);
					RankingList[nSize, RankX].transform.localScale = new Vector3(0.45f, 0.45f, 1);
				}
				else if (nSize == 2)        //青
				{
					RankingList[nSize, RankX].GetComponent<Renderer>().material.color = new Color32(200, 110, 55, 255);
					RankingList[nSize, RankX].transform.localScale = new Vector3(0.38f, 0.38f, 1);
				}

				//サイズ変更
			}
		}

		//各目標地点設定
		for (int RankY = 0; RankY < MAX_NUM; RankY++)
		{
			for (int RankX = 0; RankX < MAX_DIGIT; RankX++)
			{
				rankingGoal[RankY, RankX] = Goal[RankY];
			}
		}

		int nCntID = 0;     //識別IDカウント
							//識別と目標地点設定
		for (int RankY = 0; RankY < MAX_NUM; RankY++)
		{
			for (int RankX = 0; RankX < MAX_DIGIT; RankX++)
			{
				RankingList[RankY, RankX].GetComponent<rankingMove>().rankingID = nCntID;

				nCntID++;

				RankingList[RankY, RankX].GetComponent<rankingMove>().rankingGoal = rankingGoal[RankY, RankX];
			}
		}

		//移動のカウント初期化
		nMoveCnt = 0;
		//識別初期化
		nRankingCnt = 0;

		//ランキングの中身セーブ
		for (int nCnt = 0; nCnt < MAX_NUM; nCnt++)
		{
			PlayerPrefs.SetInt(seveData[nCnt], nRanking[nCnt]);
			PlayerPrefs.Save();
		}

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//各スコア移動
		if (nRankingCnt < MAX_NUM)
		{
			for (int RankX = 0; RankX < MAX_DIGIT; RankX++)
			{
				RankingList[nRankingCnt, RankX].GetComponent<rankingMove>().rankingMoveFlg = true;
			}

			nMoveCnt++;

			if (nMoveCnt > 10)
			{
				nMoveCnt = 0;

				nRankingCnt++;
			}
		}


	}

	public static void SetRanking(int score)
	{
		//Debug.Log(score);
		nScoreNow = score;
	}

	public void RankingReset()
	{
		nScoreNow = 0;

		for (int nCnt = 0; nCnt < MAX_NUM; nCnt++)
		{
			nRanking[nCnt] = 0;
		}

		//ランキングの中身セーブ
		for (int nCnt = 0; nCnt < MAX_NUM; nCnt++)
		{
			PlayerPrefs.SetInt(seveData[nCnt], nRanking[nCnt]);
			PlayerPrefs.Save();
		}
	}
}
