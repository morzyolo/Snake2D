using System.Collections;
using UnityEngine;

public class Score : MonoBehaviour
{
	[SerializeField] private Game _game;
	[SerializeField] private Snake _snake;
	[SerializeField] private ScoreObserver _observer;

	[SerializeField] private ParticleSystem _confettiParticles;

	[SerializeField] private float _showScoreResultLength = 1.5f;

	private PlayerScoreDataHandler _scoreDataHandler;
	private int _score = 0;
	private int _maxScore;

	private void Start()
	{
		_scoreDataHandler = new PlayerScoreDataHandler();
		_maxScore = _scoreDataHandler.MaxScore;

		_observer.SetScore(_score);
		_observer.SetMaxScore(_maxScore);
	}

	private void ShowScore() => _observer.ShowScore();

	private void AddScore()
	{
		_score++;
		_observer.ChangeScore(_score);
	}

	private void ShowResult()
	{
		_observer.ShowResultScore();
		StartCoroutine(CheckScore());
	}

	private IEnumerator CheckScore()
	{
		yield return new WaitForSeconds(_showScoreResultLength);

		if (_score > _maxScore)
		{
			_confettiParticles.Play();
			_observer.SetMaxScore(_score);

			_scoreDataHandler.SaveScore(_maxScore);
		}
	}

	private void OnEnable()
	{
		_game.GameStarted += ShowScore;
		_snake.CherryEaten += AddScore;
		_snake.Hited += ShowResult;
	}

	private void OnDisable()
	{
		_game.GameStarted -= ShowScore;
		_snake.CherryEaten -= AddScore;
		_snake.Hited -= ShowResult;
	}
}
