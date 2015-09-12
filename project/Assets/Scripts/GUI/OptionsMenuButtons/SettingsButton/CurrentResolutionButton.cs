using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentResolutionButton : UIGenericButton {
	private int currentScreenWidth;
	private int currentScreenHeight;
	private Text currentResolutionText;
	private GameObject resolutionPanel;

	protected override void OnEnable(){
		base.OnEnable();
		ChangeResolutionButton.updatedScreenSize += UpdateCurrentResolutionDisplayingText;
		ReturnToButton.resetUIPositions += DisablePanel;
	}

	protected override void OnDisable(){
		base.OnDisable();
		ChangeResolutionButton.updatedScreenSize -= UpdateCurrentResolutionDisplayingText;
		ReturnToButton.resetUIPositions -= DisablePanel;
	}

	protected override void Start(){
		thisButton = this.GetComponent<Button>();
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		currentScreenWidth = Screen.width;
		currentScreenHeight = Screen.height;
		currentResolutionText = this.transform.FindChild("Text").GetComponent<Text>();
		currentResolutionText.text = currentScreenWidth + "x" + currentScreenHeight;
		resolutionPanel = this.transform.FindChild("ChangeResolutionPanel").gameObject;
		resolutionPanel.SetActive(false);
	}

	protected override void ActionOnClick (){
		if(resolutionPanel.activeSelf){
			resolutionPanel.SetActive(false);
		}
		else{
			resolutionPanel.SetActive(true);
        }
	}

	private void UpdateCurrentResolutionDisplayingText(int width, int height){
		currentResolutionText.text = width + "x" + height;
		DisablePanel();
	}

	public void DisablePanel(){
		resolutionPanel.SetActive(false);
	}

	protected override void ChangeButtonTextLanguage (){}
}
