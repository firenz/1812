using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class OptionsMenuButton : UIGenericButton {
	public string buttonID;

	protected string buttonName = "Name";
	protected string groupID = "OPTIONS_MENU";
	protected string stringID = "NAME";
	protected bool isMenuActive = false;
	protected Text buttonText;

	public delegate void MenuIsActivated();
	public static event MenuIsActivated menuActivated;
	public delegate void EnableButtons();
	public static event EnableButtons enableButtons;
	public delegate void DisableButtons();
	public static event DisableButtons disableButtons;

	protected void OnEnable(){
		ModalWindowHandler.onInitialized += DisableButton;
		ModalWindowHandler.onDisable += EnableButton;
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
		ReturnToButton.disableButtons += DisableMenus;
		OptionsMenuButton.disableButtons += DisableButton;
		OptionsMenuButton.enableButtons += EnableButton;
	}
	
	protected void OnDisable(){
		ModalWindowHandler.onInitialized -= DisableButton;
		ModalWindowHandler.onDisable -= EnableButton;
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
		ReturnToButton.disableButtons -= DisableMenus;
		OptionsMenuButton.disableButtons -= DisableButton;
		OptionsMenuButton.enableButtons -= EnableButton;
	}

	protected virtual void Start(){
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		buttonName = LocalizedTextManager.GetLocalizedText(groupID, buttonID, stringID)[0];
		buttonText.text = buttonName;
	}
	
	protected virtual void EnableButton(){
		this.GetComponent<Button>().interactable = true;
	}
	
	protected virtual void DisableButton(){
		this.GetComponent<Button>().interactable = false;
	}

	protected void MenuActivated(){
		isMenuActive = true;
		menuActivated();
	}

	protected void DisableAllButtons(){
		disableButtons();
	}

	protected void EnableAllButtons(){
		enableButtons();
	}

	protected void ChangeLanguage(){
		buttonName = LocalizedTextManager.GetLocalizedText(groupID, buttonID, stringID)[0];
		buttonText.text = buttonName;
	}

	public virtual void EnableMenus(){
		DisableButton();
		MenuActivated();
	}

	public virtual void DisableMenus(){
		ModalWindowHandler.Instance.Disable();
		EnableButton();
		isMenuActive = false;
	}

	public bool IsMenuActive(){
		return isMenuActive;
	}
}
