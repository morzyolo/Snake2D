using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private SceneTransition _sceneTransition;

    private void Start()
    {
        _sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void StartGame() => _sceneTransition.SwitchToScene("Game");

    public void ExitGame()
    {
        _sceneTransition.CloseScene();
        Application.Quit();
    }
}