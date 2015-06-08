using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(DisplayUIText))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Actor : InteractiveElement {
	public const float permisiveErrorDistanceBetweenActorAndDesiredPosition = 1.5f;

	protected Vector2 currentPosition;
	protected Vector2 originalPositionToGo;
	protected Vector2 currentPositionToGo;
	protected Vector2 moveDirection = Vector2.zero;
	protected float movementSpeed = 1f; //Just in case we want an Actor walking faster

	protected bool inCutScene = false; //Maybe not needed in the near future using something like CutScenes.isPlaying();
	public bool isInteracting = false;
	public bool isWalking = false;
	public bool isSpeaking = false;
	public bool isInConversation = false;
	public bool isFacingLeft = true;
	public bool isFacingRight = false;
	//protected bool isIdle = true;
	protected bool isPlayingAnimation = false;
	protected bool endOfAnimationEvent = false;
	public bool isActorActive = true;
	
	protected DisplayTextHandler displayText;
	protected DisplayUIText displayUIText;

	protected override void InitializeInformation(){
		currentPosition = this.transform.position;
		originalPositionToGo = currentPosition;
		currentPositionToGo = originalPositionToGo;
		displayText = this.gameObject.GetComponent<DisplayTextHandler>();
		displayUIText = this.gameObject.GetComponent<DisplayUIText>();

		InitializeAdditionalActorInformation();
	}
	
	protected abstract void InitializeAdditionalActorInformation(); //Write here additional information about the current actor
																	//groupID = "NPC";
																	//...
	
	protected void Update () {
		if(moveDirection != Vector2.zero){
			float _distanceBetweenActorAndNewPosition = Mathf.Abs(Vector2.Distance(currentPosition, currentPositionToGo));
			if(_distanceBetweenActorAndNewPosition > permisiveErrorDistanceBetweenActorAndDesiredPosition){
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

	protected virtual void AdditionalUpdateInformation(){} //In case if needed to handle, for example, an idle animation depending on time

	public void LookToTheRight(){
		isFacingRight = true;
		isFacingLeft = false;
	}
	
	public void LookToTheLeft(){
		isFacingRight = false;
		isFacingLeft = true;
	}
	
	public void LookAtPosition(Vector2 position){ //To be enhanced
		if(currentPositionToGo.x > (currentPosition.x + (spriteWidth * 0.5f))){
			LookToTheRight();
		}
		else{
			LookToTheRight();
        }
    }
    
    public void GoTo(Vector2 newPosition, bool isNewPositionRelativeToActorWidth = true){

		StartCoroutine(WaitForGoToCompleted(newPosition, isNewPositionRelativeToActorWidth));
	}

	protected IEnumerator WaitForGoToCompleted(Vector2 newPosition, bool isNewPositionRelativeToActorWidth){

		if(originalPositionToGo != newPosition){
			do{
				yield return null;
			}while(isSpeaking && isInteracting &&isInConversation); //Maybe in future this would be changed to while(AnotherActionIsPlaying());
			
			//Reset moveDirection just in case it was already walking
			moveDirection = Vector2.zero;
			isWalking = false;
			
			//Now the normal process
			isWalking = true;
			
			currentPositionToGo = newPosition;
			originalPositionToGo = newPosition;

			/* To be fixed or removed
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
			*/

			if(currentPositionToGo.x < 0f){
				currentPositionToGo.x = 10f;
			}
			else if((currentPositionToGo.x + spriteWidth) > (Screen.width * 0.5f)){
				currentPositionToGo.x = (Screen.width * 0.5f - 10f);
			}

			if(currentPositionToGo.x > (currentPosition.x + spriteWidth)){
				LookToTheRight();
				currentPositionToGo.x -= spriteWidth;
			}
			else if(currentPositionToGo.x < currentPosition.x){
				LookToTheLeft();
			}
			else if(currentPositionToGo.x > currentPosition.x && currentPositionToGo.x < (currentPosition.x + spriteWidth)){
				currentPositionToGo.x -= (currentPositionToGo.x - currentPosition.x);
			}

			if(currentPositionToGo.x < 0f){
				currentPositionToGo.x = 10f;
			}
			else if((currentPositionToGo.x + spriteWidth) > (Screen.width * 0.5f)){
				currentPositionToGo.x = (Screen.width * 0.5f - 10f);
			}

			//Navigation calculation should be here
			
			moveDirection = currentPositionToGo - currentPosition;
			moveDirection.Normalize();
			moveDirection *= movementSpeed;
		}

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

		Vector2 _talkingPosition = this.transform.FindChild("dialogText").transform.position;

		displayUIText.DisplayText(groupID, nameID, stringID, _talkingPosition);

		do{
			yield return null;
		}while(!displayUIText.HasEndedDisplayingText()); 

		isSpeaking = false;
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

	public bool HasEndedSpeaking(){
		return displayUIText.HasEndedDisplayingText();
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

	public virtual bool IsIdle(){
		if(isWalking || isInteracting || isSpeaking || isInConversation){
			return false;
		}
		else{
			return true;
		}
	}
}
