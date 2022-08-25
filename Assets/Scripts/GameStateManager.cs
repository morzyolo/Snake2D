using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Text _text;
    [SerializeField] private Button _button;

    private int _score;

    private void Awake()
    {
        Snake.GameOverAction += LoseGame;
        _inputManager.gameObject.SetActive(false);
        _button.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        _score = 0;
        _inputManager.gameObject.SetActive(true);
        _button.gameObject.SetActive(false);
        _text.text = "";
        _snake.MoveSnake();
    }

    private void LoseGame()
    {
        _text.text = $"Your Score: {_score}";
        _button.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Snake.GameOverAction -= LoseGame;
    }
}