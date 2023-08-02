using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ScoreObserver : MonoBehaviour
{
	[Header("Texts")]
	[SerializeField] private Text _scoreText;
	[SerializeField] private Text _maxScoreText;

	[Header("Strings")]
	[SerializeField] private string _showTriggerName = "Show";
	[SerializeField] private string _shakeTriggerName = "Shake";
	[SerializeField] private string _gameOverTriggerName = "GameOver";

	private Animator _animator;

	private void Start() => _animator = GetComponent<Animator>();

	public void SetScore(int score) => _scoreText.text = score.ToString();

	public void SetMaxScore(int maxScore) => _maxScoreText.text = maxScore.ToString();

	public void ShowScore() => _animator.SetTrigger(_showTriggerName);

	public void ChangeScore(int score)
	{
		_animator.SetTrigger(_shakeTriggerName);
		SetScore(score);
	}

	public void ShowResultScore() => _animator.SetTrigger(_gameOverTriggerName);
}