using UnityEngine;
using System.Collections;

public class Skull : InteractiveElement {
	
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
			yield return new WaitForSeconds(0.1f);
			Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
			
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);
			
			EndAction();
		}
	}
}
