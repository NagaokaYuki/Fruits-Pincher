using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

//シーン切り替え
public class titleChange : MonoBehaviour
{

	GameObject ModeAData;
	GameObject RankingData;
    GameObject rankingObjData;       //変数
    //GameObject tutorialData;
    GameObject staffData;
    //private AudioSource titleBGM;
    const int MAX_NUM = 10;                   //最大表示数
    //セーブデータ

	// Use this for initialization
	void Start()
	{
        //titleBGM = GetComponent<AudioSource>();
		//モードとランキングの情報取得
		ModeAData = GameObject.Find("GameModeA");
		RankingData = GameObject.Find("RankingMode");
       // tutorialData = GameObject.Find("Tutorial");
	}

	// Update is called once per frame
	void FixedUpdate()
	{
       // titleBGM.PlayOneShot(titleBGM.clip);
        //// マウス座標（テスト用）
        //Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);


        //// 押されていたら(マウス)
        //if (Input.GetMouseButton(0))
        //{
        //    //モードAを押したらしたら、ゲームへ
        //    if (ModeAData.transform.position.x - 3 < mousePos.origin.x && ModeAData.transform.position.x + 3 > mousePos.origin.x &&
        //        ModeAData.transform.position.y - 1 < mousePos.origin.y && ModeAData.transform.position.y + 1 > mousePos.origin.y)
        //    {
        //        SceneManager.LoadScene("countDown");
        //    }

        //    //ランキングを押したらしたら、ランキングへ
        //    if (RankingData.transform.position.x - 3 < mousePos.origin.x && RankingData.transform.position.x + 3 > mousePos.origin.x &&
        //        RankingData.transform.position.y - 1 < mousePos.origin.y && RankingData.transform.position.y + 1 > mousePos.origin.y)
        //    {
        //        //スコア設定
        //        ranking.SetRanking(0);       //初期値、やらないと前にやっていた点数がランキングに入るので、「0」にする
        ////        Debug.Log("スコア初期化");
        //        SceneManager.LoadScene("Ranking");
        //    }
        //}


        // 指のタッチ数取得
        int touchCount = Input.touches.Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
        // 押されていたら
        if (touchCount == 1)
        {
           Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

           //モードAをタッチしたら、ゲームへ
           if (ModeAData.transform.position.x - 3 < touchPos.x && ModeAData.transform.position.x + 3 > touchPos.x &&
               ModeAData.transform.position.y - 1 < touchPos.y && ModeAData.transform.position.y + 1 > touchPos.y)
           {
                           //Debug.Log("countdown!!!!!!!!!");

               SceneManager.LoadScene("countDown");
           }

           //ランキングをタッチしたら、ランキングへ
           if (RankingData.transform.position.x - 3 < touchPos.x && RankingData.transform.position.x + 3 > touchPos.x &&
               RankingData.transform.position.y - 1 < touchPos.y && RankingData.transform.position.y + 1 > touchPos.y)
           {
               //スコア設定
               ranking.SetRanking(0);       //初期値、やらないと前にやっていた点数がランキングに入るので、「0」にする
               //Debug.Log("スコア初期化");
               SceneManager.LoadScene("Ranking");
           }
        }

	}
}
