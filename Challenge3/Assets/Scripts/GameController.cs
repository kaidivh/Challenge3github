using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float waveWait;
	public float spawnWait;
	public float startWait;
	
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	
	private int score = 0;
	
	private bool gameOver;
	private bool restart;
	
	
	void Start()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		UpdateScore();
		StartCoroutine( SpawnWaves());
	}
	
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
		
		if (restart){
			if (Input.GetKeyDown(KeyCode.Y)){
				SceneManager.LoadScene("SpaceShooter");
			}
		}
		
	}
	
	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds(startWait);
		while (true){
			for (int i = 0; i < hazardCount; i++){
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			
			if (gameOver)
			{
				restartText.text = "Press 'Y' for Restart!";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}
	
	void UpdateScore()
	{
		scoreText.text = "Points: " + score;
		if (score >= 100){
			GameOver();
		}
	}
	
	public void GameOver(){
		if (score < 100){
			gameOverText.text = "Game Over! :-( Game by Collin Conner!";
		}
		else{
			gameOverText.text = "You win! Game by Collin Conner!";
		}
		gameOver = true;
	}
}
