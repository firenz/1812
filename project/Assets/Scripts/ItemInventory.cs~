using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ItemInventory : InteractiveElement {
	public bool isSelectedByMouse { get; protected set;}

	protected Vector3 originalPosition;
	protected Vector2 mouseOffset;
	protected const float positionZWhenSelected = 4f;

	public delegate void BeginPlayerUsingItemInventory();
	public static event BeginPlayerUsingItemInventory beginUsingItem;
	public delegate void EndPlayerUsingItemInventory();
	public static event EndPlayerUsingItemInventory endUsingItem;

	protected override void Start (){
		base.Start ();
		
		groupID = "INVENTORY";
		//this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		originalPosition = this.transform.position;
		isSelectedByMouse = false;
	}

	/*
	protected override void InitializeInformation(){
		//Write here the info for your interactive element
		groupID = "INVENTORY";
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2f);
		originalPosition = this.transform.position;
		//...
		InitializeItemInformation();
	}
	*/
	protected virtual void InitializeItemInformation(){} 

	
	// Update is called once per frame
	protected void Update () {
		if(isSelectedByMouse){
			Vector2 _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 _screenMousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
			this.transform.position = new Vector3(_screenMousePosition.x + mouseOffset.x, _screenMousePosition.y + mouseOffset.y, this.transform.position.z);
		}
	}
	
	public override void LeftClickAction(){}

	protected override IEnumerator WaitForRightClickAction(){
		//Player.Instance.SetInteractionActive();
		BeginAction();

		Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
		
		do{
			yield return null;
		}while(Player.Instance.IsSpeaking());

		EndAction();
		//Player.Instance.SetInteractionInactive();
	}


	public void UpdateInfo(){
		displayName = LocalizedTextManager.GetLocalizedText(groupID, nameID, "NAME")[0];
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
		if(!Player.Instance.isDoingAction && !Player.Instance.isUsingItemInventory && !CutScenesManager.IsPlaying()){
			if(!isSelectedByMouse){
				//Player.Instance.isUsingItemInventory = true;
				BeginUsingItem();
				CustomCursorController.Instance.HideCursor();
				isSelectedByMouse = true;
				originalPosition = this.transform.position;
				mouseOffset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
				this.GetComponent<Renderer>().sortingOrder = 2;
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, positionZWhenSelected);
			}
		}
	}

	public void Deselect(){
		if(isSelectedByMouse){
			CustomCursorController.Instance.UnhideCursor();
			isSelectedByMouse = false;
			this.transform.position = originalPosition;
			this.GetComponent<Renderer>().sortingOrder = 1;
			
			EndUsingItem();
            //Player.Instance.isUsingItemInventory = false;
		}
	}

	protected void BeginUsingItem(){
		beginUsingItem();
	}

	protected void EndUsingItem(){
		endUsingItem();
	}

	/*

	public override void ChangeCursorOnMouseOver(){
		Debug.Log("change cursor");
		CustomCursorController.Instance.ChangeCursorInteractiveElement();
	}
	
	public bool IsCurrentlyUsed(){
		return isSelectedByMouse;
	}
	*/
}
