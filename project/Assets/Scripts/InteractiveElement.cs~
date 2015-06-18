using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class InteractiveElement : MonoBehaviour {
	public Vector2 interactivePosition { get; protected set;}
	public bool isInactive { get; protected set;}
	public float spriteWidth { get; protected set;}
	public float spriteHeight { get; protected set;}
	public string displayName { get; protected set;}

	[SerializeField]
	protected string groupID = "GUI";
	[SerializeField]
	protected string nameID = "DEFAULT";
	[SerializeField]
	protected const float permisiveErrorBetweenPlayerPositionAndInteractivePosition = 5f;
	protected SpriteRenderer thisSpriteRenderer = null;

	public delegate void BeginPlayerAction();
	public static event BeginPlayerAction beginPlayerAction;
	public delegate void EndPlayerAction();
	public static event EndPlayerAction endPlayerAction;

	// Use this for initialization
	protected virtual void Start () {
		isInactive = false;
		displayName = LocalizedTextManager.GetLocalizedText(groupID, nameID, "NAME")[0];

		try{
			interactivePosition = this.transform.FindChild("WalkingPoint").position;
		}
		catch(NullReferenceException){
			interactivePosition = Vector2.zero;
        }

		thisSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		if(thisSpriteRenderer != null){
			spriteWidth = this.CalculateSpriteWidth();
			spriteHeight = this.CalculateSpriteHeight();
		}
		else{
			spriteWidth = 0f;
            spriteHeight = 0f;
        }

        //InitializeInformation();
	}

	protected virtual void InitializeInformation(){} //Write here the info for your interactive element

	public List<string> Description(){
		List<string> _definitionText = new List<string>();

		_definitionText.Add(groupID);
		_definitionText.Add(nameID);
		_definitionText.Add("DESCRIPTION");
		return _definitionText;
	}

	public virtual void LeftClickAction(){
		//if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking() && !Player.Instance.IsInConversation()){
		if(!Player.Instance.isDoingAction && !CutScenesManager.IsPlaying()){
			StartCoroutine(WaitForLeftClickAction());
		}
	}

	protected virtual IEnumerator WaitForLeftClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());
		}

		if(Player.Instance.LastTargetedPosition() == interactivePosition){
			//Player.Instance.isDoingAction = true;
			BeginAction();

			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());

			EndAction();
			//Player.Instance.isDoingAction = false;
		}
	}

	public virtual void RightClickAction(){
		//if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking() && !Player.Instance.IsInConversation()){
		if(!Player.Instance.isDoingAction && !CutScenesManager.IsPlaying()){
			StartCoroutine(WaitForRightClickAction());
		}
	}

	protected virtual IEnumerator WaitForRightClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());
		}
		
		if(Player.Instance.LastTargetedPosition() == interactivePosition){
			//Player.Instance.isDoingAction = true;
			BeginAction();

			Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());

			EndAction();
			//Player.Instance.isDoingAction = false;
		}
	}


	public virtual void ActionOnItemInventoryUsed(GameObject itemInventory){
		/* How to use it:
		if(!Player.Instance.isDoingAction && !CutScenesManager.IsPlaying()){
			switch(itemInventory.name){
				case "name 1": 
					itemInventory.GetComponent<ItemInventory>().Unselect();
					DoSomething1....
					break;
	            case "name 2": 
					itemInventory.GetComponent<ItemInventory>().Unselect();
					DoSomething2....
	                break;
				...
	        }
        }
    	*/
	}

	public virtual void ChangeCursorOnMouseOver(){
		CustomCursorController.Instance.ChangeCursorOverInteractiveElement();
	}
    
	protected float CalculateSpriteWidth(){ //In case sprite width need to be recalculated
		SpriteRenderer _thisSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

		if(_thisSpriteRenderer == null){
			return 0f;
		}
		else{
			return _thisSpriteRenderer.bounds.size.x;
		}
	}

	protected float CalculateSpriteHeight(){ //In case sprite height need to be recalculated
		SpriteRenderer _thisSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

		if(_thisSpriteRenderer == null){
			return 0f;
		}
		else{
			return _thisSpriteRenderer.bounds.size.x;
		}
	}

	public void SetInactive(){
		isInactive = true;
		this.gameObject.GetComponent<Collider2D>().enabled = false;
		if(thisSpriteRenderer != null){
			this.gameObject.GetComponent<Renderer>().enabled = false;
		}
	}

	public void SetActive(){
		isInactive = false;
		this.gameObject.GetComponent<Collider2D>().enabled = true;
		if(thisSpriteRenderer != null){
			this.gameObject.GetComponent<Renderer>().enabled = true;
		}
	}

	protected void BeginAction(){
		beginPlayerAction();
	}

	protected void EndAction(){
		endPlayerAction();
	}

	/*
	public string GetName(){
		//Debug.Log( "GetName: Element values " + groupID + " " + nameID);
		//return  LocalizedTextManager.GetLocalizedText(groupID, nameID, "NAME")[0];
		return displayName;
	}


	public Vector2 GetPosition(){
		return interactivePosition;
	}
	
	public float GetWidth(){
		return spriteWidth;
	}
	
	public float GetHeight(){
		return spriteHeight;
	}

	public bool IsInactive(){
		return isInactive;
    }
    */

}
