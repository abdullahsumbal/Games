using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {


	public GameObject[] hazards ;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText GPAText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText startInfo;
	public GUIText endInfo;

	private bool gameOver;
	private bool restart;
	private float GPA;

	IEnumerator SpawnWaves(){
		
		startInfo.text = "This game is dedicated to Mini Cheon \n and all the architecture students \n at McGill University";
		yield return new WaitForSeconds (6);
		startInfo.text = "Keep Working Hard";
		yield return new WaitForSeconds (2);
		startInfo.text = "Due to limited time, I could not make this game \n more lame";
		yield return new WaitForSeconds (3);
		startInfo.text = "As an Architecture Student you must travel \n through space and time ...";
		yield return new WaitForSeconds (3);
		startInfo.text = "This space and time is called a semester...";
		yield return new WaitForSeconds (2);
		startInfo.text = "navigate dangerous terrain and dark enemies \n known as professers, assignments and projects";
		yield return new WaitForSeconds (3);
		startInfo.text = "where evil knows no bounds.";
		yield return new WaitForSeconds (2);
		startInfo.text = "ohh! one more thing \n play in full screen mode please \n For controls, shot with spacebar \n and move with arrow keys ";
		yield return new WaitForSeconds (5);
		startInfo.text = "";		

		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int count = 0; count < hazardCount; count++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				int num = Random.Range (0, 2);
				if (num == 0) {
					restartText.text = " Ready for the Next semester, Press 'N'";
				}
				if (num == 1) {
					restartText.text = " You got this, Press 'N'";
				}
				if (num == 2) {
					restartText.text = " Good luck for Next semester, Press 'N'";
				}
				restart = true;
				break;
			}
		}
	}
	// Use this for initialization
	void Start () {
		gameOver = false;
		restart = false;
		restartText.text= "";
		gameOverText.text = "";
		GPA = 0.0f;
		updateGPA ();
		StartCoroutine(SpawnWaves ());


	}


	public void AddGPA(float newGPAValue){
		GPA += newGPAValue;
		updateGPA ();
	}
	void updateGPA(){
		GPAText.text  = "GPA : " + GPA;
	}

	public float getGPA(){
		return GPA;
	}


	public void GameOver(){
		if (GPA >= 2.0) {
			gameOverText.text = "You FUCKING survived the semester";
		} else {
			gameOverText.text = "You may have lost the battle\n but the war is not over \n try again!";
		}
		endInfo.text = "Created by Abdullah Sumbal";
		gameOver = true;
	}

	void Update(){

		if (GPA >= 4.0) {
			GameOver ();
		
		}
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.N))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

}
