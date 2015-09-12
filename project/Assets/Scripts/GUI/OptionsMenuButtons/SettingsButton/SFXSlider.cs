using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SFXSlider : UIGenericSlider {
	private int currentSFXVolume;

	private void Start () {
		currentSFXVolume = GameState.SystemData.AudioVolumeSettings.sfx;

		//Volume goes 25 to 25
		int _sliderValue = currentSFXVolume/25;
		thisSlider.value = _sliderValue;
	}
	
	protected override void ActionOnValueChange() {
		int _newSliderValue = (int)thisSlider.value;
		currentSFXVolume = _newSliderValue * 25;
		AudioManager.ChangeVolumeAllCurrentSFX(currentSFXVolume);
	}
}
