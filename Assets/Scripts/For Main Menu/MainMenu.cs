using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    private SceneTransition _sceneTransition;
    private Animator _menuAnimator;

    private void Start()
    {
        _menuAnimator = GetComponent<Animator>();
        _sceneTransition = FindObjectOfType<SceneTransition>();
        ShowMenu();
    }

    private async void ShowMenu()
    {
        await Task.Delay(1250);
        _menuAnimator.SetTrigger("Start");
    }

    public void ShowSettings() => _menuAnimator.SetTrigger("ShowSettings");

    public void ShowMainMenu() => _menuAnimator.SetTrigger("ShowMenu");

    public void StartGame() => _sceneTransition.SwitchToScene("Game");

    public async void ExitGame()
    {
        var task = _sceneTransition.CloseScene();
        await Task.WhenAll(task);
        Application.Quit();
    }
}