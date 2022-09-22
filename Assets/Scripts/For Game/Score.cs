using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private Animator _animator;
    private int _score;

    private void Start()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
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