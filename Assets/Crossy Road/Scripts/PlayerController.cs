﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float moveDistance = 1;
	public float moveTime = 0.4f;
	public float colliderDistCheck = 1; // この近さだと衝突したと判定する距離
	public bool isIdle = true;
	public bool isDead = false;
	public bool isMoving = false;
	public bool isJumping = false;
	public bool jumpStart = false;
	public ParticleSystem particle = null;
	public GameObject chicken = null;
	private Renderer renderer = null;
	private bool isVisible = false;

	void Start ()
	{
		renderer = chicken.GetComponent<Renderer>();
	}

	void Update ()
	{
		if (!Manager.instance.CanPlay ())			return;

		if ( isDead ) return;
		CanIdle ();
		CanMove ();
		IsVisible ();
	}

	void CanIdle ()
	{
		if (isIdle) {
			if ( Input.GetKeyDown (KeyCode.UpArrow)   ||
				 Input.GetKeyDown (KeyCode.DownArrow) ||
				 Input.GetKeyDown (KeyCode.LeftArrow) ||
				 Input.GetKeyDown (KeyCode.RightArrow) )
			{
				CheckIfCanMove ();
			}
		}
	}

	void CheckIfCanMove ()
	{

		RaycastHit hit;
		Physics.Raycast (this.transform.position, chicken.transform.forward, out hit, colliderDistCheck );

		Debug.DrawRay ( this.transform.position, chicken.transform.forward * colliderDistCheck, Color.red, 2 );

		if ( hit.collider == null )
		{
			SetMove();
		}
		else
		{
			if (hit.collider.tag == "collider" )
			{
				Debug.DrawRay ( this.transform.position, chicken.transform.forward * colliderDistCheck, Color.blue, 2 );

				Debug.Log ( "Hit something with collider tag. ");

				GotHit();
			}
			else
			{
				SetMove();
			}
		}
	}

	void SetMove ()
	{
		// Debug.Log ( "Hit nothing. Keep moving." );

		isIdle = false;
		isMoving = true;
		jumpStart = true;
	}

	void CanMove ()
	{
		if (isMoving) {
			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				Moving (new Vector3 (transform.position.x, transform.position.y, transform.position.z + moveDistance));
				SetMoveForwardState();
			}
			else if (Input.GetKeyUp (KeyCode.DownArrow)) {
				Moving (new Vector3 (transform.position.x, transform.position.y, transform.position.z - moveDistance));
			}
			else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				Moving (new Vector3 (transform.position.x - moveDistance, transform.position.y, transform.position.z));
			}
			else if (Input.GetKeyUp (KeyCode.RightArrow)) {
				Moving (new Vector3 (transform.position.x + moveDistance, transform.position.y, transform.position.z));
			}
		}

	}

	void Moving (Vector3 pos)
	{
		isIdle = false;
		isMoving = false;
		isJumping = true;
		jumpStart = false;

		LeanTween.move (this.gameObject, pos, moveTime).setOnComplete( MoveComplete );
	}

	void MoveComplete ()
	{
		isJumping = false;
		isIdle = true;
	}

	void SetMoveForwardState ()
	{
		Manager.instance.UpdateDistanceCount ();
	}

	void IsVisible ()
	{

		if ( renderer.isVisible )
		{
			isVisible = true;
		}

		if ( !renderer.isVisible && isVisible )
		{
			Debug.Log ("Player off screen. Apply GotHit()" );

			GotHit ();

		}
	}

	public void GotHit ()
	{
		isDead = true;
		ParticleSystem.EmissionModule em = particle.emission;
		em.enabled = true;

		Manager.instance.GameOver ();
	}

}
