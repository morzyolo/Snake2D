using UnityEngine;

public class Sound : MonoBehaviour
{
	private void Awake()
	{
		Sound[] sound = FindObjectsOfType<Sound>();

		if (sound != null && sound.Length > 1)
			Destroy(this.gameObject);
		else
			DontDestroyOnLoad(this.gameObject);
	}
}