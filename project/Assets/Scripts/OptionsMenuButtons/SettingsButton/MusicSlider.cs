using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicSlider : UIGenericButton {
	private int currentMusicVolume;
	private Slider musicSlider;

	// Use this for initialization
	private void Start () {
		currentMusicVolume = GameState.SystemData.AudioVolumeSettings.music;
		musicSlider = this.GetComponent<Slider>();

		int _sliderValue = currentMusicVolume/25; //Volume goes 25 to 25
		musicSlider.value = _sliderValue;

	}

	public void OnVolumeChange(){
		int _newSliderValue = (int)musicSlider.value;
		currentMusicVolume = _newSliderValue * 25;

		GameState.SystemData.AudioVolumeSettings.music = currentMusicVolume;
		SettingsFileManager.Instance.SaveSettingsFile();
		//AudioManager.ChangeMusicVolume(currentMusicVolume) to be implemented
	}

}
