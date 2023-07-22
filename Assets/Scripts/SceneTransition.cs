using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
	[SerializeField] private string _sceneOpeningTriggerName = "SceneOpening";
	[SerializeField] private string _sceneClosingTriggerName = "SceneClosing";

	private Animator _sceneTransitionAnimator;
	private AsyncOperation _asyncOperation;

	private bool _isAnimationDone = false;

	private void Start()
	{
		_sceneTransitionAnimator = GetComponent<Animator>();
		_sceneTransitionAnimator.SetTrigger(_sceneOpeningTriggerName);
	}

	public void SwitchToScene(string sceneName)
	{
		_sceneTransitionAnimator.SetTrigger(_sceneClosingTriggerName);
		_asyncOperation = SceneManager.LoadSceneAsync(sceneName);
		_asyncOperation.allowSceneActivation = false;
	}

	public async Task CloseScene()
	{
		_isAnimationDone = false;
		_sceneTransitionAnimator.SetTrigger(_sceneClosingTriggerName);
		await Task.WhenAll(AnimationDone());
	}

	public void OnTransitionOver()
	{
		_isAnimationDone = true;
		if (_asyncOperation != null)
			_asyncOperation.allowSceneActivation = true;
	}

	public async Task<bool> AnimationDone()
	{
		while (!_isAnimationDone)
			await Task.Delay(5);
		_isAnimationDone = false;
		return true;
	}
}