using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class UIGenericSlider : MonoBehaviour {
	[SerializeField]
	protected string sfxOnValueChange = "BlipSelect";

	protected Slider thisSlider;

	protected virtual void OnEnable(){
		ModalWindowHandler.beginQuestion += DisableSlider;
		ModalWindowHandler.endedQuestion += EnableSlider;
	}

	protected virtual void OnDisable(){
		ModalWindowHandler.beginQuestion -= DisableSlider;
		ModalWindowHandler.endedQuestion -= EnableSlider;
	}
	
	private void Awake () {
		thisSlider = this.GetComponent<Slider>();
	}

	public virtual void OnValueChange(){
		if( thisSlider.interactable && !CustomCursorController.Instance.isCursorHidden){
			AudioManager.PlaySFX(sfxOnValueChange);
			ActionOnValueChange();
		}
	}

	protected abstract void ActionOnValueChange();

	public virtual void OnMouseOver(){
		if(thisSlider.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorOverUIElement();
		}
	}
	
	public virtual void OnMouseExit(){
		if(thisSlider.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorToDefault();
		}
	}
	
	protected virtual void DisableSlider(){
		thisSlider.interactable = false;
	}

	protected virtual void EnableSlider(){
		thisSlider.interactable = true;
	}
}
