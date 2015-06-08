using UnityEngine;
using System.Collections;

public class TrashBin : InteractiveElement {
	public int timesTrashBinHasBeenInteracted = 0;
	
	protected override void InitializeInformation(){
		//Write here the info for your interactive object
		groupID = "SCENE_CORRIDOR";
		nameID = "OBJECT_TRASHBIN";

		timesTrashBinHasBeenInteracted = GameState.LevelCorridorData.timesTrashBinWasExaminated;

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
			Player.Instance.SetInteractionActive();
			if(timesTrashBinHasBeenInteracted > 0){
				Player.Instance.Speak(groupID, nameID, "INTERACTION_2");
			}
			else{
				Player.Instance.Speak(groupID, nameID, "INTERACTION_1");
				timesTrashBinHasBeenInteracted++;
			}
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());
			
			Player.Instance.SetInteractionInactive();
		}
	}



	public int TimesInteracted(){
		return timesTrashBinHasBeenInteracted;
	}
}
