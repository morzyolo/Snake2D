using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MenuManager : MonoBehaviour
{
    private SceneTransition _sceneTransition;
    private Animator _menuAnimator;

    private void Start()
    {
        _menuAnimator = GetComponent<Animator>();
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