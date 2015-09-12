using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitGameButton : MenuButton {

	public delegate void AbortedExitGame();
	public static event AbortedExitGame abortedExitGame;

	protected override void ActionOnClick (){
		CustomCursorController.Instance.ChangeCursorToDefault();
		EnableMenu();
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
			DisableMenu();
			abortedExitGame();
		}
	}
}
