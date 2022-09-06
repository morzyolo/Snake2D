using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private SceneTransition _sceneTransition;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _text;
    [SerializeField] private Button _button;

    private int _score;

    private void Start()
    {
        Snake.GameOverAction += LoseGame;
        CellGrid.CherryEatenAction += AddScore;

        _inputManager.gameObject.SetActive(false);
        _button.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        _score = 0;
        _scoreText.text = $"Score: {_score}";

        _inputManager.gameObject.SetActive(true);
        _button.gameObject.SetActive(false);
        _text.text = "";
        _snake.MoveSnake();
        _button.onClick.RemoveAllListeners();
    }

    private void AddScore()
    {
        _scoreText.text = $"Your Score: {++_score}";
    }

    private void LoseGame()
    {
        _text.text = $"Your Score: {_score}";
        _button.gameObject.SetActive(true);
        _button.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        _sceneTransition.SwitchToScene(SceneManager.GetActiveScene().name);
    }

    private void OnDisable()
    {
        Snake.GameOverAction -= LoseGame;
        CellGrid.CherryEatenAction -= AddScore;
    }
}