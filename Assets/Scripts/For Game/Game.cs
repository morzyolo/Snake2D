using System;
using UnityEngine;

public class Game : MonoBehaviour
{
	public event Action GameStarted;

	[SerializeField] private GUI _gui;
	[SerializeField] private Snake _snake;
	[SerializeField] private SceneTransition _sceneTransition;

	[Header("Strings")]
	[SerializeField] private string _gameSceneName = "Game";
	[SerializeField] private string _mainMenuSceneName = "MainMenu";


	private void Start() => _gui.ShowStartScreen();

	private void StartGame() => GameStarted?.Invoke();

	private void ShowLosing() => _gui.ShowLosing();

	public void RestartGame() => GoToScene(_gameSceneName);

	public void QuitToMenu() => GoToScene(_mainMenuSceneName);

	private void GoToScene(string sceneName)
		=> _sceneTransition.SwitchToScene(sceneName);

	private void OnEnable()
	{
		_gui.GameStarted += StartGame;
		_snake.Hited += ShowLosing;
		_gui.RestartButtonClicked += RestartGame;
		_gui.QuitButtonClicked += QuitToMenu;
	}

	private void OnDisable()
	{
		_gui.GameStarted -= StartGame;
		_snake.Hited -= ShowLosing;
		_gui.RestartButtonClicked -= RestartGame;
		_gui.QuitButtonClicked -= QuitToMenu;
	}
}