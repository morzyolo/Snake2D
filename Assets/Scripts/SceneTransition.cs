using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    private Animator _sceneTransitionAnimator;
    private AsyncOperation _asyncOperation;
    
    private bool _animationDone = false;

    private void Start()
    {
        _sceneTransitionAnimator = GetComponent<Animator>();
        _sceneTransitionAnimator.SetTrigger("SceneOpening");
    }

    public void SwitchToScene(string sceneName)
    {
        _sceneTransitionAnimator.SetTrigger("SceneClosing");
        _asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        _asyncOperation.allowSceneActivation = false;
    }

    public async Task CloseScene()
    {
        _animationDone = false;
        _sceneTransitionAnimator.SetTrigger("SceneClosing");
        await Task.WhenAll(AnimationDone());
    }

    public void OnTransitionOver()
    {
        _animationDone = true;
        if (_asyncOperation != null )
            _asyncOperation.allowSceneActivation = true;
    }

    public async Task<bool> AnimationDone()
    {
        while (!_animationDone)
            await Task.Delay(5);
        _animationDone = false;
        return true;
    }
}