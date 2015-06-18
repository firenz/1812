using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeLanguageButton : UIGenericButton {
	private GameObject languagesPanel;
	private Text buttonText;

	private void OnEnable(){
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
	}

	private void OnDisable(){
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
	}

	// Use this for initialization
	private void Start () {
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		languagesPanel = this.transform.parent.parent.parent.Find("LanguagesPanel").gameObject;	

		languagesPanel.transform.SetAsFirstSibling();
		buttonText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SETTINGS", "CHANGE_LANGUAGE_BUTTON")[0];
	}

	public override void OnClick(){
		base.OnClick();
		languagesPanel.transform.SetAsLastSibling();
	}

	private void ChangeLanguage(){
		buttonText.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SETTINGS", "CHANGE_LANGUAGE_BUTTON")[0];
    }

}
