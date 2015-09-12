using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Button))]
public abstract class UIGenericButton : MonoBehaviour {
	[SerializeField]
	protected string localizedTextGroupID = "OPTIONS_MENU";
	[SerializeField]
	protected string localizedTextElementID = "SETTINGS";
	[SerializeField]
	protected string localizedTextStringID = "NAME";
	[SerializeField]
	protected string sfxOnClick = "BlipSelect";

	protected Button thisButton;
	protected Text buttonText;

	protected virtual void OnEnable(){
		ModalWindowHandler.beginQuestion += DisableButton;
		ModalWindowHandler.endedQuestion += EnableButton;
		LocatedTextManager.currentLanguageChanged += ChangeButtonTextLanguage;
	}
	
	protected virtual void OnDisable(){
		ModalWindowHandler.beginQuestion -= DisableButton;
		ModalWindowHandler.endedQuestion -= EnableButton;
		LocatedTextManager.currentLanguageChanged -= ChangeButtonTextLanguage;
	}

	protected virtual void Start(){
		thisButton = this.GetComponent<Button>();
		try{
			buttonText = this.transform.FindChild("Text").GetComponent<Text>();
			buttonText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID)[0];
		}catch(NullReferenceException){
			buttonText = null;
		}
	}

	public virtual void OnClick(){
		if(thisButton.interactable && !CustomCursorController.Instance.isCursorHidden){
			AudioManager.PlaySFX(sfxOnClick);
			StartCoroutine(WaitForSFXAndExecuteClickAction());
		}
	}

	protected IEnumerator WaitForSFXAndExecuteClickAction(){
		do{
			yield return null;
		}while(AudioManager.IsPlayingSFX(sfxOnClick));
		ActionOnClick();
	}

	protected abstract void ActionOnClick();

	public virtual void OnMouseOver(){
		if(thisButton.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorOverUIElement();
		}
	}

	public virtual void OnMouseExit(){
		if(thisButton.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorToDefault();
		}
	}

	public virtual void EnableButton(){
		thisButton.interactable = true;
	}
	
	public virtual void DisableButton(){
		thisButton.interactable = false;
	}

	protected virtual void ChangeButtonTextLanguage(){
		if(buttonText != null){
			buttonText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID)[0];
		}
	}
}
