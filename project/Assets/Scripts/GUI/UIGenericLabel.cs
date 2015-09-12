using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class UIGenericLabel : MonoBehaviour {
	[SerializeField]
	private string localizedTextGroupID = "OPTIONS_MENU";
	[SerializeField]
	private string localizedTextMenuID;
	[SerializeField]
	private string localizedTextLabelID;

	private Text label;

	private void OnEnable(){
		LocatedTextManager.currentLanguageChanged += ChangeLanguage;
	}
	
	private void OnDisable(){
		LocatedTextManager.currentLanguageChanged -= ChangeLanguage;
    }

	// Use this for initialization
	private void Start () {
		label = this.GetComponent<Text>();
		label.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextMenuID, localizedTextLabelID)[0];
	}

	private void ChangeLanguage(){
		label.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextMenuID, localizedTextLabelID)[0];
	}
}
