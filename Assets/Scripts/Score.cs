using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private int _score;

    private void Start()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
        CellGrid.CherryEaten += AddScore;
    }

    private void AddScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }

    private void OnDisable()
    {
        CellGrid.CherryEaten -= AddScore;
    }
}