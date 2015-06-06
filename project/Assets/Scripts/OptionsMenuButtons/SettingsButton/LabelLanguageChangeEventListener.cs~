using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LabelLanguageChangeEventListener : MonoBehaviour {
	public string groupID = "OPTIONS_MENU";
	public string menuID;
	public string labelID;
	private Text label;

	private void OnEnable(){
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
	}
	
	private void OnDisable(){
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
    }

	// Use this for initialization
	private void Start () {
		label = this.GetComponent<Text>();
		label.text = LocalizedTextManager.GetLocalizedText(groupID, menuID, labelID)[0];
	}

	private void ChangeLanguage(){
		label.text = LocalizedTextManager.GetLocalizedText(groupID, menuID, labelID)[0];
	}
}
