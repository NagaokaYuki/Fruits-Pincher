using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleMoveKill : MonoBehaviour {

    int count;

    public Sprite[] texture;
    public GameObject fruit;
    GameObject fruitData;

    public int type;

    public float moveSpeed;

    public Vector3 pos;
    public Vector3 size;

	// Use this for initialization
	void Start () {
        fruitData = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (fruitData == null)
        {
            fruit.GetComponent<SpriteRenderer>().sprite = texture[type];

            fruitData = Instantiate(fruit, new Vector3(this.transform.position.x + pos.x, this.transform.position.y + pos.y, this.transform.position.z + pos.z), Quaternion.identity);
            fruitData.transform.localScale = new Vector3(this.transform.localScale.x * size.x, this.transform.localScale.y * size.y, this.transform.localScale.z * size.z);
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + moveSpeed, this.transform.position.z);
        fruitData.transform.position = new Vector3(this.transform.position.x, fruitData.transform.position.y + moveSpeed, this.transform.position.z);

        if (count >= 25)
        {
            DestroyImmediate(this.gameObject);
            Destroy(fruitData);
            fruitData = null;
        }
        count++;
	}
}
