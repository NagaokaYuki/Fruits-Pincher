using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	// 画面の比率の制御
	// MainCanvasと連動

public class cameraSize : MonoBehaviour
{
	// MainCanvasのオブジェクトを入れる
	public GameObject canvas;

	// 計算用変数
	private float width;
	private float height;
	private Camera cam;

	void Start()
	{
		// カメラは一つしか存在しないこと
		cam = GetComponent<Camera>();
		// 取得したカメラによる画面領域を算出
		Rect rect = canvas.gameObject.GetComponent<RectTransform>().rect;
		width = rect.width;
		height = rect.height;
	}

	void Update()
	{
		// 画面比率を固定する
		float aspect = (float)Screen.height / (float)Screen.width;
		float bgAcpect = height / width;

		// カメラの視野サイズを設定
		cam.orthographicSize = height / 2f;
		
		// 画面比率の計算
		if (bgAcpect > aspect)
		{
			// 倍率
			float bgScale = height / Screen.height;
			// viewport rectの幅
			float camWidth = width / (Screen.width * bgScale);
			// viewportRectを設定
			cam.rect = new Rect((1f - camWidth) / 2f, 0f, camWidth, 1f);
		}
		else
		{
			// 倍率
			float bgScale = width / Screen.width;
			// viewport rectの幅
			float camHeight = height / (Screen.height * bgScale);
			// viewportRectを設定
			cam.rect = new Rect(0f, (1f - camHeight) / 2f, 1f, camHeight);
		}

	}
}
