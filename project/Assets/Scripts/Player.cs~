using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Player : Actor {
	public bool isWaiting { get; private set;}
	public bool isDoingAction { get; private set;}
	public bool isUsingItemInventory { get; private set;}
	public bool isGrabbingUpperItem { get; private set;}
	public bool isGrabbingBottomItem { get; private set;}
	public bool isTouchingItemAnimEventActivated { get; private set;}

	[SerializeField]
	private const float maxTimeIdleUntilWaitingAnimation = 6f;
	private float timeCounterUntilWaitingAnimation;
		
	//Initializing Singleton because Player can't inherit Actor and Singleton<T> at the same time
	private static Player instance;
	
	public static Player Instance{
		get{
			if(instance == null){
				Debug.Log("Player Instance null");
			}
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

	private void OnEnable(){
		InteractiveElement.beginPlayerAction += SetActionActive;
		InteractiveElement.endPlayerAction += SetActionInactive;
		Actor.beginPlayerConversation += SetConversationActive;
		Actor.endPlayerConversation += SetConversationInactive;
		ItemInventory.beginUsingItem += SetUsingInventoryActive;
		ItemInventory.endUsingItem += SetUsingInventoryInactive;
	}

	private void OnDisable(){
		InteractiveElement.beginPlayerAction -= SetActionActive;
		InteractiveElement.endPlayerAction -= SetActionInactive;
		Actor.beginPlayerConversation -= SetConversationActive;
		Actor.endPlayerConversation -= SetConversationInactive;
		ItemInventory.beginUsingItem -= SetUsingInventoryActive;
		ItemInventory.endUsingItem -= SetUsingInventoryInactive;
	}

	protected override void Start (){
		base.Start ();

		isWaiting = false;
		isDoingAction = false;
		isUsingItemInventory = false;
		isGrabbingUpperItem = false;
		isGrabbingBottomItem = false;
		isTouchingItemAnimEventActivated = false;

		if(GameState.lastPlayerPosition != Vector2.zero){
			this.transform.position = GameState.lastPlayerPosition;
		}
		
		originalPositionToGo = GameState.lastTargetedPositionByMouse;
		currentPosition = this.transform.position;
	}

	protected override void InitializeAdditionalActorInformation(){
		if(GameState.lastPlayerPosition != Vector2.zero){
			this.transform.position = GameState.lastPlayerPosition;
		}

		originalPositionToGo = GameState.lastTargetedPositionByMouse;
		currentPosition = this.transform.position;
	}

	protected override void Update () {
		base.Update();

		//if(IsIdle() && !CutScenesManager.IsPlaying() && !isWaiting){
		if(!isDoingAction && !CutScenesManager.IsPlaying() && !isWaiting){
			if((Time.time - timeCounterUntilWaitingAnimation) > maxTimeIdleUntilWaitingAnimation){
				isWaiting = true;
				timeCounterUntilWaitingAnimation = Time.time;
			}
		}
		else{
			timeCounterUntilWaitingAnimation = Time.time;
		}
	}

	protected override void AdditionalUpdateInformation(){
		if(IsIdle() && !CutScenesManager.IsPlaying() && !isWaiting){
			if((Time.time - timeCounterUntilWaitingAnimation) > maxTimeIdleUntilWaitingAnimation){
				isWaiting = true;
				timeCounterUntilWaitingAnimation = Time.time;
			}
		}
		else{
			timeCounterUntilWaitingAnimation = Time.time;
		}
	}

	public override void ActionOnItemInventoryUsed(GameObject itemInventory){}

	/*
	public void GrabUpperItem(PickableElement pickableItem){
		StartCoroutine(WaitForGrabbingUpperItemCompleted(pickableItem));
	}

	private IEnumerator WaitForGrabbingUpperItemCompleted(PickableElement pickableItem){
		do{
			yield return null;
		}while(!isSpeaking || !isWalking || !isInConversation || !isGrabbingBottomItem || !isGrabbingUpperItem);

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
	}

	public void GrabBottomItem(PickableElement pickableItem){
		StartCoroutine(WaitForGrabbingBottomItemCompleted(pickableItem));
	}
	
	private IEnumerator WaitForGrabbingBottomItemCompleted(PickableElement pickableItem){
		do{
			yield return null;
		}while(!isSpeaking || !isWalking || !isInConversation || !isGrabbingBottomItem || !isGrabbingUpperItem);

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
	}
	*/

	public void GrabItem(List<string> nameListGrabbedItems){
		foreach(string itemName in nameListGrabbedItems){
			Inventory.Instance.AddItem(itemName);
		}
	}

	public void Manipulate(PickableElement element){
		if(element.currentPickablePosition == PickableElement.PickableFrom.up){
			StartCoroutine(WaitForUpperInteractionCompleted(element));
		}
		else{
			StartCoroutine(WaitForBottomInteractionCompleted(element));
		}
	}

	/*
	public IEnumerator Interaction(PickableElement element){
		if(element.currentPickablePosition == PickableElement.PickableFrom.down){
			yield return WaitForBottomInteractionCompleted(element);
		}
		else{
			yield return WaitForUpperInteractionCompleted(element);
		}
	}
	*/

	public void UpperInteraction(PickableElement element){
		StartCoroutine(WaitForUpperInteractionCompleted(element));
	}
	
	private IEnumerator WaitForUpperInteractionCompleted(PickableElement element){
		do{
			yield return null;
		}while(isSpeaking && isWalking && isInteracting && isInConversation);

		isInteracting = true;
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
		isInteracting = false;
	}

	public void BottomInteraction(PickableElement element){
		StartCoroutine(WaitForBottomInteractionCompleted(element));
	}
	
	private IEnumerator WaitForBottomInteractionCompleted(PickableElement element){
		do{
			yield return null;
		}while(isSpeaking && isWalking && isInteracting && isInConversation);

		isInteracting = true;
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
		isInteracting = false;
	}

	public void PlayAnimation(string nameAnimation){
		StartCoroutine(WaitPlayAnimation(nameAnimation));
	}

	private IEnumerator WaitPlayAnimation(string nameAnimation){
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
	}

	public override void ChangeCursorOnMouseOver(){}

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

	public void SetActionActive(){
		isDoingAction = true;
	}

	public void SetActionInactive(){
		isDoingAction = false;
	}

	public void SetConversationActive(){
		isInConversation = true;
	}

	public void SetConversationInactive(){
		isInConversation = false;
	}

	public void SetUsingInventoryActive(){
		isUsingItemInventory = true;
	}
	
	public void SetUsingInventoryInactive(){
		isUsingItemInventory = false;
    }

	public override bool IsIdle(){
		if(isWalking || isInteracting || isSpeaking || isGrabbingUpperItem || isGrabbingBottomItem || isInConversation){
			return false;
		}
		else{
			return true;
		}
	}
}
