using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanguageButton : UIGenericButton {
	private string languageID;
	private string languageName;
	
	public delegate void DisableLanguagePanel();
	public static event DisableLanguagePanel disableLanguagePanel;

	public void Initialize(string language, string lanID){
		languageID = lanID;
		languageName = language;
		this.transform.FindChild("Text").GetComponent<Text>().text = languageName;
	}

	public override void OnClick(){
		base.OnClick();
		LocalizedTextManager.ChangeCurrentLanguage(languageID);
		disableLanguagePanel();
	}

	public string GetLanguageName(){
		return languageName;
	}

	public string GetLanguageID(){
		return languageID;
	}
}
