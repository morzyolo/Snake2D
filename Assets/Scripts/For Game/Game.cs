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
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Text _continueText;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movementInput.gameObject.SetActive(false);
        _continueButton.onClick.AddListener(StartGame);
        _restartButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        _continueButton.onClick.RemoveAllListeners();
        _animator.SetTrigger("DisappearText");
        _movementInput.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(false);
        GameStarted?.Invoke();
    }

    private async void ShowLosing()
    {
        _movementInput.gameObject.SetActive(false);
        _continueButton.gameObject.SetActive(true);
        await Task.Delay(1000);
        _continueButton.onClick.AddListener(AllowRestart);
        _continueText.text = "Click to continue";
        _animator.SetTrigger("AppearText");
    }

    private void AllowRestart()
    {
        _animator.SetTrigger("DisappearText");
        _animator.SetTrigger("AppearButtons");
        _continueButton.onClick.RemoveAllListeners();
        _quitButton.onClick.AddListener(GoToMenu);
        _restartButton.onClick.AddListener(RestartGame);
        _restartButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        _continueButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
        _sceneTransition.SwitchToScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void GoToMenu()
    {
        _continueButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
        _sceneTransition.SwitchToScene("MainMenu");
    }

    private void OnEnable()
    {
        GameOver += ShowLosing;
    }

    private void OnDisable()
    {
        GameOver -= ShowLosing;
    }
}