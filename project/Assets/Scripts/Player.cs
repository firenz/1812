using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Player : Actor {
	public bool isWaiting = false;
	public bool isUsingItemInventory = false;
	private bool isGrabbingUpperItem = false;
	private bool isGrabbingBottomItem = false;
	private bool isTouchingItemAnimEventActivated = false;
	private static float timeCounterUntilWaitingAnimation;
	private const float maxTimeIdleUntilWaitingAnimation = 6f;

	private static Player instance;
	
	public static Player Instance{
		get{
			return instance;
		}
	}
	
	private void OnDestroy() {
		if (instance == this) {
			instance = null;
		}
	}
	
	private void OnApplicationQuit(){
		instance = null;
	}
	
	private void Awake(){
		if(instance == null){
			instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }

	protected override void InitializeAdditionalActorInformation(){
		if(GameState.lastPlayerPosition != Vector2.zero){
			this.transform.position = GameState.lastPlayerPosition;
		}

		originalPositionToGo = GameState.lastTargetedPositionByMouse;
		currentPosition = this.transform.position;
	}

	protected override void AdditionalUpdateInformation(){
		if(isIdle && !isInteracting && !CutScenesManager.IsPlaying() && !isWaiting){
			if((Time.time - timeCounterUntilWaitingAnimation) > maxTimeIdleUntilWaitingAnimation){
				isWaiting = true;
				timeCounterUntilWaitingAnimation = Time.time;
			}
		}
		else{
			timeCounterUntilWaitingAnimation = Time.time;
		}
	}

	public override void ActionOnItemInventoryUsed(string nameItemInventory){}

	public void GrabUpperItem(PickableElement pickableItem){
		StartCoroutine(WaitForGrabbingUpperItemCompleted(pickableItem));
	}

	private IEnumerator WaitForGrabbingUpperItemCompleted(PickableElement pickableItem){
		do{
			yield return null;
		}while(!isIdle);

		isIdle = false;
		isGrabbingUpperItem = true;
		isPlayingAnimation = true;

		do{
			yield return null;
		}while(!isTouchingItemAnimEventActivated);
		
		List<string> _nameListGrabbedItems = pickableItem.NameListPickableObjects();
		foreach(string itemName in _nameListGrabbedItems){
			Inventory.Instance.AddItem(itemName);
		}

		if(pickableItem.IsDestroyedAfterBeingPicked()){
			pickableItem.SetInactive();
		}

		do{
			yield return null;
		}while(!isPlayingAnimation);

		isGrabbingUpperItem = false;
		isIdle = true;
	}

	public void GrabBottomItem(PickableElement pickableItem){
		StartCoroutine(WaitForGrabbingBottomItemCompleted(pickableItem));
	}
	
	private IEnumerator WaitForGrabbingBottomItemCompleted(PickableElement pickableItem){
		do{
			yield return null;
		}while(isInConversation || !isIdle);
		
		isIdle = false;
		isGrabbingBottomItem = true;
		
		do{
			yield return null;
		}while(!isTouchingItemAnimEventActivated);

		List<string> _nameListGrabbedItems = pickableItem.NameListPickableObjects();
		foreach(string itemName in _nameListGrabbedItems){
			Inventory.Instance.AddItem(itemName);
		}

		if(pickableItem.IsDestroyedAfterBeingPicked()){
			pickableItem.SetInactive();
		}

		do{
			yield return null;
		}while(!isPlayingAnimation);

		isGrabbingBottomItem = false;
		isIdle = true;
	}

	public void UpperInteraction(InteractiveElement element){
		StartCoroutine(WaitForUpperInteractionCompleted(element));
	}
	
	private IEnumerator WaitForUpperInteractionCompleted(InteractiveElement element){
		do{
			yield return null;
		}while(!isIdle);
		
		isIdle = false;
		isGrabbingUpperItem = true;
		isPlayingAnimation = true;
		
		do{
			yield return null;
		}while(!isTouchingItemAnimEventActivated);

		element.OnPlayerTouchingAction();
		
		do{
			yield return null;
		}while(!isPlayingAnimation);
		
		isGrabbingUpperItem = false;
		isIdle = true;
	}

	public void BottomInteraction(InteractiveElement element){
		StartCoroutine(WaitForBottomInteractionCompleted(element));
	}
	
	private IEnumerator WaitForBottomInteractionCompleted(InteractiveElement element){
		do{
			yield return null;
		}while(!isIdle);
		
		isIdle = false;
		isGrabbingBottomItem = true;
		isPlayingAnimation = true;
		
		do{
			yield return null;
		}while(!isTouchingItemAnimEventActivated);
		
		element.OnPlayerTouchingAction();
		
		do{
			yield return null;
		}while(!isPlayingAnimation);
		
		isGrabbingBottomItem = false;
		isIdle = true;
	}

	public void PlayAnimation(string nameAnimation){
		StartCoroutine(WaitPlayAnimation(nameAnimation));
	}

	private IEnumerator WaitPlayAnimation(string nameAnimation){
		isIdle = false;
		switch(nameAnimation){
		case "UpperGrabbing":
			isGrabbingUpperItem = true;

			do{
				yield return null;
			}while(isPlayingAnimation);

			isGrabbingUpperItem = false;
			break;
		case "BottomGrabbing":
			isGrabbingBottomItem = true;

			do{
				yield return null;
			}while(isPlayingAnimation);

            isGrabbingBottomItem = false;
            break;
		case "UsingPhone":
	        isWaiting = true;

			do{
				yield return null;
			}while(isPlayingAnimation);

	        isWaiting = false;
	        break;
		default:
			break;
		}
		endOfAnimationEvent = false;
		isIdle = true;
	}
    

	public void StartOfAnimationEvent(){
		isPlayingAnimation = true;
		endOfAnimationEvent = false;
		isTouchingItemAnimEventActivated = false;
	}

	public void TouchingItemAnimEvent(){
		isTouchingItemAnimEventActivated = true;
	}
	
	public bool IsTouchingItem(){
		return isTouchingItemAnimEventActivated;
    }

	public void EndOfAnimationEvent(){
		isPlayingAnimation = false;
		endOfAnimationEvent = true;
		isTouchingItemAnimEventActivated = false;
	}
	
	public bool IsEndOfAnimationEvent(){
		return endOfAnimationEvent;
	}
	
	public void SetEndOfAnimationEventInactive(){
		endOfAnimationEvent = true;
	}

	public bool IsGrabbingUpperItem(){
		return isGrabbingUpperItem;
	}

	public bool IsGrabbingBottomItem(){
		return isGrabbingBottomItem;
	}

	public bool IsWaiting(){
		return isWaiting;
	}

	public bool IsUsingItemInventory(){
		return isUsingItemInventory;
	}

	public void SetInteractionActive(){
		isInteracting = true;
	}

	public void SetInteractionInactive(){
		isInteracting = false;
	}

	public void SetUsingInventoryActive(){
		isUsingItemInventory = true;
	}

	public void SetUsingInventoryInactive(){
		isUsingItemInventory = false;
	}

	public void SetWaitingInactive(){
		isWaiting = false;
	}

	public void SetUpperInteractionActive(){
		isGrabbingUpperItem = true;
	}

	public void SetUpperInteractionInactive(){
		isGrabbingUpperItem = false;
    }

	public void SetBottomInteractionActive(){
		isGrabbingBottomItem = true;
	}
	
	public void SetBottomInteractionInactive(){
		isGrabbingBottomItem = false;
    }
}
