using System.IO;
using UnityEngine;

public class SettingsDataHandler : MonoBehaviour
{
	public SettingsData Settings { get; private set; }

	[SerializeField] private float _defaultAudioValue = -10f;

	private string _pathToSettings;

	private void Awake()
	{
		_pathToSettings = Application.dataPath + "/settings.json";
		LoadSettings();
	}

	private void LoadSettings()
	{
		if (File.Exists(_pathToSettings))
		{
			string data = File.ReadAllText(_pathToSettings);
			Settings = JsonUtility.FromJson<SettingsData>(data);
		}
		else
		{
			Settings = new SettingsData()
			{ SoundsVolume = _defaultAudioValue, EffectsVolume = _defaultAudioValue };
		}
	}

	public void SaveSettings(SettingsData settingsData)
	{
		Settings = settingsData;
		string data = JsonUtility.ToJson(settingsData);
		File.WriteAllText(_pathToSettings, data);
	}
}
