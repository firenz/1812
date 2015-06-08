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
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2f);
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

	/*
	protected virtual void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking()){
				DisplayNameText.text = "";
				if(!isSelectedByMouse){
					//Player.Instance.SetUsingInventoryActive();
					Debug.Log("selected: " + this.gameObject.name);
					CustomCursorController.Instance.HideCursor();
					isSelectedByMouse = true;
					originalPosition = this.transform.position;
					mouseOffset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
					this.GetComponent<Renderer>().sortingOrder = 2;
					this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -4f);
				}
				else{
					Debug.Log("unselected: " + this.gameObject.name);
					CustomCursorController.Instance.UnhideCursor();
					isSelectedByMouse = false;
					this.transform.position = originalPosition;
					this.GetComponent<Renderer>().sortingOrder = 1;
					//Player.Instance.SetUsingInventoryInactive();
				}
			}
		}
	}
	*/

	public void Select(){
		if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking() && !Player.Instance.isUsingItemInventory){
			try{
				DisplayNameText.text = "";
			}
			catch(NullReferenceException){
				DisplayNameText = null;
			}

			if(!isSelectedByMouse){
				Player.Instance.isUsingItemInventory = true;
				Debug.Log("selected: " + this.gameObject.name);
				CustomCursorController.Instance.HideCursor();
				isSelectedByMouse = true;
				originalPosition = this.transform.position;
				mouseOffset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
				this.GetComponent<Renderer>().sortingOrder = 2;
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -4f);
			}
		}
	}

	public void Unselect(){
		Debug.Log("unselected: " + this.gameObject.name);
		CustomCursorController.Instance.UnhideCursor();
		isSelectedByMouse = false;
		this.transform.position = originalPosition;
		this.GetComponent<Renderer>().sortingOrder = 1;
		Player.Instance.isUsingItemInventory = false;
	}

	public override void ChangeCursorOnMouseOver(){}

	public bool IsCurrentlyUsed(){
		return isSelectedByMouse;
	}

}
