using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public float GPAvalue;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary" || other.tag == "Enemy") {
			return;
		}
		if (explosion != null) {
			Debug.Log (explosion.name);
			Instantiate (explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player") {
			
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		
		}

		gameController.AddGPA (GPAvalue);
		Destroy (other.gameObject);
		Destroy (gameObject);

	}
}
