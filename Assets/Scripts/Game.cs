using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private MovementInput _movementInput;
    [SerializeField] private SceneTransition _sceneTransition;

    [SerializeField] private Button _button;
    [SerializeField] private Text _text;

    private void Start()
    {
        _movementInput.gameObject.SetActive(false);
        _button.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        _movementInput.gameObject.SetActive(true);
        _button.gameObject.SetActive(false);
        _text.text = "";
        _snake.MoveSnake();
        _button.onClick.RemoveAllListeners();
    }

    private void ShowLosing()
    {
        _button.gameObject.SetActive(true);
        _button.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
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