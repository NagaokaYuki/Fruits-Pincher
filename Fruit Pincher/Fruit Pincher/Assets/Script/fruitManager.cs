using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 果物のマネジャークラス
public class fruitManager : MonoBehaviour
{
	// 果物最大数
	const int FruitMAX = 58;
	// 果物オブジェ対象
	public Sprite[] texture;
	public Mesh[] mesh;
	public GameObject fruitObj;
	public GameObject scoreManager;
	// 果物リスト
	private GameObject[] ObjList = new GameObject[FruitMAX];

	public int EmptyMake;

	public int fruitX;      //指定された種類
	public int fruitY;      //ほかの種類
	private int cntTime = 0;

	int[] fruitPattern = {
						0 , 1 , 2 , 3 , 4 ,     //目標物が[0]の場合のパターン
                        1 , 2 , 3 , 4 , 0 ,     //目標物が[1]の場合のパターン
                        2 , 3 , 4 , 0 , 1 ,     //目標物が[2]の場合のパターン
                        3 , 4 , 0 , 1 , 2 ,     //目標物が[3]の場合のパターン
                        4 , 0 , 1 , 2 , 3       //目標物が[4]の場合のパターン
                         };

	// 果物のテクスチャ
	void Start()
	{
	}
	void Update()
	{
		cntTime++;
		if (cntTime % (10 * 60) == 0)
		{
			fruitX -= 1;
			if(fruitX < 2)
			{
				fruitX = 2;
			}
		}
		if (cntTime % (20 * 60) == 0)
		{
			fruitY -= 1;
			if(fruitY < 1)
			{
				fruitY = 1;
			}
		}
		int fruitType = juiceManager.getType();
		int empty = 0;
		for (int nCnt = 0; nCnt < FruitMAX; nCnt++)
		{
			if (ObjList[nCnt] == null)
			{
				empty++;
			}
			else
			{
				// 全オブジェの回転とZ軸位置を固定する
				ObjList[nCnt].transform.position = new Vector3(ObjList[nCnt].transform.position.x, ObjList[nCnt].transform.position.y, 3);
				ObjList[nCnt].transform.rotation = new Quaternion(0, 0, ObjList[nCnt].transform.rotation.z, ObjList[nCnt].transform.rotation.w);
				ObjList[nCnt].GetComponent<SpriteRenderer>().sortingOrder = -nCnt;
			}
		}
		if (empty >= EmptyMake)
		{
			int randamMax = fruitX + fruitY;
			int randomSame = Random.Range(0, randamMax);

			for (int q = 0; q < EmptyMake; q++)
			{
				for (int nCnt = 0; nCnt < FruitMAX; nCnt++)
				{
					if (randomSame < fruitX)
					{
						// リスト中の対象を全部参照し、nullの場合生成する
						if (ObjList[nCnt] == null)
						{

							GameObject ObjData = Instantiate(fruitObj, new Vector3(0.0f, 9.0f, 0.1f), Quaternion.identity);

							//int randomType = Random.Range(0, 10);        //確率を決める
							//if (randomType >= 2)        //90%の確率
							{
								// オブジェの種類を決める
								ObjList[nCnt] = ObjData;
								ObjList[nCnt].GetComponent<SpriteRenderer>().sprite = texture[fruitPattern[(fruitType * 5)]];
								ObjList[nCnt].GetComponent<MeshCollider>().sharedMesh = mesh[fruitPattern[(fruitType * 5)]];
								ObjList[nCnt].GetComponent<fruitStatus>().m_type = fruitPattern[(fruitType * 5)];
								break;
							}
						}
					}
					else
					{
						// リスト中の対象を全部参照し、nullの場合生成する
						if (ObjList[nCnt] == null)
						{
							int randomdiff = Random.Range(0, 4);
							GameObject ObjData = Instantiate(fruitObj, new Vector3(0.0f, 9.0f, 0.1f), Quaternion.identity);

							//int randomType = Random.Range(0, 10);        //確率を決める
							//if (randomType >= 2)        //90%の確率
							{
								// オブジェの種類を決める
								ObjList[nCnt] = ObjData;
								ObjList[nCnt].GetComponent<SpriteRenderer>().sprite = texture[fruitPattern[(fruitType * 5) + randomdiff + 1]];
								ObjList[nCnt].GetComponent<MeshCollider>().sharedMesh = mesh[fruitPattern[(fruitType * 5) + randomdiff + 1]];
								ObjList[nCnt].GetComponent<fruitStatus>().m_type = fruitPattern[(fruitType * 5) + randomdiff + 1];
								break;
							}
						}
					}
				}
			}
		}
	}
}
