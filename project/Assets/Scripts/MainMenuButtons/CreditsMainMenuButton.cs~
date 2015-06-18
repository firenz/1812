using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsMainMenuButton : UIGenericButton {
	public GameObject returnToBtn;

	private Text creditsMainMenuBtnText;
	private GameObject creditsMenu;

	private void OnEnable(){
		ModalWindowHandler.onInitialized += DisableButton;
		ModalWindowHandler.onDisable += EnableButton;
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
		ReturnToMainMenuButton.disableAll += DisableMenu;
	}
	
	private void OnDisable(){
		ModalWindowHandler.onInitialized -= DisableButton;
		ModalWindowHandler.onDisable -= EnableButton;
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
		ReturnToMainMenuButton.disableAll -= DisableMenu;
	}
	
	private void Start(){
		creditsMenu = this.transform.parent.parent.parent.Find("CreditsMenu").gameObject;
		creditsMenu.transform.SetAsFirstSibling();
		creditsMainMenuBtnText = this.transform.FindChild("Text").GetComponent<Text>();
		creditsMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "CREDITS", "NAME")[0];
	}
	
	protected override void Update(){
		if(isMouseOver){
			if(this.GetComponent<Button>().interactable){
				CustomCursorController.Instance.ChangeCursorOverUIButton();
			}
		}
	}
	
	public override void OnClick(){
		base.OnClick();
		creditsMenu.transform.SetAsLastSibling();
	}
	
	private void ChangeLanguage(){
		creditsMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "CREDITS", "NAME")[0];
	}
	
	private void EnableButton(){
		this.GetComponent<Button>().interactable = true;
	}
	
	private void DisableButton(){
		this.GetComponent<Button>().interactable = false;
	}

	private void DisableMenu(){
		creditsMenu.transform.SetAsFirstSibling();
	}
}
