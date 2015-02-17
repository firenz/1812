using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class PickableElement : InteractiveElement {
	protected List<string> nameListGivableElements;
	protected bool isDestroyedAfterBeingPicked = true;

	public enum pickableTypes{
		upper,
		bottom
	}

	protected pickableTypes currentPickableType;

	protected override void InitializeInformation(){
		nameListGivableElements = new List<string>();

		if(isInactive){
			this.gameObject.renderer.enabled = false;
		}

		InitializePickableInformation();
	}

	protected abstract void InitializePickableInformation(); //Here the information about the pickable object

	public override void LeftClickAction(){
		if(!isInactive && !Player.Instance.IsSpeaking() && !Player.Instance.IsInteracting()){
			StartCoroutine(PickingObject());
		}
	}

	protected virtual IEnumerator PickingObject(){
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

			Player.Instance.SetInteractionActive();

			if(currentPickableType == pickableTypes.upper){
				Player.Instance.SetUpperInteractionActive();
				Player.Instance.GrabUpperItem(this);			
				Player.Instance.SetUpperInteractionInactive();
			}
			else if(currentPickableType == pickableTypes.bottom){
				Player.Instance.SetBottomInteractionActive();
				Player.Instance.GrabBottomItem(this);			
				Player.Instance.SetBottomInteractionInactive();
			}

			yield return new WaitForSeconds(0.2f);

			Player.Instance.Speak(this.GrabbedObjectsDescription());

			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());
			//...
			
			Player.Instance.SetInteractionInactive();

			yield return new WaitForSeconds(0.1f);
		}

	}

	public List<string> NameListPickableObjects(){
		return nameListGivableElements;
	}

	public List<string> GrabbedObjectsDescription(){
		List<string> _definitionText = new List<string>();
		
		_definitionText.Add(groupID);
		_definitionText.Add(nameID);
		_definitionText.Add("PICKED");
		
		return _definitionText;
	}

	public bool IsDestroyedAfterBeingPicked(){
		return isDestroyedAfterBeingPicked;
	}
	
}
