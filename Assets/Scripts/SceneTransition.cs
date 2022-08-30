using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    private static SceneTransition _instance;

    private Animator _sceneTransitionAnimator;
    private AsyncOperation _asyncOperation;

    private void Start()
    {
        if (_instance == null) 
            _instance = this;

        _sceneTransitionAnimator = GetComponent<Animator>();
        _sceneTransitionAnimator.SetTrigger("SceneOpening");
    }

    public void SwitchToScene(string sceneName)
    {
        _sceneTransitionAnimator.SetTrigger("SceneClosing");
        _asyncOperation = SceneManager.LoadSceneAsync("Game");
        _asyncOperation.allowSceneActivation = false;
    }

    public void CloseScene() => _sceneTransitionAnimator.SetTrigger("SceneClosing");

    public void OnAnimationOver()
    {
        if (_asyncOperation != null )
            _asyncOperation.allowSceneActivation = true;
    }
}