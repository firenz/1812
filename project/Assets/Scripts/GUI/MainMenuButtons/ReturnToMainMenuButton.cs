using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Transform))]
public class ReturnToMainMenuButton : ReturnToButton {
	private Color defaultColorText;
	private Transform buttonTransform; 
	private Image buttonImage;

	protected override void OnEnable(){
		LocatedTextManager.currentLanguageChanged += ChangeButtonTextLanguage;
		MenuButton.menuSetActive += ReturnToMenuModeActivated;
		ExitGameButton.abortedExitGame += ReturnToGameModeActivated;
		ModalWindowHandler.beginQuestion += DisableButton;
		ModalWindowHandler.endedQuestion += EnableButton;
	}
	
	protected override void OnDisable(){
		LocatedTextManager.currentLanguageChanged -= ChangeButtonTextLanguage;
		MenuButton.menuSetActive -= ReturnToMenuModeActivated;
		ExitGameButton.abortedExitGame -= ReturnToGameModeActivated;
		ModalWindowHandler.beginQuestion -= DisableButton;
		ModalWindowHandler.endedQuestion -= EnableButton;
	}

	protected override void Start(){
		base.Start();
		defaultColorText = buttonText.color;
		buttonTransform = this.GetComponent<Transform>();
		buttonImage = this.GetComponent<Image>();
		buttonText.color = Color.clear;
		buttonText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID)[0];
		HideButton();
	}

	protected override void ActionOnClick (){
		ReturnToGameModeActivated();
	}

	protected override void ReturnToMenuModeActivated(){
		isAnyMenuActive = true;
		UnhideButton();
		buttonTransform.SetAsLastSibling();
	}
	
	protected override void ReturnToGameModeActivated(){
		base.ReturnToGameModeActivated();
		HideButton();
	}

	private void UnhideButton(){
		base.EnableButton();
		buttonText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID)[0];
		buttonText.color = defaultColorText;
		buttonImage.enabled = true;
	}
	
	private void HideButton(){
		base.DisableButton();
		buttonText.color = Color.clear;
		buttonImage.enabled = false;
	}
}

