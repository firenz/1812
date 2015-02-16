using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Librarian : Actor {
	public static Librarian instance{ get; private set;}

	public const float maxTimeIdleUntilCoughingAnimation = 8f;

	private bool isCoughing = false;
	private float timeCounterUntilCoughingAnimation;

	
	private void OnDestroy() {
		if (instance == this) {
			instance = null;
		}
	}
	
	private void OnApplicationQuit(){
		instance = null;
	}
	
	private void Awake(){
		if(instance == null){
			instance = this;
		}
		else{
			Destroy(this.gameObject);
		}
	}
	
	protected override void InitializeAdditionalActorInformation(){
		groupID = "NPC";
		nameID = "LIBRARIAN";
		timeCounterUntilCoughingAnimation = Time.time;

		if(!GameState.CutSceneData.isPlayedIntro){
			SetInactive();
		}
	}
	
	protected override void AdditionalUpdateInformation(){ //In case if needed to handle, for example, an idle animation depending on time
		if(!isInactive && isIdle && !isInteracting && !CutScenesManager.IsPlaying() && !isCoughing){
			if((Time.time - timeCounterUntilCoughingAnimation) > maxTimeIdleUntilCoughingAnimation){
				isCoughing = true;
				isIdle = false;
				timeCounterUntilCoughingAnimation = Time.time;
			}
		}
		else{
			timeCounterUntilCoughingAnimation = Time.time;
		}
	}

	protected override IEnumerator WaitForRightClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());
		}
		
		if(Player.Instance.LastTargetedPosition() == interactivePosition){
			Player.Instance.SetInteractionActive();
			isInteracting = true;

			if(Player.Instance.transform.position.x < (interactivePosition.x + spriteWidth * 0.5f)){
				Player.Instance.LookToTheRight();
			}
			else{
				Player.Instance.LookToTheLeft();
			}

			Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());

			isInteracting = false;
			Player.Instance.SetInteractionInactive();
		}
	}	
	
	protected override IEnumerator WaitForLeftClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());
		}
		
		if(Player.Instance.LastTargetedPosition() == interactivePosition){
			Player.Instance.LookToTheRight();
			yield return new WaitForSeconds(0.2f);
			
			if(isCoughing){
				Player.Instance.Speak(groupID, nameID, "NPC_IS_BUSY");
				
				do{
					yield return null;
				}while(Player.Instance.IsSpeaking());
			}
			else{
				Player.Instance.SetInteractionActive();
				isInteracting = true;

				Player.Instance.Speak(groupID, nameID, "PLAYER_CONVERSATION_01");

				do{
					yield return null;
				}while(Player.Instance.IsSpeaking());
				
				this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_01");
				
				do{
					yield return null;
				}while(isSpeaking);
				
				isInteracting = false;
				Player.Instance.SetInteractionInactive();
			}
		}
	}

	public bool IsCoughing(){
		return isCoughing;
	}

	private void CoughingEventAnimation(){
		Vector2 textPosition = this.transform.FindChild("dialogText").transform.position;
		displayText.DisplayText(groupID, nameID, "COUGH", textPosition, false);
	}
	
	private void CoughingEventEnds(){
		isCoughing = false;
		isIdle = true;
	}
}
