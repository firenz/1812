using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class InteractiveElement : MonoBehaviour {
	public Vector2 interactivePosition { 
		get{
			if(walkingPosition != null){
				return walkingPosition.position;
			}
			else{
				return Vector2.zero;
			}
		} 
		protected set{}
	}
	public float spriteWidth { get; protected set;}
	public float spriteHeight { get; protected set;}
	public string displayName { get; protected set;}

	[SerializeField]
	public bool isInactive { get; protected set;}
	[SerializeField]
	protected string groupID = "GUI";
	[SerializeField]
	protected string nameID = "DEFAULT";
	[SerializeField]
	protected float minDistance = 1.5f;

	protected SpriteRenderer thisSpriteRenderer = null;
	protected Transform walkingPosition;

	public delegate void BeginPlayerAction();
	public static event BeginPlayerAction beginPlayerAction;
	public delegate void EndPlayerAction();
	public static event EndPlayerAction endPlayerAction;
	
	protected virtual void Start () {
		isInactive = false;
		displayName = LocatedTextManager.GetLocatedText(groupID, nameID, "NAME")[0];

		try{
			walkingPosition = this.transform.FindChild("WalkingPoint");
		}
		catch(NullReferenceException){
			walkingPosition = null;
        }

		if(walkingPosition != null){
			try{
				walkingPosition.GetComponent<SpriteRenderer>().enabled = false;
			}catch(NullReferenceException){}
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

		InitializeInformation();
	}

	//Write here the extra info for your interactive element
	protected virtual void InitializeInformation(){}

	public virtual void LeftClickAction(){
		if(!Player.Instance.isDoingAction && !CutScenesManager.IsPlaying()){
			StartCoroutine(WaitForLeftClickAction());
		}
	}

	protected virtual IEnumerator WaitForLeftClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.currentPosition, interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= minDistance){
			Player.Instance.GoTo(interactivePosition);
			do{
				yield return null;
			}while(Player.Instance.isWalking);
		}

		if(Player.Instance.originalTargetedPosition == interactivePosition){
			BeginAction();

			Player.Instance.LookAtPosition(this.transform.position);
			yield return new WaitForSeconds(0.1f);
			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			EndAction();
		}
	}

	public virtual void RightClickAction(){
		if(!Player.Instance.isDoingAction && !CutScenesManager.IsPlaying()){
			StartCoroutine(WaitForRightClickAction());
		}
	}

	protected virtual IEnumerator WaitForRightClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.currentPosition, interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= minDistance){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.isWalking);
		}
		
		if(Player.Instance.originalTargetedPosition == interactivePosition){
			BeginAction();

			Player.Instance.LookAtPosition(this.transform.position);
			yield return new WaitForSeconds(0.1f);
			Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			EndAction();
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
    
	//In case sprite width need to be recalculated
	protected float CalculateSpriteWidth(){
		SpriteRenderer _thisSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

		if(_thisSpriteRenderer == null){
			return 0f;
		}
		else{
			return _thisSpriteRenderer.bounds.size.x;
		}
	}

	//In case sprite height need to be recalculated
	protected float CalculateSpriteHeight(){
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
}
