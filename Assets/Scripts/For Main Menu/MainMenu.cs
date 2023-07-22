using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MainMenu : MonoBehaviour
{
	[SerializeField] private string _gameSceneName = "Game";
	[SerializeField] private SceneTransition _sceneTransition;

	[Header("Name of triggers in animator")]
	[SerializeField] private string _showMenuTriggerName = "ShowMenu";
	[SerializeField] private string _showSettingsTriggerName = "ShowSettings";

	[Header("Main menu buttons")]
	[SerializeField] private Button _startButton;
	[SerializeField] private Button _settingsButton;
	[SerializeField] private Button _exitButton;

	private Animator _menuAnimator;

	private void Start()
	{
		_menuAnimator = GetComponent<Animator>();
		AddLintenersToButtons();
	}

	public void ShowMainMenu()
		=> _menuAnimator.SetTrigger(_showMenuTriggerName);

	private void StartGame()
	{
		RemoveListenersFromButtons();
		_sceneTransition.SwitchToScene(_gameSceneName);
	}

	private void ShowSettings()
		=> _menuAnimator.SetTrigger(_showSettingsTriggerName);

	private void AddLintenersToButtons()
	{
		_startButton.onClick.AddListener(StartGame);
		_settingsButton.onClick.AddListener(ShowSettings);
		_exitButton.onClick.AddListener(ExitGame);
	}

	private void RemoveListenersFromButtons()
	{
		_startButton.onClick.RemoveListener(StartGame);
		_settingsButton.onClick.RemoveListener(ShowSettings);
		_exitButton.onClick.RemoveListener(ExitGame);
	}

	private async void ExitGame()
	{
		RemoveListenersFromButtons();
		var task = _sceneTransition.CloseScene();
		await Task.WhenAll(task);
		Application.Quit();
	}
}