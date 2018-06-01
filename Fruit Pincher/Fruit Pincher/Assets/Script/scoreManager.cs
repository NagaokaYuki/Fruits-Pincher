using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
	public GameObject hitPolygon;
	public GameObject slotBox;
	public GameObject numberSingle;
	public GameObject scoreTex;
	public GameObject plus;
	public GameObject minus;
	public Sprite[] texture;
	public Sprite[] plustexture;
	public Sprite[] minustexture;
	public int maxObj;

	public const int MAX_DIGIT = 5;        //最大桁数

	public int score;
	private float scoreOnce;
	private GameObject[] number = new GameObject[MAX_DIGIT];
	// public int scoreType;
	private int cntSame;
	private int cntDiff;
	private Vector3 diffPos;
	private int scoreType;

    public GameObject perfectObj;
    public Sprite[] perfectTex;

    int goodType = 0;

    public GameObject timerPlus;
    public GameObject timerMinus;
    public GameObject timer1;
    public GameObject timer2;
    public Vector3 timerPos;
    public float timerSpace;
	//int fruitType = readygo.GetFruitType();

	// Use this for initialization
	void Start()
	{
		score = 0;
		scoreOnce = 0;
		//scoreType = 0;
		cntSame = 0;
		cntDiff = 0;

		int nNum, nValue;
		nValue = score;
		for (int RankX = 0; RankX < MAX_DIGIT; RankX++)
		{
			nNum = nValue % 10;

			//Debug.Log(RankX + RankY + ":" + nNum);

			nValue /= 10;

			numberSingle.GetComponent<SpriteRenderer>().sprite = texture[nNum];

			Instantiate(slotBox, new Vector3(3.8f - RankX / 2.22f, 6.75f, -7.0f), Quaternion.identity);

			number[RankX] = Instantiate(numberSingle, new Vector3(3.8f - RankX / 2.2f, 6.75f, -6.0f), Quaternion.identity);


		}
		Instantiate(scoreTex, new Vector3(3.37f, 7.45f, 0), Quaternion.identity);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		int nNum, nValue;
		nValue = score;

		for (int RankX = 0; RankX < MAX_DIGIT; RankX++)
		{
			nNum = nValue % 10;

			nValue /= 10;

			number[RankX].GetComponent<SpriteRenderer>().sprite = texture[nNum];

		}
	}

	public void SetType(int fruitType)
	{
		scoreType = fruitType;
	}

	public void GetList(hitObj hitObj)
	{
        // 挟んだ果物の数量を計算
        for (int i = 0; i < hitObj.FruitsList.Count; i++)
        {
            if (hitObj.FruitsList[i].GetComponent<fruitStatus>().m_type == scoreType)
            {
                int cntSameData = cntSame;
                if (cntSameData > 4)
                {
                    cntSameData = 4;
                }
                plus.GetComponent<SpriteRenderer>().sprite = plustexture[cntSameData];
                GameObject temp = Instantiate(plus, new Vector3(hitObj.FruitsList[i].transform.position.x, hitObj.FruitsList[i].transform.position.y, -3), Quaternion.identity);
                temp.GetComponent<SpriteRenderer>().sortingOrder = 141;
                cntSame++;
                //if (cntSame > 4)
                //{
                //    cntSame = 4;
                //}

            }
            else
            {
                cntDiff++;
                if (cntDiff > 4)
                {
                    cntDiff = 4;
                }
                diffPos = new Vector3(hitObj.FruitsList[i].transform.position.x, hitObj.FruitsList[i].transform.position.y, -3);
            }

            if (cntSame == hitObj.FruitsList.Count)
            {
                if (hitObj.FruitsList.Count >= 3)
                {
                    perfectObj.GetComponent<SpriteRenderer>().sprite = perfectTex[3];
                    GameObject temp2 = Instantiate(perfectObj, new Vector3(hitObj.FruitsList[0].transform.position.x, hitObj.FruitsList[0].transform.position.y, -3), Quaternion.identity);
                    temp2.GetComponent<SpriteRenderer>().sortingOrder = 142;
                    temp2.GetComponent<goodKill>().type = 3;
                    goodType = 3;

                    timer.setTime(1 * 60);
                    Instantiate(timerPlus, timerPos, Quaternion.identity);
                    Instantiate(timer1, new Vector3(timerPos.x + timerSpace, timerPos.y, timerPos.z), Quaternion.identity);
                }
                else
                {
                    perfectObj.GetComponent<SpriteRenderer>().sprite = perfectTex[2];
                    GameObject temp2 = Instantiate(perfectObj, new Vector3(hitObj.FruitsList[0].transform.position.x, hitObj.FruitsList[0].transform.position.y, -3), Quaternion.identity);
                    temp2.GetComponent<SpriteRenderer>().sortingOrder = 142;
                    temp2.GetComponent<goodKill>().type = 2;
                    goodType = 2;
                }
            }
        }

        //if (cntSame == hitObj.FruitsList.Count)
        //{
        //    perfectObj.GetComponent<SpriteRenderer>().sprite = perfectTex[2];
        //    Instantiate(perfectObj, new Vector3(hitObj.FruitsList[0].transform.position.x, hitObj.FruitsList[0].transform.position.y, -3), Quaternion.identity);
        //}
        //else if (cntSame == 0)
        //{
        //    perfectObj.GetComponent<SpriteRenderer>().sprite = perfectTex[0];
        //    Instantiate(perfectObj, new Vector3(hitObj.FruitsList[0].transform.position.x, hitObj.FruitsList[0].transform.position.y, -3), Quaternion.identity);
        //}
        //else
        //{
        //    perfectObj.GetComponent<SpriteRenderer>().sprite = perfectTex[1];
        //    Instantiate(perfectObj, new Vector3(hitObj.FruitsList[0].transform.position.x, hitObj.FruitsList[0].transform.position.y, -3), Quaternion.identity);
        //}

        if (cntSame == 0)
        {
            minus.GetComponent<SpriteRenderer>().sprite = minustexture[0];
            GameObject temp = Instantiate(minus, diffPos, Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingOrder = 140;

            perfectObj.GetComponent<SpriteRenderer>().sprite = perfectTex[0];
            GameObject temp2 = Instantiate(perfectObj, diffPos, Quaternion.identity);
            temp2.GetComponent<SpriteRenderer>().sortingOrder = 142;
            temp2.GetComponent<goodKill>().type = 0;
            goodType = 0;

            timer.setTime(-2 * 60);
            Instantiate(timerMinus, timerPos, Quaternion.identity);
            Instantiate(timer2, new Vector3(timerPos.x + timerSpace, timerPos.y, timerPos.z), Quaternion.identity);

        }
        else if (cntSame != hitObj.FruitsList.Count)
        {
            minus.GetComponent<SpriteRenderer>().sprite = minustexture[cntDiff];
            GameObject temp = Instantiate(minus, diffPos, Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingOrder = 140;

            float percent = (float)cntSame / (float)hitObj.FruitsList.Count * 100;

            //Debug.Log(cntSame + "/" + hitObj.FruitsList.Count + "=" + percent);
            if ((int)percent >= 1 && (int)percent <= 50)
            {
                perfectObj.GetComponent<SpriteRenderer>().sprite = perfectTex[1];
                GameObject temp2 = Instantiate(perfectObj, diffPos, Quaternion.identity);
                temp2.GetComponent<SpriteRenderer>().sortingOrder = 142;
                temp2.GetComponent<goodKill>().type = 1;
                goodType = 1;
            }
            else if ((int)percent >= 51 && (int)percent <= 99)
            {
                perfectObj.GetComponent<SpriteRenderer>().sprite = perfectTex[2];
                GameObject temp2 = Instantiate(perfectObj, diffPos, Quaternion.identity);
                temp2.GetComponent<SpriteRenderer>().sortingOrder = 142;
                temp2.GetComponent<goodKill>().type = 2;
                goodType = 2;
            }
        }


		// 得点を計算
		for (int i = 1; i < cntSame + 1; i++)
		{
			scoreOnce += i * 10;
		}

		scoreOnce *= (1.0f - ((float)cntDiff * 0.1f));
		//Debug.Log("->" + scoreOnce);

		score += (int)scoreOnce;

		//スコア設定
		result.SetScore(score);

		// 状態をリセット
		scoreOnce = 0;
		cntDiff = 0;
		cntSame = 0;

        juiceManager.setPoint(hitObj.FruitsList.Count);

        //GameObject target = GameObject.Find("fruitTag(Clone)");
        GameObject target = GameObject.Find("targetObj(Clone)");

        targetData targetDataCS = target.GetComponent<targetData>();

        for (int i = 0; i < hitObj.FruitsList.Count; i++)
        {
            targetDataCS.setJuiceType(hitObj.FruitsList[i].GetComponent<fruitStatus>().m_type);
        }
		// // ボーナス得点計算
		// // 通常得点計算
		// if (!calcScoreNormal)
		// {
		// 	int[] fruitResult = new int[5];
		// 	for (int i = 0; i < hitObj.FruitsList.Count; i++)
		// 	{
		// 		fruitResult[hitObj.FruitsList[i].GetComponent<fruitStatus>().m_type]++;

		// 	}
		// 	Debug.Log(fruitResult[0] + "||" + fruitResult[1] + "||" + fruitResult[2] + "||" + fruitResult[3] + "||" + fruitResult[4]);
		// }

	}

	public int getTargetList(int num)
	{
		return scoreType;
	}

    public int getParticleType()
    {
        return goodType;
    }

}
