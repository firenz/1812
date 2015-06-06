using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewGameMainMenuButton : UIGenericButton {
	private Text newMainMenuBtnText;

	private void OnEnable(){
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
	}
	
	private void OnDisable(){
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
	}

	// Use this for initialization
	private void Awake () {
		newMainMenuBtnText = this.transform.FindChild("Text").GetComponent<Text>(); 	
		newMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "NEW_GAME", "NAME")[0];
	}

	public void OnClick() {
		GameController.WarpToLevel("DemoScene_01");
	}

	private void ChangeLanguage(){
		newMainMenuBtnText.text = LocalizedTextManager.GetLocalizedText("MAIN_MENU", "NEW_GAME", "NAME")[0];
	}
}
