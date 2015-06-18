using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadButton : UIGenericButton {
	public SavesGameSlotsManager gameSlotsManager;

	private Text loadBtnText;
	
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
		loadBtnText = this.transform.FindChild("Text").GetComponent<Text>();
		loadBtnText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "LOAD_GAME")[0];
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
		gameSlotsManager.Load();
	}
	
	private void ChangeLanguage(){
		loadBtnText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SAVED_GAMES", "LOAD_GAME")[0];
	}
	
	private void DisableButton(){
		this.GetComponent<Button>().interactable = false;
	}
	
	private void EnableButton(){
		this.GetComponent<Button>().interactable = true;
	}
}
