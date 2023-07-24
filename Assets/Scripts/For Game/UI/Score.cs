using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Score : MonoBehaviour
{
	[SerializeField] private Game _game;
	[SerializeField] private Snake _snake;

	[Header("Texts")]
	[SerializeField] private Text _scoreText;
	[SerializeField] private Text _maxScoreText;

	[Header("Strings")]
	[SerializeField] private string _maxScoreName = "MaxScore";
	[SerializeField] private string _showTriggerName = "Show";
	[SerializeField] private string _shakeTriggerName = "Shake";
	[SerializeField] private string _gameOverTriggerName = "GameOver";

	private Animator _animator;
	private int _score = 0;
	private int _maxScore;

	private void Start()
	{
		_maxScore = PlayerPrefs.GetInt(_maxScoreName, 0);
		_scoreText.text = _score.ToString();
		_maxScoreText.text = $"Max score: {_maxScore}";
		_animator = GetComponent<Animator>();
	}

	public void ShowScore()
	{
		_animator.SetTrigger(_showTriggerName);
	}

	private void AddScore()
	{
		_score++;
		_animator.SetTrigger(_shakeTriggerName);
		_scoreText.text = _score.ToString();
	}

	public void ShowResultScore()
	{
		_animator.SetTrigger(_gameOverTriggerName);
		CheckScore();
	}

	private void CheckScore()
	{
		if (_score > _maxScore)
			PlayerPrefs.SetInt(_maxScoreName, _score);
	}

	private void OnEnable() => _snake.CherryEaten += AddScore;

	private void OnDisable() => _snake.CherryEaten -= AddScore;
}