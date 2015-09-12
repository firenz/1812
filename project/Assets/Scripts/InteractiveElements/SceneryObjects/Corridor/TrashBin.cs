using UnityEngine;
using System.Collections;

public sealed class TrashBin : InteractiveElement {
	public int timesInteracted {get; private set;}

	[SerializeField]
	private int valueDialogueChanges = 0;

	protected override void InitializeInformation() {}

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
			if(GameState.LevelCorridorData.timesTrashBinWasExaminated > valueDialogueChanges){
				Player.Instance.Speak(groupID, nameID, "INTERACTION_2");
			}
			else{
				Player.Instance.Speak(groupID, nameID, "INTERACTION_1");
				GameState.LevelCorridorData.timesTrashBinWasExaminated++;
			}
			
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			EndAction();
		}
	}

	protected override IEnumerator WaitForRightClickAction(){
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
			Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
			
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);
			
			EndAction();
		}
	}
}
