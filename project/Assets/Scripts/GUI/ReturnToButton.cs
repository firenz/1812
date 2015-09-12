using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ReturnToButton : UIGenericButton {
	public static bool isAnyMenuActive;

	protected string returnToGameDisplayingText = "";
	protected string returnToMenuDisplayingText = "";

	public delegate void ResetUIPositions();
	public static event ResetUIPositions resetUIPositions;

	protected override void OnEnable(){
		LocatedTextManager.currentLanguageChanged += ChangeButtonTextLanguage;
		MenuButton.menuSetActive += ReturnToMenuModeActivated;
		ExitGameButton.abortedExitGame += ReturnToGameModeActivated;
		GadipediaSearchManager.beginSearchResults += DisableButton;
		GadipediaSearchManager.endSearchResults += EnableButton;
	}

	protected override void OnDisable(){
		LocatedTextManager.currentLanguageChanged -= ChangeButtonTextLanguage;
		MenuButton.menuSetActive -= ReturnToMenuModeActivated;
		ExitGameButton.abortedExitGame -= ReturnToGameModeActivated;
		GadipediaSearchManager.beginSearchResults -= DisableButton;
		GadipediaSearchManager.endSearchResults -= EnableButton;
	}

	protected override void Start () {
		thisButton = this.GetComponent<Button>();
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		isAnyMenuActive = false;
		returnToGameDisplayingText = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, "RETURN_GAME")[0];
		returnToMenuDisplayingText = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, "RETURN_MENU")[0];
		buttonText.text = returnToGameDisplayingText;
	}

	protected override void ActionOnClick (){
		if(isAnyMenuActive){
			ReturnToGameModeActivated();
		}
		else{
			StartCoroutine(ChangeToScene(GameState.lastPlayedLevel));
		}
	}

	protected IEnumerator ChangeToScene(string scene){
		do{
			yield return null;
		}while(AudioManager.IsPlayingSFX(sfxOnClick));
		GameController.WarpToLevel(scene);
	}

	protected virtual void ReturnToMenuModeActivated(){
		buttonText.text = returnToMenuDisplayingText;
		isAnyMenuActive = true;
	}
	
	protected virtual void ReturnToGameModeActivated(){
		resetUIPositions();
		buttonText.text = returnToGameDisplayingText;
		isAnyMenuActive = false;
	}

	protected override void ChangeButtonTextLanguage(){
		returnToGameDisplayingText = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, "RETURN_GAME")[0];
		returnToMenuDisplayingText = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, "RETURN_MENU")[0];

		if(isAnyMenuActive){
			buttonText.text = returnToMenuDisplayingText;
		}
		else{
			buttonText.text = returnToGameDisplayingText;
		}
	}
}
