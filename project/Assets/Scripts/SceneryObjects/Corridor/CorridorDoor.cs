using UnityEngine;
using System.Collections;

public class CorridorDoor : WarperElement {
	
	protected override void InitializeInformation(){
		groupID = "SCENE_CORRIDOR";
		nameID = "OBJECT_DOOR";

		nameSceneDestination = "DemoScene_00";
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
			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());
			
			Player.Instance.SetInteractionInactive();

			GameState.CutSceneData.isPlayedIntro = true;

			GameController.WarpToLevel(nameSceneDestination);
		}
	}

}
