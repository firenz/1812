using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInventory : InteractiveElement {
	[SerializeField]
	protected const float positionZWhenSelected = -4f;

	public bool isSelectedByMouse { get; protected set;}

	protected Vector3 originalPosition;
	protected Vector2 mouseOffset;

	public delegate void BeginPlayerUsingItemInventory();
	public static event BeginPlayerUsingItemInventory beginUsingItem;
	public delegate void EndPlayerUsingItemInventory();
	public static event EndPlayerUsingItemInventory endUsingItem;

	protected override void Start (){
		base.Start ();
		
		groupID = "INVENTORY";
		originalPosition = this.transform.position;
		isSelectedByMouse = false;
	}

	protected virtual void InitializeItemInformation(){} 

	protected void Update () {
		if(isSelectedByMouse){
			Vector2 _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 _screenMousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
			this.transform.position = new Vector3(_screenMousePosition.x + mouseOffset.x, _screenMousePosition.y + mouseOffset.y, this.transform.position.z);
		}
	}
	
	public override void LeftClickAction(){}

	protected override IEnumerator WaitForRightClickAction(){
		BeginAction();

		Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
		
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);

		EndAction();
	}


	public void UpdateInfo(){
		displayName = LocatedTextManager.GetLocatedText(groupID, nameID, "NAME")[0];
	}

	public void Select(){
		if(!Player.Instance.isDoingAction && !Player.Instance.isUsingItemInventory && !CutScenesManager.IsPlaying()){
			if(!isSelectedByMouse){
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
		}
	}

	protected void BeginUsingItem(){
		beginUsingItem();
	}

	protected void EndUsingItem(){
		endUsingItem();
	}
}
