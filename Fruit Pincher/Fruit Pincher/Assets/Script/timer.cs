using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{

	public GameObject timerObj;     //タイマーのオブジェクト
	public GameObject pausing;
	public GameObject restart;
	public GameObject menu;
	private AudioSource[] sources;
	private AudioSource[] finish;
	public GameObject pauseLayer;
	public GameObject countinue;

	static int timerNow;         //現在のタイマー
	const int MAX_DIGIT = 2;        //最大桁数
	private GameObject[] timerList = new GameObject[MAX_DIGIT];        //桁数分の配列
	public Sprite[] texture;        //タイマーの数字テクスチャ
	public int turnTime;

	public GameObject timerFrame;       //数字を囲うもの

	public GameObject finishObj;        //終了画像のオブジェクト
	private bool finishObjFlag = false;
	static int sceneCnt;        //シーンカウント
	static private bool sound3s = true;
	static private bool sound5s = true;
	public bool pause;
	public GameObject pusherManager;
	private int cntFrame = 0;
	public Vector3 timerPos;

	public Vector3 timerFramePos;

	// Use this for initialization
	void Start()
	{
		//タイマー初期化
		timerNow = turnTime * 60;

		//タイマーのオブジェクト生成
		int nNum, nValue = timerNow / 60;

		for (int nCnt = 0; nCnt < MAX_DIGIT; nCnt++)
		{
			nNum = nValue % 10;

			nValue /= 10;

			timerObj.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			Instantiate(timerFrame, new Vector3(timerFramePos.x - (nCnt * 1.0f), timerFramePos.y, timerFramePos.z), Quaternion.identity);

			timerList[nCnt] = Instantiate(timerObj, new Vector3(timerPos.x - (nCnt * 1.0f), timerPos.y, timerPos.z), Quaternion.identity);
		}

		//シーンカウントを初期化
		sceneCnt = 0;

		finish = gameObject.GetComponents<AudioSource>();
		sources = pusherManager.GetComponents<AudioSource>();

	}

	// Update is called once per frame
	void Update()
	{
		//タイマーテクスチャ変更
		int nNum, nValue = timerNow / 60;

		if (pause)
		{
			if (pausing.activeSelf == true)
			{
				if (cntFrame % 50 == 0)
				{
					pausing.GetComponent<Renderer>().material.color = Color.grey;

				}
				if (cntFrame % 100 == 0)
				{
					pausing.GetComponent<Renderer>().material.color = Color.white;

				}

				if (cntFrame >= 100)
				{
					cntFrame = 0;
				}
				cntFrame++;

			}
		}
		else
		{
			for (int nCnt = 0; nCnt < MAX_DIGIT; nCnt++)
			{
				nNum = nValue % 10;

				nValue /= 10;

				timerList[nCnt].GetComponent<SpriteRenderer>().sprite = texture[nNum];
				if ((timerNow / 60) <= 2)
				{
					//sound3s = true;
					if (sound3s)
					{
						finish[2].Stop();
						finish[1].Play();
						sound3s = false;
						sound5s = false;
					}
					if (timerNow % 2 == 0)
					{
						timerList[nCnt].GetComponent<SpriteRenderer>().material.color = Color.red;

					}
					if (timerNow % 4 == 0)
					{
						timerList[nCnt].GetComponent<SpriteRenderer>().material.color = Color.white;
					}
				}
				else if ((timerNow / 60) <= 6)
				{

					if (sound5s)
					{
						finish[2].Play();
						sound3s = true;
						sound5s = false;
					}
					if (timerNow % 5 == 0)
					{
						timerList[nCnt].GetComponent<SpriteRenderer>().material.color = Color.red;

					}
					if (timerNow % 10 == 0)
					{
						timerList[nCnt].GetComponent<SpriteRenderer>().material.color = Color.white;
					}
				}
				else if ((timerNow / 60) <= 10)
				{
					if (timerNow % 15 == 0)
					{
						timerList[nCnt].GetComponent<SpriteRenderer>().material.color = Color.red;

					}
					if (timerNow % 30 == 0)
					{
						timerList[nCnt].GetComponent<SpriteRenderer>().material.color = Color.white;
					}
				}
				else
				{
						timerList[nCnt].GetComponent<SpriteRenderer>().material.color = Color.white;
				}
				
			}

			if((timerNow / 60) > 6)
			{
				finish[2].Stop();
				finish[1].Stop();
				sound3s = true;
				sound5s = true;
			}
			if (timerNow == 0)       //「Finish」表示
			{
				finish[1].Stop();
				if (finishObjFlag == false)
				{
					// FINISH音声
					finish[0].Play();
					sources[0].Stop();
					Instantiate(finishObj, new Vector3(0, 0, 0), Quaternion.identity);
					finishObjFlag = true;
					pusherManager.GetComponent<pushManager>().gameFinish();
				}

				if (sceneCnt > 2.0f * 60)        //1.5秒後ランキングへ
				{
					SceneManager.LoadScene("result");
					sceneCnt = 0;
				}

				sceneCnt++;
			}
			else
			{
				timerNow--;
			}
		}




	}

	//タイマー情報取得
	public static int GetTime()
	{
		return timerNow;
	}

	public static void setTime(int point)
	{
		timerNow += point;

		if (timerNow >= 100 * 60)
		{
			timerNow = 100 * 60;
		}

        if (timerNow <= 0 * 60)
        {
            timerNow = 0 * 60;
        }
	}

	public void pauseSwitch()
	{
		if (!pause)
		{
			pause = true;
			sources[0].Pause();
			finish[2].Pause();
			finish[1].Pause();
			sound3s = true;
			sound5s = true;
			Time.timeScale = 0;
			pausing.SetActive(true);
			restart.SetActive(true);
			menu.SetActive(true);
			countinue.SetActive(true);
			pauseLayer.SetActive(true);
		}
	}

	public void continueSwitch()
	{
		if (pause)
		{
			Time.timeScale = 1;
			sources[0].Play();
			pause = false;
			pausing.SetActive(false);
			restart.SetActive(false);
			menu.SetActive(false);
			countinue.SetActive(false);
			pauseLayer.SetActive(false);
		}
	}

	public void restartSweith()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("countDown");
	}

	public void menuSwitch()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("title");

	}
}
