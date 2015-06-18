using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveButton : UIGenericButton {
	public SavesGameSlotsManager gameSlotsManager;

	private Text saveBtnText;

	private void OnEnable(){
		//ModalWindowHandler.onInitialized += DisableButton;
		//ModalWindowHandler.onDisable += EnableButton;
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
	}
	
	private void OnDisable(){
		//ModalWindowHandler.onInitialized -= DisableButton;
		//ModalWindowHandler.onDisable += EnableButton;
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
	}

	// Use this for initialization
	private void Start () {
		saveBtnText = this.transform.FindChild("Text").GetComponent<Text>();
		saveBtnText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "SAVE_GAME")[0];
	}
	
	// Update is called once per frame
	protected override void Update () {
		if(isMouseOver){
			if(this.GetComponent<Button>().interactable){
				CustomCursorController.Instance.ChangeCursorOverUIButton();
			}
		}
	}

	public override void OnClick(){
		base.OnClick();
		gameSlotsManager.Save();
	}

	private void ChangeLanguage(){
		saveBtnText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "SAVE_GAME")[0];
	}

	private void DisableButton(){
		this.GetComponent<Button>().interactable = false;
	}
	
	private void EnableButton(){
		this.GetComponent<Button>().interactable = true;
	}
}
