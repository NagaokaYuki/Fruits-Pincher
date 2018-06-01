using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class readygo : MonoBehaviour
{
	public GameObject ready;
	public GameObject arrow;
	public GameObject readyInfo;
	public GameObject go;
	public GameObject goInfo;
	public float frameCount;
	public float blinkCount;
	private AudioSource[] countdown;
	bool SEflag = false;

	// Use this for initialization
	void Start()
	{
		frameCount = 0;
		blinkCount = 0;
		countdown = gameObject.GetComponents<AudioSource>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		if (!SEflag && frameCount == 10)
		{
			countdown[0].Play();
			readyInfo = Instantiate(ready, new Vector3(7.0f, 0.0f, 0), Quaternion.identity);
		}
		if (readyInfo != null)
		{
			if (readyInfo.transform.position.x >= 1.7f || readyInfo.transform.position.x <= -1.7f)
			{
				readyInfo.transform.position -= new Vector3(0.4f, 0.0f, 0.0f);
			}
			else
			{
				readyInfo.transform.position -= new Vector3(0.08f, 0.0f, 0.0f);

			}
			if (readyInfo.transform.position.x <= -7)
			{
				// GO音声
				countdown[1].Play();
				DestroyImmediate(readyInfo);
				goInfo = Instantiate(go, new Vector3(0.0f, 0.0f, 0), Quaternion.identity);
				SEflag = true;
				frameCount = 0;
			}
		}

		if (SEflag && frameCount == 40)
		{
			SceneManager.LoadScene("MainScene");
			frameCount = 0;
		}
		frameCount++;
		blinkCount++;

		if (blinkCount % 8 == 0)
		{
			arrow.SetActive(true);
		}
		if (blinkCount % 16 == 0)
		{
			arrow.SetActive(false);
		}
	}
}
