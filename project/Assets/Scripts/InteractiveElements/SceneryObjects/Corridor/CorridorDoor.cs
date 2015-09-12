using UnityEngine;
using System.Collections;

public sealed class CorridorDoor : WarperElement {
	private string nameDoorSFX = "OpenWindow_v2";

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
			
			Player.Instance.LookAtPosition(this.transform.position);
			yield return new WaitForSeconds(0.1f);
			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			AudioManager.PlaySFX(nameDoorSFX);
			do{
				yield return null;
			}while(AudioManager.IsPlayingSFX(nameDoorSFX));

			EndAction();

			GameController.WarpToLevel(destinationScene);

		}
	}
}
