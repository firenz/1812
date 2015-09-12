using UnityEngine;
using System.Collections;

public sealed class Window : PickableElement {	
	private string nameOpenWindowSFX = "OpenWindow_v1";
	private string nameCloseWindowSFX = "CloseWindow_v1";
	private string nameWindThroughWindowSFX = "Wind_v2";
	private Animator windAnimator;

	public delegate void WindowOpenedFirstTimeEvent();
	public static event WindowOpenedFirstTimeEvent windowOpenedFirstTime;

	protected override void InitializePickableInformation() {
		windAnimator = this.transform.FindChild("Wind").GetComponent<Animator>();
		isDestroyedAfterBeingPicked = false;

		if(GameState.LevelProfessorOfficeData.isWindowOpened){
			this.Open();
		}
        else{
            this.Close();
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

			if(GameState.LevelProfessorOfficeData.isWindowOpened){
                Player.Instance.Speak(groupID, nameID, "INTERACTION_OPENED");
			}
			else{
				Player.Instance.Speak(groupID, nameID, "INTERACTION_CLOSED");
			}

			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			Player.Instance.Manipulate(this);

			EndAction();
        }
    }

	public override void OnPlayerTouchingAction(){
		if(GameState.LevelProfessorOfficeData.isWindowOpened){
			this.Close();
		}
		else{
			this.Open();
		}
	}
    
    public override void ActionOnItemInventoryUsed(GameObject itemInventory){
		if(!Player.Instance.isDoingAction && !CutScenesManager.IsPlaying()){
			switch(itemInventory.name){
			case "FailedTestInventory":
				itemInventory.GetComponent<ItemInventory>().Deselect();
				StartCoroutine(FailedTestOnWindow());
                    break;
                default:
                    break;
			}
		}
	}
	
	private IEnumerator FailedTestOnWindow(){
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

			if(GameState.LevelProfessorOfficeData.isWindowOpened){
				Player.Instance.Speak(groupID, nameID, "INTERACTION_OPENED_FAILEDTEST");

				do{
					yield return null;
				}while(Player.Instance.isSpeaking);
				yield return new WaitForSeconds(0.1f);
				Inventory.Instance.RemoveItem("FailedTestInventory");
			}
			else{
				Player.Instance.Speak(groupID, nameID, "INTERACTION_CLOSED_FAILEDTEST");

				do{
					yield return null;
				}while(Player.Instance.isSpeaking);
			}

			EndAction();
		}
	}

	private void Close(){
		GameState.LevelProfessorOfficeData.isWindowOpened = false;
		this.gameObject.GetComponent<Renderer>().enabled = true;
		if(AudioManager.IsPlayingSFX(nameWindThroughWindowSFX)){
			AudioManager.StopSFX(nameWindThroughWindowSFX);
		}
		AudioManager.PlaySFX(nameCloseWindowSFX);
		windAnimator.SetBool("isWindowOpen", false);
	}

	private void Open(){
		GameState.LevelProfessorOfficeData.isWindowOpened = true;
		this.gameObject.GetComponent<Renderer>().enabled = false;
		AudioManager.PlaySFX(nameOpenWindowSFX);
		AudioManager.PlaySFX(nameWindThroughWindowSFX, true);
		windAnimator.SetBool("isWindowOpen", true);
		if(!GameState.LevelProfessorOfficeData.isLittleFlagsFellIntoFloor){
			windowOpenedFirstTime();
		}
	}
}
