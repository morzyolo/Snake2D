using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Game : MonoBehaviour
{
    public static Action GameStarted;

    [SerializeField] private MovementInput _movementInput;
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] private Button _button;
    [SerializeField] private Text _text;
    private Animator _animator;

    private void Start()
    {
        _movementInput.gameObject.SetActive(false);
        _animator = GetComponent<Animator>();
        _button.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        _button.onClick.RemoveAllListeners();
        _animator.SetTrigger("GameStarted");
        _movementInput.gameObject.SetActive(true);
        _button.gameObject.SetActive(false);
        GameStarted?.Invoke();
    }

    private void ShowLosing()
    {
        _button.gameObject.SetActive(true);
        _button.onClick.AddListener(RestartGame);
        _text.text = "Click to restart";
        _animator.SetTrigger("GameOver");
    }

    private void RestartGame()
    {
        _button.onClick.RemoveAllListeners();
        _sceneTransition.SwitchToScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        Snake.GameOver += ShowLosing;
    }

    private void OnDisable()
    {
        Snake.GameOver -= ShowLosing;
    }
}