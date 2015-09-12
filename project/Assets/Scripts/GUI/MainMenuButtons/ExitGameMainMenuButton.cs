using UnityEngine;
using System.Collections;

public class ExitGameMainMenuButton : UIGenericButton {
	
	protected override void ActionOnClick (){
		StartCoroutine(WaitForExitGameOnClick());
	}

	private IEnumerator WaitForExitGameOnClick(){
		ModalWindowHandler.Instance.Initialize("CLOSE_GAME");
		do{
			yield return null;
		}while(!ModalWindowHandler.Instance.isAnswerSelected);
		
		if(ModalWindowHandler.Instance.isYesClicked){
			ModalWindowHandler.Instance.Disable();
			Application.Quit();
		}
		else{
			ModalWindowHandler.Instance.Disable();
		}
	}
}
