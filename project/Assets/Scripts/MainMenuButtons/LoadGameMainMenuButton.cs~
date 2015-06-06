using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadGameMainMenuButton : UIGenericButton {
	public GameObject returnToBtn;
	
	private Text loadMainMenuBtnText;
	private GameObject loadMenu;
	private GameObject gameSlotsPanel;
	
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
		loadMenu = this.transform.parent.parent.parent.Find("LoadGameMenu").gameObject;
		gameSlotsPanel = loadMenu.transform.Find("LoadGamePanel/SlotsPanel").gameObject;
		loadMenu.transform.SetAsFirstSibling();
		loadMainMenuBtnText = this.transform.FindChild("Text").GetComponent<Text>();
		loadMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "LOAD_GAME", "NAME")[0];
	}
	
	protected override void Update(){
		if(isMouseOver){
			if(this.GetComponent<Button>().interactable){
				CustomCursorController.Instance.ChangeCursorOverUIButton();
			}
		}
	}
	
	public void OnClick(){
		loadMenu.transform.SetAsLastSibling();
		/*
		if(loadMenu.transform.FindChild("LoadGamePanel/SlotsPanel").GetComponent<SavesGameSlotsManager>().SlotsCount() > 0){
			loadMenu.transform.SetAsLastSibling();
		}
		else{
			//Wrong sound because there's no game save yet
		}
		*/
	}
	
	private void ChangeLanguage(){
		loadMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "LOAD_GAME", "NAME")[0];
	}
	
	private void EnableButton(){
		this.GetComponent<Button>().interactable = true;
	}
	
	private void DisableButton(){
		this.GetComponent<Button>().interactable = false;
	}
	
	private void DisableMenu(){
		loadMenu.transform.SetAsFirstSibling();
		gameSlotsPanel.GetComponent<SavesGameSlotsManager>().Disable();
	}
}
