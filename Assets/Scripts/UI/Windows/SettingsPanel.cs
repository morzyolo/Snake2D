using System.IO;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
	[SerializeField] private SettingsDataHandler _settingsDataHandler;

	[SerializeField] private MainMenu _menu;
	[SerializeField] private Mixer _soundMixer;
	[SerializeField] private Mixer _effectsMixer;

	private void Start() => LoadSettings();

	private void LoadSettings()
	{
		var settingsData = _settingsDataHandler.Settings;

		_soundMixer.ChangeVolume(settingsData.SoundsVolume);
		_effectsMixer.ChangeVolume(settingsData.EffectsVolume);
	}

	private void SaveSettings()
	{
		var settingsData = new SettingsData()
		{
			SoundsVolume = _soundMixer.Volume,
			EffectsVolume = _effectsMixer.Volume
		};

		_settingsDataHandler.SaveSettings(settingsData);
	}

	public void ClosePanel()
	{
		_menu.ShowMainMenu();
		SaveSettings();
	}

	public void VisitGHPage() => Application.OpenURL("https://github.com/morzyolo");
}