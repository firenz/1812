using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentResolutionButton : UIGenericButton {
	private GameObject resolutionPanel;
	private Text currentResolutionText;
	private Text resolutionLabel;
	private int currentScreenWidth;
	private int currentScreenHeight;

	private void OnEnable(){
		ChangeResolutionButton.disableResolutionPanel += DisablePanel;
		LocalizedTextManager.currentLanguageChanged  += ChangeLanguage;
	}

	private void OnDisable(){
		ChangeResolutionButton.disableResolutionPanel -= DisablePanel;
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
	}

	// Use this for initialization
	private void Start () {
		currentScreenWidth = Screen.width;
		currentScreenHeight = Screen.height;
		currentResolutionText = this.transform.FindChild("Text").GetComponent<Text>();
		resolutionLabel = this.transform.FindChild("CurrentResolutionLabel").GetComponent<Text>();
		resolutionLabel.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SETTINGS", "CHANGE_RESOLUTION_BUTTON")[0];
		currentResolutionText.text = currentScreenWidth + "x" + currentScreenHeight;
		resolutionPanel = this.transform.FindChild("ResolutionPanel").gameObject;
	}

	private void ChangeLanguage(){
		resolutionLabel.text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "SETTINGS", "CHANGE_RESOLUTION_BUTTON")[0];
	}

	public override void OnClick(){
		base.OnClick();
		if(resolutionPanel.activeSelf){
			DisablePanel();
		}
		else{
			resolutionPanel.SetActive(true);
		}
	}

	public void ChangeDisplayingResolution(int width, int height){
		currentResolutionText.text = width + "x" + height;
	}

	public void DisablePanel(){
		currentScreenWidth = Screen.width;
		currentScreenHeight = Screen.height;
		currentResolutionText.text = currentScreenWidth + "x" + currentScreenHeight;
		resolutionPanel.transform.FindChild("ScrollView/Scrollbar").GetComponent<Scrollbar>().value = 1f;
		resolutionPanel.SetActive(false);
	}
}
