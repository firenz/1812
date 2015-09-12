using UnityEngine;
using System.Collections;

public class MainMenuButton : MenuButton {

	protected override void OnEnable(){
		base.OnEnable();
		ModalWindowHandler.beginQuestion += DisableButton;
		ModalWindowHandler.endedQuestion += EnableButton;
	}
	
	protected override void OnDisable(){
		base.OnDisable();
		ModalWindowHandler.beginQuestion -= DisableButton;
		ModalWindowHandler.endedQuestion -= EnableButton;
	}
}
