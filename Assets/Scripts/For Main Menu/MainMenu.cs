using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;

    private SceneTransition _sceneTransition;
    private Animator _menuAnimator;

    private void Start()
    {
        _menuAnimator = GetComponent<Animator>();
        _sceneTransition = FindObjectOfType<SceneTransition>();
        ShowMenu();
        AddLintenersToButtons();
    }

    private async void ShowMenu()
    {
        await Task.Delay(1250);
        _menuAnimator.SetTrigger("Start");
    }
    public void StartGame()
    {
        RemoveAllListenersFromButtons();
        _sceneTransition.SwitchToScene("Game");
    }

    public void ShowSettings()
    {
        RemoveAllListenersFromButtons();
        _menuAnimator.SetTrigger("ShowSettings");
    }

    public void ShowMainMenu()
    {
        AddLintenersToButtons();
        _menuAnimator.SetTrigger("ShowMenu");
    }

    private void AddLintenersToButtons()
    {
        _startButton.onClick.AddListener(StartGame);
        _settingsButton.onClick.AddListener(ShowSettings);
        _quitButton.onClick.AddListener(ExitGame);
    }

    private void RemoveAllListenersFromButtons()
    {
        _startButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    public async void ExitGame()
    {
        RemoveAllListenersFromButtons();
        var task = _sceneTransition.CloseScene();
        await Task.WhenAll(task);
        Application.Quit();
    }
}