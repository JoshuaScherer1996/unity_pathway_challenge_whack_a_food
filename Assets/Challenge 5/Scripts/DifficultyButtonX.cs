using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonX : MonoBehaviour
{
    private Button _button;
    private GameManagerX _gameManagerX;
    public int difficulty;

    // Start is called before the first frame update
    private void Start()
    {
        _gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty);
    }

    /* When a button is clicked, call the StartGame() method
     * and pass it the difficulty value (1, 2, 3) from the button 
    */
    private void SetDifficulty()
    {
        _gameManagerX.StartGame(difficulty);
    }



}
