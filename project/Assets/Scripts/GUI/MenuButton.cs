using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class MenuButton : UIGenericButton {
	[SerializeField]
	protected Transform menuPanel = null;

	protected bool isThisMenuActive = false;

	public delegate void SetMenuActive();
	public static event SetMenuActive menuSetActive;

	protected override void OnEnable(){
		LocatedTextManager.currentLanguageChanged += ChangeButtonTextLanguage;
		MenuButton.menuSetActive += DisableButton;
		ReturnToButton.resetUIPositions += DisableMenu;
		ReturnToMainMenuButton.resetUIPositions += DisableMenu;
	}
	
	protected override void OnDisable(){
		LocatedTextManager.currentLanguageChanged -= ChangeButtonTextLanguage;
		MenuButton.menuSetActive -= DisableButton;
		ReturnToButton.resetUIPositions -= DisableMenu;
		ReturnToMainMenuButton.resetUIPositions -= DisableMenu;
	}

	protected override void Start(){
		base.Start();

		if(menuPanel != null){
			menuPanel.SetAsFirstSibling();
		}

		isThisMenuActive = false;
	}

	protected override void ActionOnClick (){
		if(menuPanel != null){
			if(!ReturnToButton.isAnyMenuActive){
				CustomCursorController.Instance.ChangeCursorToDefault();
				EnableMenu();
			}
		}
	}

	public virtual void EnableMenu(){
		DisableButton();
		if(menuPanel != null){
			menuPanel.SetAsLastSibling();
		}
		isThisMenuActive = true;
		menuSetActive();
	}
	
	public virtual void DisableMenu(){
		if(menuPanel != null){
			menuPanel.SetAsFirstSibling();
		}
		isThisMenuActive = false;
		ModalWindowHandler.Instance.Disable();
		EnableButton();
	}
}
