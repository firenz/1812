using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class PickableElement : InteractiveElement {
	public bool isDestroyedAfterBeingPicked {get; protected set;}

	public enum PickableFrom{
		down,
		up
	}
	
	public PickableFrom currentPickablePosition {get; protected set;}

	[SerializeField]
	protected List<string> nameListGivableElements;

	protected override void Start(){
		base.Start();

		isDestroyedAfterBeingPicked = true;
		
		if(isInactive){
			this.gameObject.GetComponent<Renderer>().enabled = false;
		}

	}

	/*
	protected override void InitializeInformation(){
		nameListGivableElements = new List<string>();

		if(isInactive){
			this.gameObject.GetComponent<Renderer>().enabled = false;
		}

		InitializePickableInformation();
	}
	*/

	protected virtual void InitializePickableInformation(){} //Here the information about the pickable object

	public override void LeftClickAction(){
		//if(!isInactive && !Player.Instance.IsSpeaking() && !Player.Instance.IsInteracting() && !Player.Instance.IsInConversation()){
		if(!Player.Instance.isDoingAction && !CutScenesManager.IsPlaying()){
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
			//Player.Instance.isDoingAction = true;
			BeginAction();

			Player.Instance.Speak(groupID, nameID, "INTERACTION");

			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());

			yield return new WaitForSeconds(0.2f);

			Player.Instance.Manipulate(this);

			yield return new WaitForSeconds(0.2f);

			Player.Instance.Speak(groupID, nameID, "PICKED");

			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());
			//...

			yield return new WaitForSeconds(0.1f);

			EndAction();
			//Player.Instance.isDoingAction = false;
		}

	}
	
	public virtual void OnPlayerTouchingAction(){
		if(Inventory.Instance.IsInventoryFull()){
			Player.Instance.Speak("GUI", "DEFAULT", "FULL_INVENTORY");
		}
		else{
			Player.Instance.GrabItem(nameListGivableElements);
			if(isDestroyedAfterBeingPicked){
				SetInactive();
			}
		}
	}

	public List<string> NameListPickableObjects(){
		return nameListGivableElements;
	}

	/*
	public List<string> GrabbedObjectsDescription(){
		List<string> _definitionText = new List<string>();
		
		_definitionText.Add(groupID);
		_definitionText.Add(nameID);
		_definitionText.Add("PICKED");
		
		return _definitionText;
	}
	
	public PickableFrom PositionType(){
		return currentPickablePosition;
	}

	public bool IsDestroyedAfterBeingPicked(){
		return isDestroyedAfterBeingPicked;
	}
	*/
	
}
