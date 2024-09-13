using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton; 

    public List<GameObject> targetPrefabs;

    private int _score;
    private int _timer;
    private float _spawnRate = 1.5f;
    public bool isGameActive;

    private const float SpaceBetweenSquares = 2.5f; 
    private const float MinValueX = -3.75f; //  x value of the center of the left-most square
    private const float MinValueY = -3.75f; //  y value of the center of the bottom-most square
    
    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficulty)
    {
        _spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        _score = 0;
        _timer = 60;
        UpdateScore(0);
        StartCoroutine(CountDown());
        titleScreen.SetActive(false);
    }

    // While game is active spawn a random target
    private IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            var index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }
    
    // While the Game is active the timer counts down
    private IEnumerator CountDown()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            UpdateCounter();

            if (_timer == 0)
            {
                GameOver();
            }
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    private Vector3 RandomSpawnPosition()
    {
        var spawnPosX = MinValueX + (RandomSquareIndex() * SpaceBetweenSquares);
        var spawnPosY = MinValueY + (RandomSquareIndex() * SpaceBetweenSquares);

        var spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    private int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        scoreText.text = "Score: " + _score;
    }

    // Decreases and updated the counter
    private void UpdateCounter()
    {
        _timer -= 1;
        timerText.text = "Timer: " + _timer;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
