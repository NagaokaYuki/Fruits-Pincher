using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// プッシャーのマネジャークラス
public class pushManager : MonoBehaviour
{
	public bool DestroyFruitNow = false;
	// 左右のプッシャーオブジェクト
	public GameObject pusherR;
	public GameObject pusherL;
	public GameObject scoreManager;
	public float pusherDistance;
	// プッシャー情報の容器
	private GameObject pusherR_info;
	private GameObject pusherL_info;

	// SE
	private AudioSource[] sources;

	// 当たり判定用のオブジェクト
	public GameObject hitPolygon;
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
	public GameObject[] particleObj = new GameObject[5];

	// タッチ・マウス操作
	public bool touchPlay = true;

	public int particleBad;
	public int particleOk;
	public int particleGood;
	public int particlePerfect;
	private int fingerCnt = 0;


	public GameObject touchA;
	public GameObject touchB;
	public int distance;
	public float moveSpeed;
	private Vector3 oldA;
	private Vector3 oldB;
	public int start;
	public bool fingerPlay = false;
	private int cntFrameFinger = 0;
	bool OneFingerPress = false;
	bool sound;
	bool game = true;
	// Use this for initialization
	void Start()
	{
		oldA = touchA.GetComponent<Transform>().position;
		oldB = touchB.GetComponent<Transform>().position;
		fingerRelease = true;
		hitPolygon_info = Instantiate(hitPolygon) as GameObject;
		hitPolygon_info.GetComponent<hitObj>().pm = this;
		hitPolygon_info.SetActive(false);
		//AudioSourceコンポーネントを取得し、変数に格納
		sources = gameObject.GetComponents<AudioSource>();
		sources[0].Play();          // BGM
		sound = false;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//==================================================================================================================
		// MOUSE DEBUG

		if (!touchPlay && game)
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
				hitObj hitObj = hitPolygon_info.GetComponent<hitObj>();
				scoreManager.GetComponent<scoreManager>().GetList(hitObj);
				// 当たり判定オブジェから

				int goodType = scoreManager.GetComponent<scoreManager>().getParticleType();


				for (int i = 0; i < hitObj.FruitsList.Count; i++)
				{
					//パーティクルの色指定
					int particleColor = hitObj.FruitsList[i].GetComponent<fruitStatus>().m_type;

					if (goodType == 0)
					{
						particleObj[particleColor].GetComponent<ParticleSystem>().maxParticles = particleBad;
						sources[1].Stop();  // 移動SE
						sources[3].Play();          // バッドSE
					}
					else if (goodType == 1)
					{
						particleObj[particleColor].GetComponent<ParticleSystem>().maxParticles = particleOk;
					}
					else if (goodType == 2)
					{
						particleObj[particleColor].GetComponent<ParticleSystem>().maxParticles = particleGood;
					}
					else if (goodType == 3)
					{
						particleObj[particleColor].GetComponent<ParticleSystem>().maxParticles = particlePerfect;
						sources[1].Stop();  // 移動SE
						sources[4].Play();     // パーフェクトSE
					}

					//パーティクル生成
					Instantiate(particleObj[particleColor], new Vector3(hitObj.FruitsList[i].transform.position.x, hitObj.FruitsList[i].transform.position.y, -2), Quaternion.identity);
					DestroyImmediate(hitObj.FruitsList[i]);
					sound = true;
				}
				if (sound == true)
				{
					// 果物の消去音声
					sources[2].Play();
				}
				//===============
				hitObj.FruitsList.Clear();

				// 当たり判定用のオブジェクトを消去する
				hitPolygon_info.SetActive(false);
				DestroyFruitNow = false;
			}
		}

		// MOUSE DEBUG END
		//==================================================================================================================


		//==================================================================================================================
		// TOUCH

		else if(game)
		{
			// 指のタッチ数取得
			int touchCount = Input.touches.Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
			if (touchCount == 1 && OneFingerPress == false)
			{
				fingerCnt++;
				OneFingerPress = true;
			}
			else
			{
				OneFingerPress = false;
			}
			if(fingerCnt >= 8 && fingerPlay == false)
			{
				fingerPlay = true;
				fingerCnt = 0;
			}

			// 二本の指でタッチし、プッシャーまだ作成されてない場合
			if (touchCount >= 2 && pusherR_info == null && pusherL_info == null)
			{
				if (fingerPlay == false)
				{
					fingerCnt = 0;
				}
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

				// 移動音
				sources[1].Play();  // 移動SE
									//===============

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
						hitPolygon_info.transform.position = new Vector3(c.x, c.y, c.z);
						hitPolygon_info.transform.rotation = pusherL_info.transform.rotation;
						hitPolygon_info.SetActive(true);
						// 当たり判定が一回しか発生しないように、指の状態を強制更新する
						// 指が離れたら次回タッチする時、当たり判定が発生できる
						fingerRelease = false;

					}
					if (hitPolygon_info.gameObject.activeSelf && DestroyFruitNow)
					{
						hitObj hitObj = hitPolygon_info.GetComponent<hitObj>();
						scoreManager.GetComponent<scoreManager>().GetList(hitObj);
						// 当たり判定オブジェから

						int goodType = scoreManager.GetComponent<scoreManager>().getParticleType();


						for (int i = 0; i < hitObj.FruitsList.Count; i++)
						{
							//パーティクルの色指定
							int particleColor = hitObj.FruitsList[i].GetComponent<fruitStatus>().m_type;

							if (goodType == 0)
							{
								particleObj[particleColor].GetComponent<ParticleSystem>().maxParticles = particleBad;
						sources[1].Stop();  // 移動SE
								sources[3].Play();      // バッドSE
							}
							else if (goodType == 1)
							{
								particleObj[particleColor].GetComponent<ParticleSystem>().maxParticles = particleOk;
							}
							else if (goodType == 2)
							{
								particleObj[particleColor].GetComponent<ParticleSystem>().maxParticles = particleGood;
							}
							else if (goodType == 3)
							{
								particleObj[particleColor].GetComponent<ParticleSystem>().maxParticles = particlePerfect;
						sources[1].Stop();  // 移動SE
								sources[4].Play();      // パーフェクトSE
							}

							//パーティクル生成
							Instantiate(particleObj[particleColor], new Vector3(hitObj.FruitsList[i].transform.position.x, hitObj.FruitsList[i].transform.position.y, -2), Quaternion.identity);
							DestroyImmediate(hitObj.FruitsList[i]);

							sound = true;
						}

						if (sound == true)
						{
							// 果物の消去音声
						sources[1].Stop();  // 移動SE
							sources[2].Play();
						}
						//===============
						hitObj.FruitsList.Clear();

						// 当たり判定用のオブジェクトを消去する
						hitPolygon_info.SetActive(false);
						DestroyFruitNow = false;
					}
				}
			}

		}
		// TOUCH END
		//==================================================================================================================

		if (fingerPlay)
		{
			if (cntFrameFinger == 0)
			{
				touchA.SetActive(true);
				touchB.SetActive(true);
			}

			if (cntFrameFinger >= start)
			{
				touchA.GetComponent<Transform>().position -= new Vector3(0, moveSpeed, 0);
				touchB.GetComponent<Transform>().position += new Vector3(0, moveSpeed, 0);
			}

			if (cntFrameFinger >= distance + start)
			{
				touchA.SetActive(false);
				touchB.SetActive(false);
				touchA.GetComponent<Transform>().position = oldA;
				touchB.GetComponent<Transform>().position = oldB;
				cntFrameFinger = 0;
				fingerPlay = false;
			}
			else
			{
				cntFrameFinger++;

			}
		}

	}

	public void gameFinish()
	{
		game = false;
	}

}
