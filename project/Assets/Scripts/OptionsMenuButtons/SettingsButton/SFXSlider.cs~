using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SFXSlider : UIGenericButton {
	private Slider sfxSlider;
	private int currentSFXVolume;
	
	// Use this for initialization
	private void Start () {
		currentSFXVolume = GameState.SystemData.AudioVolumeSettings.sfx;
		sfxSlider = this.GetComponent<Slider>();

		int _sliderValue = currentSFXVolume/25; //Volume goes 25 to 25
		sfxSlider.value = _sliderValue;
		
	}
	
	public void OnVolumeChange(){
		int _newSliderValue = (int)sfxSlider.value;
		currentSFXVolume = _newSliderValue * 25;

		GameState.SystemData.AudioVolumeSettings.sfx = currentSFXVolume;
		SettingsFileManager.Instance.SaveSettingsFile();
		//AudioManager.ChangeSFXVolume(currentSFXVolume) to be implemented
	}
	
}
