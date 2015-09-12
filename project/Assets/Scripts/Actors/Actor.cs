using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(DisplayDialogueText))]
[RequireComponent(typeof(SpriteRenderer))]
public class Actor : InteractiveElement {
	public bool isInteracting { get; protected set;}
	public bool isWalking { get; protected set;}
	public bool isSpeaking { get; protected set;}
	public bool isInConversation { get; protected set;}
	public bool isFacingLeft { get; protected set;}
	public bool isFacingRight { get; protected set;}
	public bool isActorActive { get; protected set;}

	public Vector2 originalTargetedPosition{ get; protected set;}
	public Vector2 currentTargetedPosition;
	public Vector2 moveDirection = Vector2.zero;
	public Vector2 currentPosition {
		get{
			return this.transform.position;
		}
		protected set{}
	}

	[SerializeField]
	protected float movementSpeed = 1f; //Just in case we want an Actor walking faster
		
	protected Coroutine walkingCoroutine;

	protected bool isPlayingAnimation = false;
	protected bool endOfAnimationEvent = false;
	protected DisplayDialogueText dialogueHandler;
	
	public delegate void BeginPlayerConversation();
	public static event BeginPlayerConversation beginPlayerConversation;
	public delegate void EndPlayerConversation();
	public static event EndPlayerConversation endPlayerConversation;

	protected override void InitializeInformation(){
		groupID = "NPC";

		isInteracting = false;
		isWalking = false;
		isSpeaking = false;
		isInConversation = false;
		isFacingLeft = true;
		isFacingRight = false;
		isActorActive = true;
		currentPosition = this.transform.position;
		originalTargetedPosition = currentPosition;
		currentTargetedPosition = originalTargetedPosition;
		dialogueHandler = this.gameObject.GetComponent<DisplayDialogueText>();
       	
        InitializeAdditionalActorInformation();
	}

	//Write here additional information about the current actor if needed
	protected virtual void InitializeAdditionalActorInformation(){}
		
    protected virtual void Update () {
		if(moveDirection != Vector2.zero){
			float _distance = Mathf.Abs(Vector2.Distance(currentPosition, currentTargetedPosition));
			if(_distance > minDistance){
				this.transform.position = new Vector2(currentPosition.x + moveDirection.x, currentPosition.y + moveDirection.y);
				currentPosition = this.transform.position;
			}
			else{
				moveDirection = Vector2.zero;
				isWalking = false;
			}
		} 

		AdditionalUpdateInformation();
	}

	//In case if needed to handle, for example, a waiting animation depending on time
	protected virtual void AdditionalUpdateInformation(){} 

	public void LookToTheRight(){
		isFacingRight = true;
		isFacingLeft = false;
	}
	
	public void LookToTheLeft(){
		isFacingRight = false;
		isFacingLeft = true;
	}
	
	public void LookAtPosition(Vector2 position){
		if(position.x > (currentPosition.x + spriteWidth)){
			LookToTheRight();
		}
		else if(position.x < currentPosition.x){
			LookToTheLeft();
		}
		else{
			if(position.x < walkingPosition.position.x){
				LookToTheLeft();
			}
			else{
				LookToTheRight();
			}
		}
    }
    
    public virtual void GoTo(Vector2 newPosition){
		StartCoroutine(WaitForGoToCompleted(newPosition));
	}

	protected virtual IEnumerator WaitForGoToCompleted(Vector2 newPosition){
		if(originalTargetedPosition != newPosition){
			do{
				yield return null;
			}while(isSpeaking && isInteracting && isInConversation); //Maybe in future this would be changed to while(AnotherActionIsPlaying());

			if(isWalking){ //If it was already walking
				StopCoroutine(walkingCoroutine);
			}

			isWalking = true;
			moveDirection = Vector2.zero;
			walkingCoroutine = StartCoroutine(WaitUntilTargetedPositionIsReached(newPosition));
		}
	}

	protected virtual IEnumerator WaitUntilTargetedPositionIsReached(Vector2 newPosition){
		moveDirection = CalculateMoveDirection(newPosition);
		do{
			yield return null;
		}while(isWalking);
	}

	protected Vector2 CalculateMoveDirection(Vector2 newPosition){
		//Reset moveDirection just in case it was already walking
		Vector2 _newMoveDirection = Vector2.zero;
		
		//Now the normal process
		currentTargetedPosition = newPosition;
		
		if(currentTargetedPosition.x > (currentPosition.x + spriteWidth)){
			LookToTheRight();
			currentTargetedPosition.x -= spriteWidth;
		}
		else if(currentTargetedPosition.x < currentPosition.x){
			LookToTheLeft();
		}
		else if(currentTargetedPosition.x > currentPosition.x && currentTargetedPosition.x < (currentPosition.x + spriteWidth)){
			currentTargetedPosition.x -= (currentTargetedPosition.x - currentPosition.x);
		}
		else{
			if(currentTargetedPosition.x < walkingPosition.position.x){
				LookToTheLeft();
			}
			else{
				LookToTheRight();
			}
			currentTargetedPosition.x -= (currentTargetedPosition.x - currentPosition.x);
		}
		
		if(currentTargetedPosition.x < 0f){
			currentTargetedPosition.x = 10f;
		}
		else if((currentTargetedPosition.x + spriteWidth) > (Screen.width * 0.5f)){
			currentTargetedPosition.x = (Screen.width * 0.5f - 10f);
		}
	
		_newMoveDirection = currentTargetedPosition - currentPosition;
		_newMoveDirection.Normalize();
		_newMoveDirection *= movementSpeed;
		
		return _newMoveDirection;
	}

	public void Speak(List<string> textData){
		StartCoroutine(WaitForSpeakCompleted(textData[0], textData[1], textData[2]));
	}

	public void Speak(string groupID, string nameID, string stringID){
		StartCoroutine(WaitForSpeakCompleted(groupID, nameID, stringID));
	}

	public IEnumerator WaitForSpeakCompleted(string groupID, string nameID, string stringID){
		do{
			yield return null;
		}while(isInteracting || isWalking || isSpeaking);

		isSpeaking = true;

		dialogueHandler.DisplayText(groupID, nameID, stringID);
		do{
			yield return null;
		}while(!dialogueHandler.HasEndedDisplayingText()); 

		isSpeaking = false;
	}

	protected void BeginConversation(){
		isInConversation = true;
		BeginAction();
		beginPlayerConversation();
	}

	protected void EndConversation(){
		isInConversation = false;
		endPlayerConversation();
		EndAction();
	}

	public virtual bool IsIdle(){
		if(isWalking || isInteracting || isSpeaking || isInConversation){
			return false;
		}
		else{
			return true;
		}
	}
}
