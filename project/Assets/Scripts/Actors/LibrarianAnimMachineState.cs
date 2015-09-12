using UnityEngine;
using System.Collections;

public class LibrarianAnimMachineState : ActorAnimStateMachine {
	private Librarian librarian;
	private LibrarianStates currentState;
	
	protected override void InitializeAnimData(){
		librarian = this.GetComponent<Librarian>();	
	}

	protected override void Update () {
		if(librarian.isWalking){
			if(librarian.isFacingLeft){
				OnAnimationStateChange(LibrarianStates.leftWalking);
				currentState = LibrarianStates.leftWalking;
			}
			else if(librarian.isFacingRight){
				OnAnimationStateChange(LibrarianStates.rightWalking);
				currentState = LibrarianStates.rightWalking;
			}
		}
		else if(librarian.isSpeaking){
			if(librarian.isFacingLeft){
				OnAnimationStateChange(LibrarianStates.leftSpeaking);
				currentState = LibrarianStates.leftSpeaking;
			}
			else if(librarian.isFacingRight){
				OnAnimationStateChange(LibrarianStates.rightSpeaking);
				currentState = LibrarianStates.rightSpeaking;
			}
		}
		else if(librarian.isCoughing){
			if(librarian.isFacingLeft){
				OnAnimationStateChange(LibrarianStates.leftCoughing);
				currentState = LibrarianStates.leftCoughing;
			}
			else if(librarian.isFacingRight){
				OnAnimationStateChange(LibrarianStates.rightCoughing);
				currentState = LibrarianStates.rightCoughing;
			}
		}
		else{
			if(librarian.isFacingLeft){
				OnAnimationStateChange(LibrarianStates.leftIdle);
				currentState = LibrarianStates.leftIdle;
			}
			else if(librarian.isFacingRight){
				OnAnimationStateChange(LibrarianStates.rightIdle);
				currentState = LibrarianStates.rightIdle;
			}
		}
	}
	
	private void OnAnimationStateChange(LibrarianStates newState){
		if(newState == currentState){
			return;
		}
		
		switch(newState){
		case LibrarianStates.leftIdle:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking",false);
			animator.SetBool("isCoughing", false);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
			
		case LibrarianStates.rightIdle:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking",false);
			animator.SetBool("isCoughing", false);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case LibrarianStates.leftWalking:
			animator.SetBool("isWalking", true);
			animator.SetBool("isSpeaking",false);
			animator.SetBool("isCoughing", false);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
			
		case LibrarianStates.rightWalking:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking",false);
			animator.SetBool("isCoughing", false);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case LibrarianStates.leftSpeaking:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking",true);
			animator.SetBool("isCoughing", false);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
			
		case LibrarianStates.rightSpeaking:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking",true);
			animator.SetBool("isCoughing", false);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case LibrarianStates.rightCoughing:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking",false);
			animator.SetBool("isCoughing", true);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case LibrarianStates.leftCoughing:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking",false);
			animator.SetBool("isCoughing", true);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
		}
		
		currentState = newState;
	}
}

public class LibrarianStates : ActorStates{
	public const int leftCoughing = 6;
	public const int rightCoughing = 7;
	
	public LibrarianStates(){}
	
	public LibrarianStates(int value){
		Value = value;
	}
	
	public static implicit operator int(LibrarianStates type){
		return type.Value;
	}
	
	public static implicit operator LibrarianStates(int value){
		return new LibrarianStates(value);
	}
}