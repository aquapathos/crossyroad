﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	public Text coin = null;  // coin counter
	public Text distance = null;
	public Camera maincamera = null;
	public GameObject guiGameOver = null;

	private int currentCoins = 0;
	private int currentDistance = 0;
	private bool canPlay = false;

	private static Manager s_Instance;
	public static Manager instance
	{
		get 
		{
			if (s_Instance == null) {
				s_Instance = FindObjectOfType (typeof(Manager)) as Manager;
			}
			return s_Instance;
		}
	}

	void Start()
	{
		// TODO: Level generator start up
	}

	public void UpdateCoinCount ( int value ){
		Debug.Log ("Player pcked up another coin for" + value);
		currentCoins += value;

		coin.text = currentCoins.ToString ();
	}

	public void UpdateDistanceCount () {
		Debug.Log ("Player moved forward for");
		currentDistance += 1;
		distance.text = currentDistance.ToString ();

		// TODO: generate new level piece here
	}

	public bool CanPlay(){
		return canPlay;
	}

	public void StartPlay(){
		canPlay = true;
	}

	public void GameOver(){
		maincamera.GetComponent<CameraShake> ().Shake ();
		maincamera.GetComponent<CameraFollow> ().enabled = false;
		GuiGameOver();
	}

	void GuiGameOver(){
		Debug.Log ("Game over!");
		guiGameOver.SetActive (true);
	}

	public void PlayAgain(){
		Debug.Log ("Play Again");
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}

	public void Quit(){
		Debug.Log ("Quit!");
		Application.Quit();
	}
}

