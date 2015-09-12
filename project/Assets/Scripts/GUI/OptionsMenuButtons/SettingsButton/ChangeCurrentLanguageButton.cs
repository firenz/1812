using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeCurrentLanguageButton : UIGenericButton {
	public string languageID { get; private set;}
	public string languageName { get; private set;}
	
	public delegate void DisableLanguagePanel();
	public static event DisableLanguagePanel disableLanguagePanel;

	protected override void Start (){
		thisButton = this.GetComponent<Button>();
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
	}

	public void Initialize(string language, string langID){
		Start();
		languageID = langID;
		languageName = language;
		buttonText.text = languageName;
	}

	protected override void ActionOnClick (){
		disableLanguagePanel();
		LocatedTextManager.ChangeCurrentLanguage(languageID);
	}

	protected override void ChangeButtonTextLanguage (){}
}
