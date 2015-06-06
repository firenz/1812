using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class WarperElement : InteractiveElement {
	protected string nameSceneDestination;

	//Write here the info for your interactive element
	//warpToSceneID = "NameScene";
	//...
	//protected abstract void InitializeInformation();

	protected override IEnumerator WaitForLeftClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());

		}
		
		if(Player.Instance.LastTargetedPosition() == interactivePosition){
			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());

			GameController.WarpToLevel(nameSceneDestination);
		}
	}

	public override void ChangeCursorOnMouseOver(){
		CustomCursorController.Instance.ChangeCursorOverWarpElement(180f);
	}
}
