using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Librarian : Actor {
	public const float maxTimeIdleUntilCoughingAnimation = 8f;
	public bool isCoughing { get; private set;}

	private float timeCounterUntilCoughingAnimation;

	public delegate void FinishedChoiceWithLibrarian();
	public static event FinishedChoiceWithLibrarian finishedChoiceWithLibrarian;

	protected override void InitializeAdditionalActorInformation() {
		if(!GameState.CutSceneData.isPlayedCutsceneAfterMapPuzzleIsSolved){
			SetInactive();
		}
		
		timeCounterUntilCoughingAnimation = Time.time;
        isCoughing = false;
    }
    
	protected override void Update (){
		if(!isInactive){
			base.Update ();

			if(IsIdle() && !CutScenesManager.IsPlaying() && !isCoughing){
				if((Time.time - timeCounterUntilCoughingAnimation) > maxTimeIdleUntilCoughingAnimation){
					isCoughing = true;
					timeCounterUntilCoughingAnimation = Time.time;
				}
			}
			else{
				timeCounterUntilCoughingAnimation = Time.time;
			}
		}
	}

	protected override IEnumerator WaitForRightClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.currentPosition, interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= minDistance){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.isWalking);
		}
		
		if(Player.Instance.originalTargetedPosition == interactivePosition){
			BeginAction();

			if(Player.Instance.transform.position.x < (interactivePosition.x + spriteWidth * 0.5f)){
				Player.Instance.LookToTheRight();
			}
			else{
				Player.Instance.LookToTheLeft();
			}

			Player.Instance.Speak(groupID, nameID, "DESCRIPTION");
			
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			yield return new WaitForSeconds(0.1f);
			EndAction();
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

			Player.Instance.LookToTheRight();
			yield return new WaitForSeconds(0.2f);
			
			if(isCoughing){
				Player.Instance.Speak(groupID, nameID, "NPC_IS_BUSY");
				
				do{
					yield return null;
				}while(Player.Instance.isSpeaking);

			}
			else if(!GameState.LevelCorridorData.isDialogueChoiceMadeWithLibrarianFinished){
				BeginConversation();

				Player.Instance.Speak(groupID, nameID, "PLAYER_CONVERSATION_CHOICE_NOT_FINISH");
				do{
					yield return null;
				}while(Player.Instance.isSpeaking);

				yield return new WaitForSeconds(0.1f);
				
				MultipleChoiceManager.Instance.CreateMultipleSelection("LIBRARIAN_CONVERSATION_CHOICE_1");
				do{
					yield return null;
				}while(!MultipleChoiceManager.Instance.isSelectionEnded);
				
				//For handling different actions depending of the chosen choice, use this
				int _resultChoice = MultipleChoiceManager.Instance.GetSelectionResult();
				switch(_resultChoice){
				case 0:
					this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_RESULT_CHOICE_0");
					yield return null;
					
					do{
						yield return null;
					}while(this.isSpeaking);
					
					yield return StartCoroutine(PlayerActionsOnChoiceResult());
					
					break;
				case 1:
					this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_RESULT_CHOICE_1");
					do{
						yield return null;
					}while(this.isSpeaking);
					
					yield return StartCoroutine(PlayerActionsOnChoiceResult());
					
					break;
				default:
					break;
				}

				EndConversation();
			}
			else{
				BeginConversation();

				if(!Inventory.Instance.HasItem("FailedTestInventory")){
					Player.Instance.Speak(groupID, nameID, "PLAYER_CONVERSATION_FAILEDTEST_LOST");
					do{
						yield return null;
					}while(Player.Instance.isSpeaking);
					
					yield return new WaitForSeconds(0.1f);
					
					this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_FAILEDTEST_LOST");
					do{
						yield return null;
					}while(this.isSpeaking);
					
					yield return new WaitForSeconds(0.1f);
					
					Inventory.Instance.AddItem("FailedTestInventory");
				}
				else{
					Player.Instance.Speak(groupID, nameID, "PLAYER_DEFAULT_CONVERSATION");
					do{
						yield return null;
					}while(Player.Instance.isSpeaking);
					
					yield return new WaitForSeconds(0.1f);
					
					this.Speak(groupID, nameID, "LIBRARIAN_DEFAULT_CONVERSATION");
					do{
						yield return null;
					}while(this.isSpeaking);
					
					yield return new WaitForSeconds(0.1f);
				}

				EndConversation();
			}

			EndAction();
		}
	}

	private IEnumerator PlayerActionsOnChoiceResult(){
		yield return new WaitForSeconds(0.2f);
		
		Player.Instance.Speak(groupID, nameID, "PLAYER_CONVERSATION_RESULT_CHOICE_FINAL_1");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_RESULT_CHOICE_FINAL_1");
		do{
			yield return null;
		}while(this.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		Player.Instance.Speak(groupID, nameID, "PLAYER_CONVERSATION_RESULT_CHOICE_FINAL_2");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_RESULT_CHOICE_FINAL_2");
		do{
			yield return null;
		}while(this.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		Player.Instance.Speak(groupID, nameID, "PLAYER_CONVERSATION_RESULT_CHOICE_FINAL_3");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_RESULT_CHOICE_FINAL_3");
		do{
			yield return null;
		}while(this.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		Player.Instance.Speak(groupID, nameID, "PLAYER_CONVERSATION_RESULT_CHOICE_FINAL_4");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_RESULT_CHOICE_FINAL_4");
		do{
			yield return null;
		}while(this.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		Player.Instance.Speak(groupID, nameID, "PLAYER_CONVERSATION_RESULT_CHOICE_FINAL_5");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_RESULT_CHOICE_FINAL_5");
		do{
			yield return null;
		}while(this.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		Player.Instance.Speak(groupID, nameID, "PLAYER_CONVERSATION_RESULT_CHOICE_FINAL_6");
		do{
			yield return null;
		}while(Player.Instance.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);

		Player.Instance.GoTo(GameObject.Find("CutscenePoint_3").transform.position);
		do{
			yield return null;
		}while(Player.Instance.isWalking);
		
		yield return new WaitForSeconds(0.2f);
		
		this.Speak(groupID, nameID, "LIBRARIAN_CONVERSATION_RESULT_CHOICE_FINAL_6");
		do{
			yield return null;
		}while(this.isSpeaking);
		
		yield return new WaitForSeconds(0.2f);
		
		GameState.LevelCorridorData.isDialogueChoiceMadeWithLibrarianFinished = true;
		finishedChoiceWithLibrarian();
		AudioManager.StopAllMusic();
		AudioManager.PlayMusic("Joy", true);
	}

	private void CoughingEventAnimation(){
		dialogueHandler.DisplayText(groupID, nameID, "COUGH", false, 0, 0);
	}
	
	private void CoughingEventEnds(){
		isCoughing = false;
	}

	public override bool IsIdle(){
		if(isWalking || isInteracting || isSpeaking || isInConversation || isCoughing){
			return false;
		}
		else{
			return true;
		}
	}
}
