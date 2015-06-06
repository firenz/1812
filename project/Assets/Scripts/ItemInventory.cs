using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class ItemInventory : InteractiveElement {
	protected bool isSelectedByMouse = false;
	protected Vector3 originalPosition;
	protected Vector2 mouseOffset;

	protected override void InitializeInformation(){
		//Write here the info for your interactive element
		groupID = "INVENTORY";
		originalPosition = this.transform.position;
		//...
		InitializeItemInformation();
	}

	protected abstract void InitializeItemInformation(); 
	
	// Update is called once per frame
	protected void Update () {
		if(isSelectedByMouse){
			Vector2 _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 _screenMousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
			this.transform.position = new Vector3(_screenMousePosition.x + mouseOffset.x, _screenMousePosition.y + mouseOffset.y, originalPosition.z);
		}
	}
	
	public override void LeftClickAction(){}

	protected override IEnumerator WaitForRightClickAction(){
		Player.Instance.SetInteractionActive();
		Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
		
		do{
			yield return null;
		}while(Player.Instance.IsSpeaking());
		
		Player.Instance.SetInteractionInactive();
	}

	public void UpdateInfo(){
		try{
			DisplayNameText = GameObject.Find("NameInteractiveElementText").GetComponent<Text>();
		}
		catch(NullReferenceException){
			DisplayNameText =  null;
		}
	}

	public bool IsCurrentlyUsed(){
		return isSelectedByMouse;
	}

	protected virtual void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking()){
				DisplayNameText.text = "";
				if(!isSelectedByMouse){
					Cursor.visible = false; //Later when we have the custom mouse, this line is going to be deleted and replaced with another function
					isSelectedByMouse = true;
					originalPosition = this.transform.position;
					mouseOffset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
					this.GetComponent<Renderer>().sortingOrder = 2;
				}
				else{
					Cursor.visible = true; //Later when we have the custom mouse, this line is going to be deleted
					isSelectedByMouse = false;
					this.transform.position = originalPosition;
					this.GetComponent<Renderer>().sortingOrder = 1;
				}
			}
		}
	}

	public override void ChangeCursorOnMouseOver(){}

}
