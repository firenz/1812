using UnityEngine;
using System.Collections;

public class TrashBin : InteractiveElement {
	public int timesInteracted {get; private set;}
	
	protected override void Start (){
		base.Start ();

		timesInteracted = GameState.LevelCorridorData.timesTrashBinWasExaminated;
	}

	/*
	protected override void InitializeInformation(){
		//Write here the info for your interactive object
		groupID = "SCENE_CORRIDOR";
		nameID = "OBJECT_TRASHBIN";

		timesInteracted = GameState.LevelCorridorData.timesTrashBinWasExaminated;

	}


	protected override void InitializeInformation(){
		timesInteracted = GameState.LevelCorridorData.timesTrashBinWasExaminated;
	}
	*/

	protected override IEnumerator WaitForLeftClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());
		}
		
		if(Player.Instance.LastTargetedPosition() == interactivePosition){
			BeginAction();
			//Player.Instance.SetInteractionActive();

			if(timesInteracted > 0){
				Player.Instance.Speak(groupID, nameID, "INTERACTION_2");
			}
			else{
				Player.Instance.Speak(groupID, nameID, "INTERACTION_1");
				timesInteracted++;
			}
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());

			EndAction();
			//Player.Instance.SetInteractionInactive();
		}
	}


	/*
	public int TimesInteracted(){
		return timesTrashBinHasBeenInteracted;
	}
	*/
}
