using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExitGameButton : OptionsMenuButton {

	public override void OnClick (){
		StartCoroutine(WaitForExitGameOnClick());
	}

	protected override void Start (){
		buttonID = "EXIT_GAME";
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		buttonName = LocalizedTextManager.GetLocalizedText(groupID, buttonID, stringID)[0];
		buttonText.text = buttonName;
	}

	private IEnumerator WaitForExitGameOnClick(){
		DisableAllButtons();
		isMenuActive = true;
		MenuActivated();

		ModalWindowHandler.Instance.Initialize("CLOSE_GAME");
		do{
			yield return null;
		}while(!ModalWindowHandler.Instance.IsSelectionEnded());

		if(ModalWindowHandler.Instance.IsYesClicked()){
			ModalWindowHandler.Instance.Disable();
			Application.Quit();
		}
		else{
			isMenuActive = false;
			EnableButton();
			ModalWindowHandler.Instance.Disable();
			EnableAllButtons();
		}
	}
}
