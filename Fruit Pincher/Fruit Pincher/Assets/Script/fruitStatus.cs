using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitStatus : MonoBehaviour
{

	// 果物とプッシャーが当たり判定発生したら
	// このスクリプトのgetCollision()が自動的に呼び出される

	// 当たり判定フラグ
	public bool m_collision;
	// 果物の種類
	public int m_type;

	void FixedUpdate()
	{
		if ( this.transform.position.x < -4.0f || this.transform.position.x > 4.0f || this.transform.position.y < -9.0f || this.transform.position.y > 10.0f )
		{
			DestroyImmediate(this.gameObject);
		}
	}
	
}
