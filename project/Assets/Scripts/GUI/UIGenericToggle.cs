using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Toggle))]
public abstract class UIGenericToggle : MonoBehaviour {
	[SerializeField]
	protected string localizedTextGroupID = "OPTIONS_MENU";
	[SerializeField]
	protected string localizedTextElementID = "SETTINGS";
	[SerializeField]
	protected string localizedTextStringID = "NAME";
	[SerializeField]
	protected string sfxOnClick = "BlipSelect";
	
	protected Toggle thisToggle;
	protected Text labelText;

	private void OnEnable(){
		ModalWindowHandler.beginQuestion += DisableToggle;
		ModalWindowHandler.endedQuestion += EnableToggle;
		LocatedTextManager.currentLanguageChanged += ChangeToggleLabelLanguage;
	}
	
	private void OnDisable(){
		ModalWindowHandler.beginQuestion -= DisableToggle;
		ModalWindowHandler.endedQuestion -= EnableToggle;
		LocatedTextManager.currentLanguageChanged -= ChangeToggleLabelLanguage;
	}
	
	protected virtual void Start () {
		 thisToggle = this.GetComponent<Toggle>();
		try{
			labelText = this.transform.FindChild("Label").GetComponent<Text>();
			labelText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID)[0];
		}catch(NullReferenceException){
			labelText = null;
		}
	}

	public virtual void OnClick(){
		if(thisToggle.interactable && !CustomCursorController.Instance.isCursorHidden){
			AudioManager.PlaySFX(sfxOnClick);
			if(thisToggle.isOn){
				SetOn();
			}
			else{
				SetOff();
			}
		}
	}

	protected abstract void SetOn();

	protected abstract void SetOff();
	
	public virtual void OnMouseOver(){
		if(thisToggle.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorOverUIElement();
		}
	}
	
	public virtual void OnMouseExit(){
		if(thisToggle.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorToDefault();
		}
	}
	
	public void EnableToggle(){
		thisToggle.interactable = true;
	}
	
	public void DisableToggle(){
		thisToggle.interactable = false;
	}
	
	private void ChangeToggleLabelLanguage(){
		if(labelText != null){
			labelText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID)[0];
		}
	}
}
