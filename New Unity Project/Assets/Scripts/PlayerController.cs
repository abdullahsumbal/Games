using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerController: MonoBehaviour {

	public float speed;

	private Rigidbody rb;
	private int count;
	public Text countText;
	public Text winText;
	public Texture2D image1;
	public Texture2D image2;
	public Texture2D image3;
	public Texture2D image4;
	public Texture2D image5;
	public Texture2D image6;
	private Rect windowRect = new Rect (20, 20, 400, 300);
	private bool paused;
	private float delay = 5.0F;
	private float triggerTime = 0.0F;
	private bool final = false;
	private bool finalGift = false;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		count = 0;
		setCountText ();
		winText.text = "";

		paused = false;


	}		

	void FixedUpdate () {
		if (!paused) {
			float moveHoritzontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (moveHoritzontal, 0.0f, moveVertical);

			rb.AddForce (movement * speed);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Pick Up")) {
			
			other.gameObject.SetActive (false);

			count = count + 1;
			triggerTime = Time.time;
			setCountText ();

		}
	}

	void setCountText(){
		countText.text = "Count: " + count.ToString ();
		if (count == 0) {
			triggerTime = Time.time;
			#if UNITY_EDITOR
			//EditorUtility.DisplayDialog("Please collect all the cube!"," to find your what your gift is","continue");
			#endif
		}

		if (count == 5 ) {
			#if UNITY_EDITOR
			//EditorUtility.DisplayDialog("So you collected all the cubes","You ready for your gift","Yes");
			#endif
			triggerTime = Time.time;
			final = true;
		}
	}

	void OnGUI(){
		if (count == 0&& triggerTime + delay > Time.time) {
			GUILayout.Window(0,windowRect,DO,"Please collect all the cube! to find your what your gift is");
		}

		if (count == 1 && triggerTime + delay > Time.time) {
			//GUILayout.Window(0,windowRect,DO,"So you collected all the cubes .You ready for your gift?");
			GUILayout.Window(0,windowRect,DO,image1);
		}
		if (count == 2 && triggerTime + delay > Time.time) {
			GUILayout.Window(0,windowRect,DO,image2);
		}
		if (count == 3 && triggerTime + delay > Time.time) {
			GUILayout.Window(0,windowRect,DO,image3);
		}
		if (count == 4 && triggerTime + delay > Time.time) {
			GUILayout.Window(0,windowRect,DO,image4);
		}
		if (count == 5 && triggerTime + delay > Time.time) {
			GUILayout.Window(0,windowRect,DO,"So you collected all the cubes .You ready for your gift?");
		}

		if (final && triggerTime + 15.0F > Time.time && triggerTime + 8.0F < Time.time) {
			GUILayout.Window(0,windowRect,DO,image5);
			finalGift = true;
		}
		if (finalGift && triggerTime + 15.0F < Time.time) {
			GUILayout.Window(0,windowRect,DO,image6);
		}
	}
	void DO(int windowID){
		GUILayout.Space (0);
	}

	public void onPauseGame(){
		paused = true;
	}
	public void OnResumeGame(){
		paused = false;
	}
}
