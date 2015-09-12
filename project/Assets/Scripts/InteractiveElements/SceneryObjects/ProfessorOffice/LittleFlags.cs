using UnityEngine;
using System.Collections;

public sealed class LittleFlags : PickableElement {

	private void OnEnable(){
		Map.activateLittleFlags += ActivateLittleFlags;
	}

	private void OnDisable(){
		Map.activateLittleFlags -= ActivateLittleFlags;
	}

	protected override void InitializePickableInformation (){
		if(!GameState.LevelProfessorOfficeData.isLittleFlagsFellIntoFloor){
			SetInactive();
		}
		else{
			if(GameState.LevelProfessorOfficeData.isLittleFlagsPickedFromFloor){
				SetInactive();
			}
		}
	}

	protected override IEnumerator WaitForLeftClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.currentPosition, interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= minDistance){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.isWalking);
		}
		
		if(Player.Instance.originalTargetedPosition == interactivePosition){
			BeginAction();
			
			Player.Instance.LookToTheLeft();
			yield return new WaitForSeconds(0.1f);

			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);
			yield return new WaitForSeconds(0.1f);

			Player.Instance.Manipulate(this);

			EndAction();
		}
	}

	public override void OnPlayerTouchingAction (){
		if(Inventory.Instance.IsInventoryFull()){
			Player.Instance.Speak("GUI", "DEFAULT", "FULL_INVENTORY");
		}
		else{
			Player.Instance.GrabItem(nameListGivableElements);
            SetInactive();
			GameState.LevelProfessorOfficeData.isLittleFlagsPickedFromFloor = true;
        }
	}

	private void ActivateLittleFlags(){
		SetActive();
	}
}
