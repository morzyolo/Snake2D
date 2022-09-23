using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Game : MonoBehaviour
{
    public static Action GameStarted;
    public static Action GameOver;

    [SerializeField] private MovementInput _movementInput;
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] private Button _button;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Text _text;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movementInput.gameObject.SetActive(false);
        _button.onClick.AddListener(StartGame);
        _restartButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        _button.onClick.RemoveAllListeners();
        _animator.SetTrigger("DisappearText");
        _movementInput.gameObject.SetActive(true);
        _button.gameObject.SetActive(false);
        GameStarted?.Invoke();
    }

    private async void ShowLosing()
    {
        _movementInput.gameObject.SetActive(false);
        _button.gameObject.SetActive(true);
        await Task.Delay(1000);
        _button.onClick.AddListener(AllowRestart);
        _text.text = "Click to continue";
        _animator.SetTrigger("AppearText");
    }

    private void AllowRestart()
    {
        _animator.SetTrigger("DisappearText");
        _animator.SetTrigger("AppearButtons");
        _button.onClick.RemoveAllListeners();
        _quitButton.onClick.AddListener(GoToMenu);
        _restartButton.onClick.AddListener(RestartGame);
        _restartButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        _button.onClick.RemoveAllListeners();
        _sceneTransition.SwitchToScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void GoToMenu() => _sceneTransition.SwitchToScene("MainMenu");

    private void OnEnable()
    {
        GameOver += ShowLosing;
    }

    private void OnDisable()
    {
        GameOver -= ShowLosing;
    }
}