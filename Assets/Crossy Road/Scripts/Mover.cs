using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed = 1.0f;
	public float moveDirection = 0;
	public bool parentOnTrigger = true;
	public bool hitBoxOnTrigger = false;
	public GameObject moveObject = null;

	private Renderer render = null;
	private bool isVisible = false;


	// Use this for initialization
	void Start () {
		render = moveObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate ( speed * Time.deltaTime, 0, 0);

		IsVisible();
	}

	void IsVisible(){
		if (render.isVisible )
		{
			isVisible = true;
		}

		if ( !render.isVisible && isVisible )
		{
			Debug.Log ("Remove object. No longer seen by camera." );

			Destroy ( this.gameObject );
		}

	}

	void OnTriggerEnter(Collider other ){
		if ( other.tag == "Player" )
		{
			Debug.Log( "Enter.");

			if ( parentOnTrigger )
			{
				Debug.Log( "Enter. Parent to me");
				other.transform.parent = this.transform;
			}

			if ( hitBoxOnTrigger )
			{
				Debug.Log( "Enter. Gothit. Game over");
				other.GetComponent<PlayerController>().GotHit();
			}

		}
	}
	void OnTriggerExit (Collider other ){
		if ( other.tag == "Player" )
		{
			if ( parentOnTrigger )
			{
				Debug.Log( "Exit.");

				other.transform.parent = null;
			}
		}

	}
}
