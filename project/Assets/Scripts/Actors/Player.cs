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

	private bool isPositionInPathReached;
	private Vector2 currentPathStepPosition;

	[SerializeField]
	private const float maxTimeIdleUntilWaitingAnimation = 6f;
	private float timeCounterUntilWaitingAnimation;
		
	//Initializing Singleton because Player can't inherit Actor and Singleton<T> at the same time
	private static Player instance;
	
	public static Player Instance{
		get{
			if(instance == null){
				Debug.LogError("Player Instance is null");
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
	//End of Singleton initialization

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

	protected override void InitializeAdditionalActorInformation(){
		isWaiting = false;
		isDoingAction = false;
		isUsingItemInventory = false;
		isGrabbingUpperItem = false;
		isGrabbingBottomItem = false;
		isTouchingItemAnimEventActivated = false;

		if(GameState.lastPlayerPosition != Vector2.zero){
			this.transform.position = GameState.lastPlayerPosition;
		}
		
		originalTargetedPosition = GameState.lastTargetedPositionByMouse;
		currentPosition = this.transform.position;
	}

	protected override void Update () {
		if(moveDirection != Vector2.zero){
			float _distance = Mathf.Abs(Vector2.Distance(currentPosition, currentTargetedPosition));
			if(_distance > minDistance){
				this.transform.position = new Vector2(currentPosition.x + moveDirection.x, currentPosition.y + moveDirection.y);
				currentPosition = this.transform.position;
			}
			else{
				moveDirection = Vector2.zero;
				isPositionInPathReached = true;
				if(currentPathStepPosition == originalTargetedPosition){
					isWalking = false;
				}
			}
		} 

		AdditionalUpdateInformation();
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

	private void GoToInStraightDirection(Vector2 newPosition){
		StartCoroutine(WaitForGoToCompleted(newPosition));
	}

	public override void GoTo(Vector2 newPosition){
		StartCoroutine(WaitForGoToInPathCompleted(newPosition));
	}

	private IEnumerator WaitForGoToInPathCompleted(Vector2 newPosition){
		if(originalTargetedPosition != newPosition){
			do{
				yield return null;
			}while(isSpeaking && isInteracting && isInConversation); 
			
			if(isWalking){ //If it was already walking
				StopCoroutine(walkingCoroutine);
				isPositionInPathReached = true;
			}

			//Reset moveDirection just in case it was already walking
			isWalking = true;
			moveDirection = Vector2.zero;
			originalTargetedPosition = newPosition;
			List<Vector2> _path = NavigationManager.Instance.FindPath(currentPosition, newPosition);

			walkingCoroutine = StartCoroutine(WaitForPathCompleted(_path));
		}
	}

	private IEnumerator WaitForPathCompleted(List<Vector2> path){
		if(path.Count == 1){
			isPositionInPathReached = false;
			currentPathStepPosition = path[0];
			moveDirection = CalculateMoveDirection(currentPathStepPosition);
			do{
				yield return null;
			}while(!isPositionInPathReached && isWalking);
		}
		else if(path.Count > 1){
			for(int i = 0; i < path.Count; i++){
				isPositionInPathReached = false;
				currentPathStepPosition = path[i];
				moveDirection = CalculateMoveDirection(currentPathStepPosition);
				do{
					yield return null;
				}while(!isPositionInPathReached);
			}
		}

		do{
			yield return null;
		}while(isWalking);
	}

	public override void ActionOnItemInventoryUsed(GameObject itemInventory){}

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

	public override void ChangeCursorOnMouseOver(){}


	//Methods for animation events
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
	//End of methods for animation events

	//Action events
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
	//End of action events

	public override bool IsIdle(){
		if(isWalking || isDoingAction || isInteracting || isSpeaking || isGrabbingUpperItem || isGrabbingBottomItem || isInConversation){
			return false;
		}
		else{
			return true;
		}
	}
}
