using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class InteractiveElement : MonoBehaviour {
	public const float permisiveErrorBetweenPlayerPositionAndInteractivePosition = 5f;

	protected string groupID = "DEFAULT";
	protected string nameID = "DEFAULT";

	protected Vector2 interactivePosition;

	protected float spriteWidth = 0f;
	protected float spriteHeight = 0f;
	protected bool isInactive = false;

	protected Text DisplayNameText;

	// Use this for initialization
	protected void Start () {
		DisplayNameText = GameObject.Find("NameInteractiveElementText").GetComponent<Text>();

		spriteWidth = this.CalculateSpriteWidth();
		spriteHeight = this.CalculateSpriteHeight();

		try{
			GameObject _position;
			_position = this.transform.FindChild("WalkingPoint").gameObject;
			interactivePosition = _position.transform.position;
		}
		catch(NullReferenceException exception){
			interactivePosition = Vector2.zero;
		}
        
        InitializeInformation();
	}

	protected abstract void InitializeInformation(); //Write here the info for your interactive element

	public List<string> Description(){
		List<string> _definitionText = new List<string>();

		_definitionText.Add(groupID);
		_definitionText.Add(nameID);
		_definitionText.Add("DESCRIPTION");
		return _definitionText;
	}

	public virtual void LeftClickAction(){
		if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking()){
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
			Player.Instance.isDoingAction = true;
			Player.Instance.SetInteractionActive();
			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());
			
			Player.Instance.SetInteractionInactive();
			Player.Instance.isDoingAction = false;
		}
	}

	public virtual void RightClickAction(){
		if(!Player.Instance.IsInteracting() && !Player.Instance.IsSpeaking()){
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
			Player.Instance.isDoingAction = true;
			Player.Instance.SetInteractionActive();
			Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());
			
			Player.Instance.SetInteractionInactive();
			Player.Instance.isDoingAction = false;
		}
	}


	public virtual void ActionOnItemInventoryUsed(string nameItemInventory){
		/* How to use it:
		switch(nameOfItemInventary){
			case "name 1": DoSomething1....
			break;
            case "name 2": DoSomething2....
                break;
			...
        }
    	*/
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
	public string GetName(){
		return  LocalizedTextManager.GetLocalizedText(groupID, nameID, "NAME")[0];
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

	public void SetInactive(){
		isInactive = true;
		this.gameObject.GetComponent<Renderer>().enabled = false;
		this.gameObject.GetComponent<Collider2D>().enabled = false;
	}

	public void SetActive(){
		isInactive = false;
		this.gameObject.GetComponent<Renderer>().enabled = true;
		this.gameObject.GetComponent<Collider2D>().enabled = true;
	}

	public bool IsInactive(){
		return isInactive;
    }

	/*
	protected virtual void OnMouseOver(){ //Change mousegraphic depending of the type of the element to be implemented
		DisplayNameText.text = LocalizedTextManager.GetLocalizedText(groupID, nameID, "NAME")[0];
	}

	protected virtual void OnMouseExit(){
		DisplayNameText.text = "";
	}
	*/
}
