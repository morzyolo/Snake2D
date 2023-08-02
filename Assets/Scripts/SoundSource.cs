using UnityEngine;

public class SoundSource : MonoBehaviour
{
	private void Awake()
	{
		SoundSource[] sound = FindObjectsOfType<SoundSource>();

		if (sound != null && sound.Length > 1)
			Destroy(this.gameObject);
		else
			DontDestroyOnLoad(this.gameObject);
	}
}