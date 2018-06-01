using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//シーン切り替え
public class rankingChange : MonoBehaviour
{
	public bool title = false;
	public bool reset = false;
	public GameObject soundObj;

	GameObject rankingObjData;       //変数
	const int MAX_NUM = 10;                   //最大表示数
											  //セーブデータ

	// Use this for initialization
	void Start()
	{
	}

	public void TitelClick()
	{
		title = true;
	}

	public void ResetClick()
	{
		reset = true;
	}


	// Update is called once per frame
	void FixedUpdate()
	{
		if (title == true)
		{
			ranking.SetRanking(0);
			if (soundObj = GameObject.Find("resultSound(Clone)"))
			{
				DestroyImmediate(soundObj);
			}
			else if (soundObj = GameObject.Find("resultSound"))
			{
				DestroyImmediate(soundObj);
			}
			SceneManager.LoadScene("Title");
		}

		if (reset == true)
		{
			rankingObjData = GameObject.Find("ranking");      //取得
			ranking rankingReset = rankingObjData.GetComponent<ranking>();      //targetFruit.csを呼び出す変数
			rankingReset.RankingReset();
			SceneManager.LoadScene("Ranking");
		}

	}




}
