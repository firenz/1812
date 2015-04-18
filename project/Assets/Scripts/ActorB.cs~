using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(DisplayTextHandler))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Actor : InteractiveElement {
	public const float permisiveErrorDistanceBetweenActorAndDesiredPosition = 1.5f;

	protected Vector2 currentPosition;
	protected Vector2 originalPositionToGo;
	protected Vector2 currentPositionToGo;
	protected Vector2 moveDirection = Vector2.zero;
	protected float movementSpeed = 1f; //Just in case we want an Actor walking faster

	protected bool inCutScene = false; //Maybe not needed in the near future using something like CutScenes.isPlaying();
	protected bool isInteracting = false;
	protected bool isWalking = false;
	protected bool isSpeaking = false;
	protected bool isInConversation = false;
	protected bool isFacingLeft = true;
	protected bool isFacingRight = false;
	protected bool isIdle = true;
	protected bool isPlayingAnimation = false;
	protected bool endOfAnimationEvent = false;
	protected bool isActorActive = true;
	
	protected DisplayTextHandler displayText;

	protected override void InitializeInformation(){
		currentPosition = this.transform.position;
		originalPositionToGo = currentPosition;
		currentPositionToGo = originalPositionToGo;
		displayText = this.gameObject.GetComponent<DisplayTextHandler>();

		InitializeAdditionalActorInformation();
	}
	
	protected abstract void InitializeAdditionalActorInformation(); //Write here additional information about the current actor
																	//groupID = "NPC";
																	//...
	
	protected void Update () {
		if(moveDirection != Vector2.zero){
			float _distanceBetweenActorAndNewPosition = Mathf.Abs(Vector2.Distance(currentPosition, currentPositionToGo));
			if(_distanceBetweenActorAndNewPosition > permisiveErrorDistanceBetweenActorAndDesiredPosition){
				LookAtPosition(currentPosition);
				this.transform.position = new Vector2(currentPosition.x + moveDirection.x, currentPosition.y + moveDirection.y);
				currentPosition = this.transform.position;
			}
			else{
				moveDirection = Vector2.zero;
				isWalking = false;
				isIdle = true;
			}
		}

		AdditionalUpdateInformation();
	}

	protected abstract void AdditionalUpdateInformation(); //In case if needed to handle, for example, an idle animation depending on time

	public void LookToTheRight(){
		isFacingRight = true;
		isFacingLeft = false;
	}
	
	public void LookToTheLeft(){
		isFacingRight = false;
		isFacingLeft = true;
	}
	
	public void LookAtPosition(Vector2 position){ //To be enhanced
		if(currentPosition.x > currentPositionToGo.x){
			this.LookToTheLeft();
		}
		else{
			this.LookToTheRight();
        }
    }
    
    public void GoTo(Vector2 newPosition, bool isNewPositionRelativeToActorWidth = true){

		StartCoroutine(WaitForGoToCompleted(newPosition, isNewPositionRelativeToActorWidth));
	}

	protected IEnumerator WaitForGoToCompleted(Vector2 newPosition, bool isNewPositionRelativeToActorWidth){
		do{
			yield return null;
		}while(isSpeaking && isInteracting &&isInConversation); //Maybe in future this would be changed to while(AnotherActionIsPlaying());

		//Reset moveDirection just in case it was already walking
		moveDirection = Vector2.zero;
		isWalking = false;
		isIdle = true;

		//Now the normal process
		isIdle = false;
		isWalking = true;

		currentPositionToGo = newPosition;
		originalPositionToGo = newPosition;

		if(isNewPositionRelativeToActorWidth){
			//if(currentPositionToGo.x > (currentPosition.x + spriteWidth)){
			if(currentPositionToGo.x > currentPosition.x){
				currentPositionToGo.x -= spriteWidth;
			}
		}

		if((currentPositionToGo.x + spriteWidth) > (Screen.width * 0.5f)){
			currentPositionToGo.x -= spriteWidth;
		}


		LookAtPosition(currentPositionToGo);


		//Navigation calculation should be here

		moveDirection = currentPositionToGo - currentPosition;
		moveDirection.Normalize();
		moveDirection *= movementSpeed;

	}

	public void Speak(List<string> textData){
		StartCoroutine(WaitForSpeakCompleted(textData[0], textData[1], textData[2]));
	}

	public void Speak(string groupID, string nameID, string stringID){
		StartCoroutine(WaitForSpeakCompleted(groupID, nameID, stringID));
	}

	protected IEnumerator WaitForSpeakCompleted(string groupID, string nameID, string stringID){
		do{
			yield return null;
		}while(!isIdle);

		isIdle = false;
		isSpeaking = true;
		//Debug.Log("isSpeaking " + isSpeaking.ToString());
		Vector2 _talkingPosition = this.transform.FindChild("dialogText").transform.position;
		displayText.DisplayText(groupID, nameID, stringID, _talkingPosition);
		do{
			yield return null;
		}while(!displayText.HasEndedDisplayingText()); //To be changed

		isSpeaking = false;
		isIdle = true;
	}
	
	public Vector2 CurrentPosition(){
		return currentPosition;
	}

	public Vector2 LastTargetedPosition(){
		return originalPositionToGo;
	}
	
	public bool IsSpeaking(){
		return isSpeaking;
	}

	public bool IsWalking(){
		return isWalking;
	}

	public bool IsInteracting(){
		return isInteracting;
	}

	public bool IsInConversation(){
		return isInConversation;
	}

	public bool IsFacingLeft(){
		return isFacingLeft;
	}

	public bool IsFacingRight(){
		return isFacingRight;
	}

	public bool IsIdle(){
		return isIdle;
	}

}
