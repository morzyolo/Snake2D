using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Text _startNRestartText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Button _startNRestartButton;

    private int _score;

    private void Start()
    {
        Snake.GameOverAction += LoseGame;
        CellGrid.CherryEatenAction += AddScore;

        _inputManager.gameObject.SetActive(false);
        _startNRestartButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        _score = 0;
        _scoreText.text = $"Your Score: {_score}";

        _inputManager.gameObject.SetActive(true);
        _startNRestartButton.gameObject.SetActive(false);
        _startNRestartText.text = "";
        _snake.MoveSnake();
    }

    private void AddScore()
    {
        _scoreText.text = $"Your Score: {++_score}";
    }

    private void LoseGame()
    {
        _startNRestartText.text = $"Your Score: {_score}";
        _startNRestartButton.gameObject.SetActive(true);
        _startNRestartButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        Snake.GameOverAction -= LoseGame;
        CellGrid.CherryEatenAction -= AddScore;
    }
}