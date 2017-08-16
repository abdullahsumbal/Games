using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

	public float speed;
	public Boundary boundary;
	public float side_tilt;
	public float front_tilt;

	public GameObject shot;
	public Transform shotSpwan;
	private float nextFire;
	public float fireRate;

	void FixedUpdate(){


		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody> ().position = new Vector3 
		(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x,boundary.xMin,boundary.xMax),
			0.0f, 
			Mathf.Clamp(GetComponent<Rigidbody>().position.z,boundary.zMin,boundary.zMax)
		);

		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (GetComponent<Rigidbody>().velocity.z * front_tilt,0.0f,GetComponent<Rigidbody>().velocity.x * -side_tilt);
	}

	void Update(){
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			//GameObject clone = 
			Instantiate (shot, shotSpwan.position, shotSpwan.rotation);//  as GameObject;
		}
	}
}
