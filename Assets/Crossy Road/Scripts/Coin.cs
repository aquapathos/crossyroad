using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public int coinValue = 1;

	void OnTriggerEnter ( Collider other )
	{
		if (other.tag == "Player") {
			Debug.Log ("Player picked up a coin!");

			// TO DO: Manager -> update coin count 

			Destroy (this.gameObject);
		}
	}
}
