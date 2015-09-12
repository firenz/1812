using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class PickableElement : InteractiveElement {
	
	public enum PickableFrom{
		down,
		up
	}

	[SerializeField]
	public PickableFrom currentPickablePosition = PickableFrom.down;

	[SerializeField]
	public bool isDestroyedAfterBeingPicked;

	[SerializeField]
	protected List<string> nameListGivableElements;
	
	protected override void InitializeInformation() {
		isDestroyedAfterBeingPicked = true;
		
		if(isInactive){
			SetInactive();
        }

		InitializePickableInformation();
	}

	protected virtual void InitializePickableInformation() {}

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
			Player.Instance.Speak(groupID, nameID, "INTERACTION");

			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			yield return new WaitForSeconds(0.2f);

			Player.Instance.Manipulate(this);
			yield return new WaitForSeconds(0.5f);
			Player.Instance.Speak(groupID, nameID, "PICKED");

			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			yield return new WaitForSeconds(0.1f);
			EndAction();
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
}
