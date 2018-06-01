using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

// プッシャーのマネジャークラス
public class pushManagerTitle : MonoBehaviour
{
	public bool DestroyFruitNow = false;
	// 左右のプッシャーオブジェクト
	public GameObject pusherR;
	public GameObject pusherL;
	public GameObject rankingButton;
	public GameObject staffButton;
	public float pusherDistance;

	// プッシャー情報の容器
	private GameObject pusherR_info;
	private GameObject pusherL_info;


	// 当たり判定用のオブジェクト
	public GameObject hitPolygonTitle;
	// 当たり判定用のオブジェクト情報の容器

	// プッシャーが作成された瞬間、果物に挿入する時の速度
	public float pushSpeed;

	// プッシャーの角度
	private float angle_L;
	private float angle_R;
	private GameObject hitPolygon_info;

	// 指の状態
	private bool fingerRelease;

	//果物が削除された時に使用するパーティクル
	public GameObject particleObj;
	public GameObject titlePusher;

	// タッチ・マウス操作
	public bool touchPlay = true;

	// Use this for initialization
	void Start()
	{
		fingerRelease = true;
		hitPolygon_info = Instantiate(hitPolygonTitle) as GameObject;
		hitPolygon_info.GetComponent<hitObjTitle>().pm = this;
		hitPolygon_info.SetActive(false);
        
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (rankingButton.GetComponent<buttonStatus>().pressed)
		{
			rankingButton.GetComponent<buttonStatus>().pressed = false;
			SceneManager.LoadScene("ranking");
		}
		if (staffButton.GetComponent<buttonStatus>().pressed)
		{
			staffButton.GetComponent<buttonStatus>().pressed = false;
			SceneManager.LoadScene("staff");
		}


		//==================================================================================================================
		// MOUSE DEBUG

		if (!touchPlay)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
				hitPolygon_info.transform.position = new Vector3(mouse.x, mouse.y, 2.42f);
				//hitPolygon_info.transform.rotation = pusherL_info.transform.rotation;
				hitPolygon_info.SetActive(true);
			}
			if (hitPolygon_info.gameObject.activeSelf && DestroyFruitNow)
			{
				pusherAction();
			}
		}

		// MOUSE DEBUG END
		//==================================================================================================================


		//==================================================================================================================
		// TOUCH

		else
		{
			// 指のタッチ数取得
			int touchCount = Input.touches.Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);

			// 二本の指でタッチし、プッシャーまだ作成されてない場合
			if (touchCount >= 2 && pusherR_info == null && pusherL_info == null)
			{
				titlePusher.SetActive(false);
				// 指の座標を取得
				Vector3 touchA = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Vector3 touchB = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
				Vector3 makeLeft = touchA + Vector3.Normalize(touchB - touchA) * 0.6f;
				Vector3 makeRight = touchB + Vector3.Normalize(touchA - touchB) * 0.6f;

				// 指の下でプッシャーを作成する

				pusherL_info = Instantiate(pusherR, new Vector3(makeLeft.x, makeLeft.y, 0.0f), Quaternion.identity);
				pusherR_info = Instantiate(pusherL, new Vector3(makeRight.x, makeRight.y, 0.0f), Quaternion.identity);

				// プッシャーの相対位置による角度を確定する
				angle_L = Vector3.Angle(pusherR_info.transform.position - pusherL_info.transform.position, new Vector3(0, 1, 0));
				angle_R = Vector3.Angle(pusherL_info.transform.position - pusherR_info.transform.position, new Vector3(0, 1, 0));
				if (pusherL_info.transform.position.x > pusherR_info.transform.position.x)
				{
					pusherL_info.transform.localEulerAngles = new Vector3(0, 0, -angle_R);
					pusherR_info.transform.localEulerAngles = new Vector3(0, 0, angle_L);
				}
				else
				{
					pusherL_info.transform.localEulerAngles = new Vector3(0, 0, angle_R);
					pusherR_info.transform.localEulerAngles = new Vector3(0, 0, -angle_L);
				}

			}
			// 指が押されてないかつプッシャーが存在する場合、プッシャーと当たり判定オブジェクトを削除する
			else if (touchCount < 2)
			{
				Destroy(pusherL_info);
				Destroy(pusherR_info);
				hitPolygon_info.SetActive(false);

				// 指の状態を更新する
				fingerRelease = true;
				titlePusher.SetActive(true);
				
				//hitCheckFrame = 0;

			}

			// 二本の指でタッチし続け、プッシャーも存在している場合
			if (touchCount >= 2 && pusherR_info != null && pusherL_info != null)
			{
				// プッシャーの座標と指の位置を同期する
				Vector3 touchA = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Vector3 touchB = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
				Vector3 makeLeft = touchA + Vector3.Normalize(touchB - touchA) * 0.6f;
				Vector3 makeRight = touchB + Vector3.Normalize(touchA - touchB) * 0.6f;
				pusherL_info.transform.position = new Vector3(makeLeft.x, makeLeft.y, pusherL_info.transform.position.z);
				pusherR_info.transform.position = new Vector3(makeRight.x, makeRight.y, pusherR_info.transform.position.z);
				// 角度を常に対面するように計算
				angle_L = Vector3.Angle(pusherR_info.transform.position - pusherL_info.transform.position, new Vector3(0, 1, 0));
				angle_R = Vector3.Angle(pusherL_info.transform.position - pusherR_info.transform.position, new Vector3(0, 1, 0));
				if (pusherL_info.transform.position.x > pusherR_info.transform.position.x)
				{
					pusherL_info.transform.localEulerAngles = new Vector3(0, 0, -angle_R);
					pusherR_info.transform.localEulerAngles = new Vector3(0, 0, angle_L);
				}
				else
				{
					pusherL_info.transform.localEulerAngles = new Vector3(0, 0, angle_R);
					pusherR_info.transform.localEulerAngles = new Vector3(0, 0, -angle_L);
				}

				// プッシャー間の距離を取る
				Vector3 Apos = pusherL_info.transform.position;
				Vector3 Bpos = pusherR_info.transform.position;
				float dis = Vector3.Distance(Apos, Bpos);

				// プッシャーの作成最初は果物の外から挿入するようにZ軸移動
				if (pusherR_info.transform.position.z < 2.4f && dis > pusherDistance)
				{

					pusherL_info.transform.position += new Vector3(0, 0, pushSpeed);
					pusherR_info.transform.position += new Vector3(0, 0, pushSpeed);
					fingerRelease = true;
				}
				// 果物に挿入したら状態を更新しながら当たり判定などの処理を行う
				else
				{
					// 距離が一定範囲内で、当たり判定用のオブジェクトまだ存在しない場合、作成する
					if (dis < pusherDistance && pusherR_info.transform.position.z >= 2.4f && !hitPolygon_info.gameObject.activeSelf && fingerRelease == true)
					{
						// 当たり判定用のオブジェクトの座標は二つのプッシャーの真ん中、角度はプッシャーと一致する
						Vector3 c;
						c.x = (pusherL_info.transform.position.x + pusherR_info.transform.position.x) / 2;
						c.y = (pusherL_info.transform.position.y + pusherR_info.transform.position.y) / 2;
						c.z = (pusherL_info.transform.position.z + pusherR_info.transform.position.z) / 2;
						hitPolygon_info.transform.position = new Vector3(c.x, c.y, hitPolygon_info.transform.position.z);
						hitPolygon_info.transform.rotation = pusherL_info.transform.rotation;
						hitPolygon_info.SetActive(true);
						// 当たり判定が一回しか発生しないように、指の状態を強制更新する
						// 指が離れたら次回タッチする時、当たり判定が発生できる
						fingerRelease = false;

					}
					if (hitPolygon_info.gameObject.activeSelf && DestroyFruitNow)
					{
						pusherAction();
					}
				}
			}

		}
		// TOUCH END
		//==================================================================================================================

	}

	void pusherAction()
	{
		hitObjTitle hitObjTitle = hitPolygon_info.GetComponent<hitObjTitle>();
		// 当たり判定オブジェから

		for (int i = 0; i < hitObjTitle.ButtonList.Count; i++)
		{
			//パーティクル生成
			particleObj.GetComponent<ParticleSystem>().maxParticles = 20;
			Instantiate(particleObj, new Vector3(hitObjTitle.ButtonList[i].transform.position.x, hitObjTitle.ButtonList[i].transform.position.y, 0), Quaternion.identity);

			hitObjTitle.ButtonList[i].GetComponent<buttonStatus>().colorBlink = true;
		}
		hitObjTitle.ButtonList.Clear();

		// 当たり判定用のオブジェクトを消去する
		hitPolygon_info.SetActive(false);
		DestroyFruitNow = false;

	}

}

