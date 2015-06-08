using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ReturnToButton : UIGenericButton {
	private bool isAnyMenuActive = false;
	private Text returnBtnText;
	private string returnToGame = "";
	private string returnToOptionsMenu = "";

	public delegate void DisableButtons();
	public static event DisableButtons disableButtons;

	public void OnEnable(){
		OptionsMenuButton.menuActivated += IsMenuEnabled;
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
	}

	public void OnDisable(){
		OptionsMenuButton.menuActivated -= IsMenuEnabled;
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
	}

	private void IsMenuEnabled(){
		returnBtnText.text = returnToOptionsMenu;
		isAnyMenuActive = true;
	}

	private void Start () {
		returnBtnText = this.transform.FindChild("Text").GetComponent<Text>();
		returnToGame = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "RETURN_TO", "RETURN_GAME")[0];
		returnToOptionsMenu = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "RETURN_TO", "RETURN_MENU")[0];
		returnBtnText.text = returnToGame;
	}

	public void ReturnToOnClick(){
		if(isAnyMenuActive){
			disableButtons();
			isAnyMenuActive = false;
			returnBtnText.text = returnToGame;
		}
		else{
			GameController.WarpToLevel(GameState.lastPlayableLevel);
		}
	}

	private void ChangeLanguage(){
		returnToGame = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "RETURN_TO", "RETURN_GAME")[0];
		returnToOptionsMenu = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "RETURN_TO", "RETURN_MENU")[0];

		if(isAnyMenuActive){
			returnBtnText.text = returnToGame;
		}
		else{
			returnBtnText.text = returnToOptionsMenu;
		}
	}
}
