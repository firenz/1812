using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarperElement : InteractiveElement {
	[SerializeField]
	protected string destinationScene;

	public enum CursorPosition{
		left,
		right,
		up,
		down
	}

	[SerializeField]
	protected CursorPosition arrowDirection = CursorPosition.left;

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

			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());

			EndAction();
			GameController.WarpToLevel(destinationScene);
		}
	}

	public override void ChangeCursorOnMouseOver(){
		switch(arrowDirection){
		case CursorPosition.left:
			CustomCursorController.Instance.ChangeCursorOverWarpElement(0f);
			break;
		case CursorPosition.right:
			CustomCursorController.Instance.ChangeCursorOverWarpElement(180f);
			break;
		case CursorPosition.up:
			CustomCursorController.Instance.ChangeCursorOverWarpElement(90f);
			break;
		case CursorPosition.down:
			CustomCursorController.Instance.ChangeCursorOverWarpElement(270f);
			break;
		default:
			CustomCursorController.Instance.ChangeCursorOverWarpElement(0f);
			break;
		}

	}
}
