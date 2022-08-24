using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private UnityEngine.UI.Text _Text;

    private void Awake()
    {
        _inputManager.gameObject.SetActive(false);
        _Text.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        _inputManager.gameObject.SetActive(true);
        _Text.gameObject.SetActive(false);
        _snake.MoveSnake();
        this.gameObject.SetActive(false);
    }
}