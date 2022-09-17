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

    private void AddScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
        _animator.SetTrigger("Shake");
    }

    private void OnEnable()
    {
        CellGrid.CherryEaten += AddScore;
    }

    private void OnDisable()
    {
        CellGrid.CherryEaten -= AddScore;
    }
}