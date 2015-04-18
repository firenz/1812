using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class PickableElement : InteractiveElement {
	protected List<string> nameListGivableElements;
	protected bool isDestroyedAfterBeingPicked = true;

	public enum positionTypes{
		upper,
		bottom
	}

	protected positionTypes currentPositionType;

	protected override void InitializeInformation(){
		nameListGivableElements = new List<string>();

		if(isInactive){
			this.gameObject.GetComponent<Renderer>().enabled = false;
		}

		InitializePickableInformation();
	}

	protected abstract void InitializePickableInformation(); //Here the information about the pickable object

	public override void LeftClickAction(){
		if(!isInactive && !Player.Instance.IsSpeaking() && !Player.Instance.IsInteracting()){
			StartCoroutine(ManipulatingObject());
		}
	}

	protected virtual IEnumerator ManipulatingObject(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());
		}

		if(Player.Instance.LastTargetedPosition() == interactivePosition){
			Player.Instance.isDoingAction = true;

			Player.Instance.Speak(groupID, nameID, "INTERACTION");

			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());

			//Player.Instance.SetInteractionActive();
			yield return new WaitForSeconds(0.2f);

			/*
			if(currentPositionType == positionTypes.upper){
				//Player.Instance.SetUpperInteractionActive();
				Player.Instance.UpperInteraction(this);			
				//Player.Instance.SetUpperInteractionInactive();
			}
			else if(currentPositionType == positionTypes.bottom){
				//Player.Instance.SetBottomInteractionActive();
				Player.Instance.BottomInteraction(this);			
				//Player.Instance.SetBottomInteractionInactive();
			}
			*/
			Debug.Log("Before Interaction");
			Player.Instance.Manipulate(this);
			Debug.Log("After Interaction");

			yield return new WaitForSeconds(0.2f);

			Player.Instance.Speak(this.GrabbedObjectsDescription());

			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());
			//...
			
			//Player.Instance.SetInteractionInactive();

			yield return new WaitForSeconds(0.1f);

			Player.Instance.isDoingAction = false;
		}

	}

	public virtual void OnPlayerTouchingAction(){
		Player.Instance.GrabItem(nameListGivableElements);
		SetInactive();
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

	public positionTypes PositionType(){
		return currentPositionType;
	}

	public bool IsDestroyedAfterBeingPicked(){
		return isDestroyedAfterBeingPicked;
	}
	
}
