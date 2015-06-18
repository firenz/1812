using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeleteButton : UIGenericButton {
	public SavesGameSlotsManager gameSlotsManager;

	private Text deleteBtnText;
	
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
		deleteBtnText = this.transform.FindChild("Text").GetComponent<Text>();
		deleteBtnText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "DELETE_GAME")[0];
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
		gameSlotsManager.Delete();
	}
	
	private void ChangeLanguage(){
		deleteBtnText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "DELETE_GAME")[0];
	}
	
	private void DisableButton(){
		this.GetComponent<Button>().interactable = false;
	}
	
	private void EnableButton(){
		this.GetComponent<Button>().interactable = true;
	}
}
