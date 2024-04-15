using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text gameOverText;
    public Text waveCountText;
    public GameObject restartButton;

    private bool gameOver;
    private int score;
    private int waveCount;
    private float waveBonus = 1.0f;
    private int startHazard = 0;
    private int endHazard = 4;


    private void Start()
    {
        gameOver = false;
        gameOverText.text = "";
        restartButton.SetActive(false);
        waveCountText.text = "";
        waveCount = 1;
        score = 0;
        UpdateWave();
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                if (waveCount > 15 ) { startHazard = 2;  endHazard = 6; }
                GameObject hazard = hazards[Random.Range(startHazard, endHazard)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            waveCount++;
            UpdateWave();

            //BONUS ACUMULATIVO - REFAZER A LOGICA NA GetWaveBonus() e no PlayerController
            //if (waveCount % 5 == 0)
            //{
            //    if (waveBonus < 2.4f)
            //    {
            //        waveBonus = waveBonus + 0.2f;
            //    }
            //}

            //spawn 1 and 2 more asteroids each wave
            if (hazardCount % 2 == 0) {hazardCount++;}
            else { hazardCount = hazardCount + 2;}

            if (gameOver)
            {
                restartButton.SetActive(true);
                break;
            }


        }
    }

    public float GetWaveBonus()
    {

        return waveCount;
    }


    public void UpdateWave()
    {
        waveCountText.text = "Wave: " + waveCount.ToString();
    }

   

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        gameOverText.text = "GAME OVER";
        gameOver = true;
    }

    
}
