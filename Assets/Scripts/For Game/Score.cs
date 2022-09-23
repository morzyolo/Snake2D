using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _maxScoreText;

    private Animator _animator;
    private int _score;
    private int _maxScore;

    private void Start()
    {
        _score = 0;
        _maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        _scoreText.text = _score.ToString();
        _maxScoreText.text = $"Max score: {_maxScore}";
        _animator = GetComponent<Animator>();
    }

    private void ShowScore()
    {
        _animator.SetTrigger("ShowText");
    }

    private void AddScore()
    {
        _score++;
        _animator.SetTrigger("Shake");
        _scoreText.text = _score.ToString();
    }

    private void ShowResultScore()
    {
        _animator.SetTrigger("GameOver");
    }

    private void CheckScore()
    {
        if (_score > _maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", _score);
        }
    }

    private void OnEnable()
    {
        CellGrid.CherryEaten += AddScore;
        Game.GameStarted += ShowScore;
        Game.GameOver += ShowResultScore;
    }

    private void OnDisable()
    {
        CellGrid.CherryEaten -= AddScore;
        Game.GameStarted -= ShowScore;
        Game.GameOver -= ShowResultScore;
    }
}