using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicSlider : UIGenericSlider {
	private int currentMusicVolume;
	
	private void Start() {
		currentMusicVolume = GameState.SystemData.AudioVolumeSettings.music;

		//Volume goes 25 to 25
		int _sliderValue = currentMusicVolume/25; 
		thisSlider.value = _sliderValue;
	}

	protected override void ActionOnValueChange(){
		int _newSliderValue = (int)thisSlider.value;
		currentMusicVolume = _newSliderValue * 25;
		AudioManager.ChangeVolumeAllCurrentMusics(currentMusicVolume);
	}

}
