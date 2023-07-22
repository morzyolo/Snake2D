using System.IO;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
	[SerializeField] private MainMenu _menu;
	[SerializeField] private Mixer _soundMixer;
	[SerializeField] private Mixer _effectsMixer;

	private string _pathToSettings;

	private void Start()
	{
		_pathToSettings = Application.dataPath + "/settings.json";
		LoadSettings();
	}

	private void LoadSettings()
	{
		if (File.Exists(_pathToSettings))
		{
			string data = File.ReadAllText(_pathToSettings);
			SettingsData settingsData = JsonUtility.FromJson<SettingsData>(data);
			_soundMixer.ChangeVolume(settingsData.SoundsVolume);
			_effectsMixer.ChangeVolume(settingsData.EffectsVolume);
		}
		else
		{
			_soundMixer.ChangeVolume(-10f);
			_effectsMixer.ChangeVolume(-10f);
		}
	}

	private void SaveSettings()
	{
		SettingsData settingsData = new SettingsData()
		{
			SoundsVolume = _soundMixer.Volume,
			EffectsVolume = _effectsMixer.Volume
		};

		string data = JsonUtility.ToJson(settingsData);
		File.WriteAllText(_pathToSettings, data);
	}

	public void ClosePanel()
	{
		_menu.ShowMainMenu();
		SaveSettings();
	}

	public void VisitGHPage()
	{
		Application.OpenURL("https://github.com/morzyolo");
	}
}