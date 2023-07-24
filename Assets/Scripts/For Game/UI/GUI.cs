using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GUI : MonoBehaviour
{
	public event Action GameStarted;
	public event Action QuitButtonClicked;
	public event Action RestartButtonClicked;

	[SerializeField] private Score _score;

	[Header("UI elements")]
	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _restartButton;
	[SerializeField] private Button _quitButton;
	[SerializeField] private Text _continueText;

	[Header("Strings")]
	[SerializeField] private string _appearTextTriggerName = "AppearText";
	[SerializeField] private string _disappearTextTriggerName = "DisappearText";
	[SerializeField] private string _appearButtonTriggerName = "AppearButtons";
	[SerializeField] private string _losingText = "Click to continue";

	private Animator _animator;

	private void Start() => _animator = GetComponent<Animator>();

	public void ShowStartScreen()
	{
		_restartButton.gameObject.SetActive(false);
		_quitButton.gameObject.SetActive(false);
		_continueButton.onClick.AddListener(StartGame);
	}

	public void StartGame()
	{
		_continueButton.onClick.RemoveListener(StartGame);
		_animator.SetTrigger(_disappearTextTriggerName);
		_score.ShowScore();
		GameStarted?.Invoke();
	}

	public void ShowLosing()
	{
		_continueButton.onClick.AddListener(ShowOptions);
		_continueText.text = _losingText;
		_animator.SetTrigger(_appearTextTriggerName);
		_score.ShowResultScore();
	}

	public void ShowOptions()
	{
		_continueButton.onClick.RemoveListener(ShowOptions);
		_animator.SetTrigger(_disappearTextTriggerName);
		_animator.SetTrigger(_appearButtonTriggerName);
		_restartButton.gameObject.SetActive(true);
		_restartButton.onClick.AddListener(RestartGame);
		_quitButton.gameObject.SetActive(true);
		_quitButton.onClick.AddListener(QuitToMenu);
	}

	private void RemoveListeners()
	{
		_restartButton.onClick.RemoveListener(RestartGame);
		_quitButton.onClick.RemoveListener(QuitToMenu);
	}

	private void RestartGame()
	{
		RemoveListeners();
		RestartButtonClicked?.Invoke();
	}

	private void QuitToMenu()
	{
		RemoveListeners();
		QuitButtonClicked?.Invoke();
	}
}
