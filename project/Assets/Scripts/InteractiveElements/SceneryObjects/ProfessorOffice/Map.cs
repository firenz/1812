using UnityEngine;
using System.Collections;

public class Map : PickableElement {
	private bool isWindBlewForFirstTime;
	private Animator mapFlagsAnimator;
	private AnimatorStateInfo currentState;

	public delegate void ActivateLittleFlagsPickableElement();
	public static event ActivateLittleFlagsPickableElement activateLittleFlags;

	private void OnEnable(){
		Window.windowOpenedFirstTime += PlayLittleFlagsFallingAnimation;
	}

	private void OnDisable(){	
		Window.windowOpenedFirstTime -= PlayLittleFlagsFallingAnimation;
	}

	protected override void InitializePickableInformation() {
		mapFlagsAnimator = this.GetComponent<Animator>();

		if(!GameState.MapPuzzleData.isMapPuzzleSolved){
			if(GameState.LevelProfessorOfficeData.isLittleFlagsFellIntoFloor){
				SetSomeLittleFlagsMissed();
			}
		}
	}

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

			if(!GameState.MapPuzzleData.isMapPuzzleSolved){
				Player.Instance.Speak(groupID, nameID, "INTERACTION_1");
			}
			else{
				Player.Instance.Speak(groupID, nameID, "INTERACTION_2");
			}

			do{
				yield return null;
			}while(Player.Instance.isSpeaking);
			
			EndAction();
		}
	}
	
	public override void OnPlayerTouchingAction(){
		GameController.WarpToLevel("MapPuzzle");
	}
	
	public override void ActionOnItemInventoryUsed(GameObject itemInventory){
		if(!Player.Instance.isDoingAction && !CutScenesManager.IsPlaying()){
			switch(itemInventory.name){
			case "LittleFlagsInventory":
				itemInventory.GetComponent<ItemInventory>().Deselect();
				StartCoroutine(LittleFlagsOnMap());
				break;
			default:
				break;
			}
		}
	}
	
	private IEnumerator LittleFlagsOnMap(){
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
			yield return new WaitForSeconds(0.2f);
			Player.Instance.Speak(groupID, nameID, "INTERACTION_LITTLEFLAGS");
			
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			Player.Instance.Manipulate(this);

			EndAction();
		}
	}

	private void PlayLittleFlagsFallingAnimation(){
		mapFlagsAnimator.SetBool("windBlowsForTheFirstTime", true);
		mapFlagsAnimator.SetBool("isMapPuzzleSolved", true);
	}

	private void SetSomeLittleFlagsMissed(){
		mapFlagsAnimator.SetBool("windBlowsForTheFirstTime", true);
		mapFlagsAnimator.SetBool("isMapPuzzleSolved", false);
	}

	public void FallenLittleFlagsUpdateValue(){
		activateLittleFlags();
		GameState.LevelProfessorOfficeData.isLittleFlagsFellIntoFloor = true; 
		GameState.MapPuzzleData.isMapPuzzleSolved = false;
	}
}
