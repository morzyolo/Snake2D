using System.Threading.Tasks;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private SceneTransition _sceneTransition;

    private void Start()
    {
        _sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void StartGame() => _sceneTransition.SwitchToScene("Game");

    public async void ExitGame()
    {
        var task = _sceneTransition.CloseScene();
        await Task.WhenAll(task);
        Application.Quit();
    }
}