using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FullscreenToggle : UIGenericToggle {
	private bool currentFullscreen;

	protected override void Start (){
		base.Start();

		currentFullscreen = Screen.fullScreen;
		
		if(currentFullscreen){
			thisToggle.isOn = true;
		}
		else{
			thisToggle.isOn = false;
		}
	}

	protected override void SetOn (){
		currentFullscreen = true;
		GameState.ChangeScreenSettings(Screen.width, Screen.height, currentFullscreen);
	}

	protected override void SetOff (){
		currentFullscreen = false;	
		GameState.ChangeScreenSettings(Screen.width, Screen.height, currentFullscreen);
	}
}
