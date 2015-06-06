using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitGameMainMenuButton : UIGenericButton {
	private Text exitGameMainMenuBtnText;

	private void OnEnable(){
		ModalWindowHandler.onInitialized += DisableButton;
		ModalWindowHandler.onDisable += EnableButton;
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
	}
	
	private void OnDisable(){
		ModalWindowHandler.onInitialized -= DisableButton;
		ModalWindowHandler.onDisable -= EnableButton;
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
	}

	private void Start(){
		exitGameMainMenuBtnText = this.transform.FindChild("Text").GetComponent<Text>();
		exitGameMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "EXIT_GAME", "NAME")[0];
	}

	protected override void Update(){
		if(isMouseOver){
			if(this.GetComponent<Button>().interactable){
			CustomCursorController.Instance.ChangeCursorOverUIButton();
			}
		}
	}

	public void OnClick(){
		StartCoroutine(WaitForExitGame());
	}

	private IEnumerator WaitForExitGame(){
		ModalWindowHandler.Instance.Initialize("CLOSE_GAME");
		do{
			yield return null;
		}while(!ModalWindowHandler.Instance.IsSelectionEnded());
		
		if(ModalWindowHandler.Instance.IsYesClicked()){
			ModalWindowHandler.Instance.Disable();
			Application.Quit();
		}
		else{
			ModalWindowHandler.Instance.Disable();
		}
	}

	private void ChangeLanguage(){
		exitGameMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "EXIT_GAME", "NAME")[0];
	}

	private void EnableButton(){
		this.GetComponent<Button>().interactable = true;
	}

	private void DisableButton(){
		this.GetComponent<Button>().interactable = false;
	}
}
