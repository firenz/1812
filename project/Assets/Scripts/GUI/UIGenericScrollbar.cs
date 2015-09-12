using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Scrollbar))]
public class UIGenericScrollbar : MonoBehaviour {
	[SerializeField]
	private float valueResetPosition = 1f;

	private Scrollbar thisScrollbar;

	protected void OnEnable(){
		ModalWindowHandler.beginQuestion += Disable;
		ModalWindowHandler.endedQuestion += Enable;
		ReturnToButton.resetUIPositions += ResetPosition;
		ReturnToMainMenuButton.resetUIPositions += ResetPosition;
	}
	
	protected void OnDisable(){
		ModalWindowHandler.beginQuestion -= Disable;
		ModalWindowHandler.endedQuestion -= Enable;
		ReturnToButton.resetUIPositions -= ResetPosition;
		ReturnToMainMenuButton.resetUIPositions -= ResetPosition;
	}

	private void Start(){
		thisScrollbar = this.GetComponent<Scrollbar>();
	}

	public void OnMouseOver(){
		if(!thisScrollbar.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorOverUIElement();
		}
	}

	public void OnMouseExit(){
		if(!thisScrollbar.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorToDefault();
		}
	}

	public void Enable(){
		thisScrollbar.interactable = true;
	}

	public void Disable(){
		thisScrollbar.interactable = false;
	}

	public void ResetPosition(){
		thisScrollbar.value = valueResetPosition;
	}
}
