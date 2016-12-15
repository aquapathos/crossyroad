using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public bool autoMove = false;
	public GameObject player = null;
	public float speed = 200.0f;
	public Vector3 offset = new Vector3 ( 1.0f, 3.9f, 0.6f );  // カメラの初期位置に合わせる 
	public Vector3 pos = new Vector3 (0, 0, 0);
	public Vector3 depth = new Vector3 (0,0,0);

	void Update()
	{
		if (!Manager.instance.CanPlay() ) return;

		if (autoMove) {
			depth += new Vector3 (0, 0, speed * Time.deltaTime );
			pos = Vector3.Lerp (gameObject.transform.position, player.transform.position + offset, Time.deltaTime);
			gameObject.transform.position = new Vector3 (pos.x, offset.y, depth.z);
		} else {
			pos = Vector3.Lerp (gameObject.transform.position, player.transform.position + offset, Time.deltaTime);
			gameObject.transform.position = new Vector3 (pos.x, offset.y, pos.z);
		}
	}
}
