using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ItemInventory : InteractiveElement {
	protected bool isSelectedByMouse = false;
	protected Vector2 originalPosition;
	protected Vector2 mouseOffset;

	protected override void InitializeInformation(){
		//Write here the info for your interactive element
		groupID = "INVENTORY";
		//...
		InitializeItemInformation();
	}

	protected abstract void InitializeItemInformation(); 
	
	// Update is called once per frame
	protected void Update () {
		if(isSelectedByMouse){
			Vector2 _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 _screenMousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
			this.transform.position = new Vector2(_screenMousePosition.x + mouseOffset.x, _screenMousePosition.y + mouseOffset.y);
		}
	}

	/*
	public override void RightClickAction(){
		if(!isSelectedByMouse){
			Player.Instance = GameObject.Find("Player").GetComponent<PlayerB>();
			if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking() && !Player.Instance.IsWalking()){
				Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
			}
		}
		//else{
		//	Screen.showCursor = true; //Later when we have the custom mouse, this line is going to be deleted
		//	isSelectedByMouse = false;
		//	this.transform.position = originalPosition;
		//}
	}
	*/

	
	public override void LeftClickAction(){
		/*
		Debug.Log("LeftClick");
		Player.Instance = GameObject.Find("Player").GetComponent<PlayerB>();

		if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking()){
			if(!isSelectedByMouse){
				Player.Instance.SetUsingInventoryActive();
				Screen.showCursor = false; //Later when we have the custom mouse, this line is going to be deleted and replaced with another function
				isSelectedByMouse = true;
				originalPosition = this.transform.position;
				mouseOffset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
			}
			else{
				Player.Instance.SetUsingInventoryInactive();
				Screen.showCursor = true; //Later when we have the custom mouse, this line is going to be deleted
				isSelectedByMouse = false;
				this.transform.position = originalPosition;
			}
		}
		*/
	}

	protected override IEnumerator WaitForRightClickAction(){
		Player.Instance.SetInteractionActive();
		Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
		
		do{
			yield return null;
		}while(Player.Instance.IsSpeaking());
		
		Player.Instance.SetInteractionInactive();
	}

	public bool IsCurrentlyUsed(){
		return isSelectedByMouse;
	}

	protected virtual void OnMouseOver(){
		//if(!isSelectedByMouse){
		//	DisplayNameText.text = LocalizedTextManager.GetLocalizedText(groupID, nameID, "NAME")[0];
		//}

		if(Input.GetMouseButtonDown(0)){
			if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking()){
				DisplayNameText.text = "";
				if(!isSelectedByMouse){
					//DisplayNameText.text = "";
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

}
