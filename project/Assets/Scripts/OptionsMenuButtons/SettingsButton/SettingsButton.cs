using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsButton : OptionsMenuButton {
	private GameObject settingsMenu;
	private GameObject languagesPanel;
    
    // Use this for initialization
	protected override void Start () {
		buttonID = "SETTINGS";
		settingsMenu = this.transform.parent.parent.FindChild("SettingsMenu").gameObject;
		settingsMenu.transform.SetAsFirstSibling();
		languagesPanel = this.transform.parent.parent.FindChild("SettingsMenu/LanguagesPanel").gameObject;
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		buttonName = LocalizedTextManager.GetLocalizedText(groupID, buttonID, stringID)[0];
		buttonText.text = buttonName;
	}

	public override void OnClick (){
		SettingsMenuOnClick();
	}

	public void SettingsMenuOnClick(){
		EnableMenus();
	}
	
	public override void EnableMenus(){
		DisableButton();
		MenuActivated();
		isMenuActive = true;
		settingsMenu.transform.SetAsLastSibling();
	}
	
	public override void DisableMenus(){
		ModalWindowHandler.Instance.Disable();
		EnableButton();
		isMenuActive = false;
		settingsMenu.transform.SetAsFirstSibling();
		languagesPanel.GetComponent<LanguagesPanelManager>().DisableLanguagesPanel();
		settingsMenu.transform.FindChild("SettingsPanel/GraphicSettings/CurrentResolutionButton").GetComponent<CurrentResolutionButton>().DisablePanel();
	}	
}
