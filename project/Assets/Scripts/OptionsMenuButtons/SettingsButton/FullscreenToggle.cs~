using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FullscreenToggle : UIGenericButton {
	private Text fullscreenToggleText;
	private Toggle toggle;
	private bool currentFullscreen;

	private void OnEnable(){
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
	}
	
	private void OnDisable(){
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
	}

	// Use this for initialization
	private void Start () {
		fullscreenToggleText = this.transform.FindChild("Label").GetComponent<Text>();
		fullscreenToggleText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SETTINGS", "FULLSCREEN_TOGGLE")[0];
		toggle = this.GetComponent<Toggle>();
		currentFullscreen = Screen.fullScreen;

		if(currentFullscreen){
			toggle.isOn = true;
		}
		else{
			toggle.isOn = false;
		}
	}

	private void ChangeLanguage(){
		fullscreenToggleText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SETTINGS", "FULLSCREEN_TOGGLE")[0];
	}

	public void OnClick(){
		if(!currentFullscreen){
			currentFullscreen = true;
			toggle.isOn = true;
		}
		else{
			currentFullscreen = false;
			toggle.isOn = false;
		}

		this.transform.parent.Find("CurrentResolutionButton").GetComponent<CurrentResolutionButton>().DisablePanel();

		Screen.SetResolution(Screen.width, Screen.height, currentFullscreen); 

		//To be implemented Modal Window with countdown asking if the resolution chosen is correct

		GameState.ChangeScreenSettings(Screen.width, Screen.height, currentFullscreen);
	}

}
