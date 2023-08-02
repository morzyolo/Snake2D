using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SnakeAudioSource : MonoBehaviour
{
	[SerializeField] private Snake _snake;

	[SerializeField] private AudioClip _biteClip;

	private AudioSource _source;

	private void Start() => _source = GetComponent<AudioSource>();

	private void MakeEatemSound()
	{
		_source.clip = _biteClip;
		_source.Play();
	}

	private void OnEnable()
	{
		_snake.CherryEaten += MakeEatemSound;
	}

	private void OnDisable()
	{
		_snake.CherryEaten -= MakeEatemSound;
	}
}
