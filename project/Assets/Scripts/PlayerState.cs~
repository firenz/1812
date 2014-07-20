/*	This file is part of 1812: La aventura.

    1812: La aventura is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    1812: La aventura is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with 1812: La aventura.  If not, see <http://www.gnu.org/licenses/>.
*/

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Player))]
public class PlayerState : ActorStateController {
	private Player player;

	/*
	protected enum actorStates{
		leftIdle = 0,
		rightIdle,
		leftWalking,
		rightWalking,
		leftSpeaking,
		rightSpeaking,
		leftUpperGrabbing,
		rightUpperGrabbing,
		leftBottomGrabbing,
		rightBottomGrabbing
		//leftUsingPhone, To be implemented
		//rightUsingPhone To be implemented
	}
	
	protected actorStates currentState;
	protected actorStates previousState;
	*/
	private bool isInteracting = false;
	private bool isGrabbingUpperObject = false;
	private bool isGrabbingBottomObject = false;
	private bool hasEndedGrabbing = true;
	//private bool hasEndedManipulatingMechanism = true;
	
	protected override void Start(){
		actor = this.GetComponent<Actor>();
		actorAnimator = this.GetComponent<Animator>();
		player = this.GetComponent<Player>();
	}

	protected override void ActionStatesHandler(){
		isInteracting = player.isInteracting;
		isSpeaking = player.isSpeaking;
		hasEndedSpeaking = player.hasEndedSpeaking;
		isGrabbingUpperObject = player.isGrabbingUpperObject;
		isGrabbingBottomObject = player.isGrabbingBottomObject;
		hasEndedGrabbing = player.hasEndedGrabbing;
		//hasEndedManipulatingMechanism = player.hasEndedManipulatingMechanism;

		//if(!isSpeaking || !isGrabbingUpperObject || !isGrabbingBottomObject){
		if(!isSpeaking && !isInteracting){
			
			switch(currentState){
				case actorStates.leftWalking:
					OnStateChange(actorStates.leftIdle);
					break;
					
				case actorStates.rightWalking:
					OnStateChange(actorStates.rightIdle);
					break;
					
				case actorStates.leftIdle:
					break;
					
				case actorStates.rightIdle:
					break;
					
				case actorStates.leftSpeaking:
					OnStateChange(actorStates.leftIdle);
					break;
					
				case actorStates.rightSpeaking:
					OnStateChange(actorStates.rightIdle);
					break;

				case actorStates.leftUpperGrabbing:
					OnStateChange(actorStates.leftIdle);
					break;

				case actorStates.rightUpperGrabbing:
					OnStateChange(actorStates.rightIdle);
					break;

				case actorStates.leftBottomGrabbing:
					OnStateChange(actorStates.leftIdle);
					break;

				case actorStates.rightBottomGrabbing:
					OnStateChange(actorStates.rightIdle);
					break;
			}
			
		}
		else if(isSpeaking){
			Debug.Log("isSpeaking");
			if(hasEndedSpeaking){
				switch(currentState){
				case actorStates.leftSpeaking:
					OnStateChange(actorStates.leftIdle);
					break;
					
				case actorStates.rightSpeaking: 
					OnStateChange(actorStates.rightIdle);
					break;
				}
				
			}
			else{
				switch(currentState){
				case actorStates.leftIdle:
					OnStateChange(actorStates.leftSpeaking);
					break;
					
				case actorStates.leftWalking:
					OnStateChange(actorStates.leftSpeaking);
					break;
					
				case actorStates.rightIdle: 
					OnStateChange(actorStates.rightSpeaking);
					break;
					
				case actorStates.rightWalking:
					OnStateChange(actorStates.rightSpeaking);
					break;
					
				case actorStates.leftUpperGrabbing:
					OnStateChange(actorStates.leftSpeaking);
					break;
					
				case actorStates.rightUpperGrabbing:
					OnStateChange(actorStates.rightSpeaking);
					break;
					
				case actorStates.leftBottomGrabbing:
					OnStateChange(actorStates.leftSpeaking);
					break;
					
				case actorStates.rightBottomGrabbing:
					OnStateChange(actorStates.rightSpeaking);
					break;

				case actorStates.rightSpeaking:
					break;

				case actorStates.leftSpeaking:
					break;
				}
			}

			
		}
		if(isInteracting){
			Debug.Log("isInteracting");
			if(hasEndedGrabbing){
				switch(currentState){
				case actorStates.leftWalking:
					OnStateChange(actorStates.leftIdle);
					break;
					
				case actorStates.rightWalking:
					OnStateChange(actorStates.rightIdle);
					break;
					
				case actorStates.leftIdle:
					break;
					
				case actorStates.rightIdle:
					break;
					
				case actorStates.leftSpeaking:
					OnStateChange(actorStates.leftIdle);
					break;
					
				case actorStates.rightSpeaking:
					OnStateChange(actorStates.rightIdle);
					break;
					
				case actorStates.leftUpperGrabbing:
					OnStateChange(actorStates.leftIdle);
					break;
					
				case actorStates.rightUpperGrabbing:
					OnStateChange(actorStates.rightIdle);
					break;
					
				case actorStates.leftBottomGrabbing:
					OnStateChange(actorStates.leftIdle);
					break;
					
				case actorStates.rightBottomGrabbing:
					OnStateChange(actorStates.rightIdle);
					break;
				}
			}
			else{
				Debug.Log("isInteracting");
				if(isGrabbingUpperObject){
					Debug.Log("isGrabbingUpperObject");
					switch(currentState){
					case actorStates.leftIdle:
						OnStateChange(actorStates.leftUpperGrabbing);
						break;
						
					case actorStates.leftWalking:
						OnStateChange(actorStates.leftUpperGrabbing);
						break;
						
					case actorStates.rightIdle: 
						OnStateChange(actorStates.rightUpperGrabbing);
						break;
						
					case actorStates.rightWalking:
						OnStateChange(actorStates.rightUpperGrabbing);
						break;
						
					case actorStates.leftSpeaking:
						OnStateChange(actorStates.leftUpperGrabbing);
						break;
						
					case actorStates.rightSpeaking:
						OnStateChange(actorStates.rightUpperGrabbing);
						break;
						
					case actorStates.leftUpperGrabbing:
						break;
						
					case actorStates.rightUpperGrabbing:
						break;
					}
					//StartCoroutine(WaitForAnimation(this.animation));
				}
				else if(isGrabbingBottomObject){
					Debug.Log("isGrabbingBottomObject");
					switch(currentState){
					case actorStates.leftIdle:
						OnStateChange(actorStates.leftBottomGrabbing);
						break;
						
					case actorStates.leftWalking:
						OnStateChange(actorStates.leftBottomGrabbing);
						break;
						
					case actorStates.rightIdle: 
						OnStateChange(actorStates.rightBottomGrabbing);
						break;
						
					case actorStates.rightWalking:
						OnStateChange(actorStates.rightBottomGrabbing);
						break;
						
					case actorStates.leftSpeaking:
						OnStateChange(actorStates.leftBottomGrabbing);
						break;
						
					case actorStates.rightSpeaking:
						OnStateChange(actorStates.rightBottomGrabbing);
						break;
						
					case actorStates.leftBottomGrabbing:
						break;
						
					case actorStates.rightBottomGrabbing:
						break;
					}
					//StartCoroutine(WaitForAnimation(this.animation));
				}
			}

		}

		
	}

	protected override void OnStateChange(actorStates newState){
		if(newState == currentState){
			return;
		}
		
		switch(newState){
		case actorStates.leftIdle:
			actorAnimator.SetBool("isLeftWalking", false);
			actorAnimator.SetBool("isRightWalking", false);
			actorAnimator.SetBool("isSpeaking",false);
			actorAnimator.SetBool("isUpperGrabbing", false);
			actorAnimator.SetBool("isBottomGrabbing", false);
			break;
			
		case actorStates.rightIdle:
			actorAnimator.SetBool("isLeftWalking", false);
			actorAnimator.SetBool("isRightWalking", false);
			actorAnimator.SetBool("isSpeaking",false);
			actorAnimator.SetBool("isUpperGrabbing", false);
			actorAnimator.SetBool("isBottomGrabbing", false);
			break;
			
		case actorStates.leftWalking:
			actorAnimator.SetBool("isLeftWalking", true);
			actorAnimator.SetBool("isRightWalking", false);
			actorAnimator.SetBool("isSpeaking",false);
			actorAnimator.SetBool("isUpperGrabbing", false);
			actorAnimator.SetBool("isBottomGrabbing", false);
			break;
			
		case actorStates.rightWalking:
			actorAnimator.SetBool("isLeftWalking", false);
			actorAnimator.SetBool("isRightWalking", true);
			actorAnimator.SetBool("isSpeaking",false);
			actorAnimator.SetBool("isUpperGrabbing", false);
			actorAnimator.SetBool("isBottomGrabbing", false);
			break;
			
		case actorStates.leftSpeaking:
			actorAnimator.SetBool("isLeftWalking", false);
			actorAnimator.SetBool("isRightWalking", false);
			actorAnimator.SetBool("isSpeaking",true);
			actorAnimator.SetBool("isUpperGrabbing", false);
			actorAnimator.SetBool("isBottomGrabbing", false);
			break;
			
		case actorStates.rightSpeaking:
			actorAnimator.SetBool("isLeftWalking", false);
			actorAnimator.SetBool("isRightWalking", false);
			actorAnimator.SetBool("isSpeaking",true);
			actorAnimator.SetBool("isUpperGrabbing", false);
			actorAnimator.SetBool("isBottomGrabbing", false);
			break;

		case actorStates.rightUpperGrabbing:
			actorAnimator.SetBool("isLeftWalking", false);
			actorAnimator.SetBool("isRightWalking", false);
			actorAnimator.SetBool("isSpeaking",false);
			actorAnimator.SetBool("isUpperGrabbing", true);
			actorAnimator.SetBool("isBottomGrabbing", false);
			break;
			
		case actorStates.leftUpperGrabbing:
			actorAnimator.SetBool("isLeftWalking", false);
			actorAnimator.SetBool("isRightWalking", false);
			actorAnimator.SetBool("isSpeaking",false);
			actorAnimator.SetBool("isUpperGrabbing", true);
			actorAnimator.SetBool("isBottomGrabbing", false);
			break;

		case actorStates.rightBottomGrabbing:
			actorAnimator.SetBool("isLeftWalking", false);
			actorAnimator.SetBool("isRightWalking", false);
			actorAnimator.SetBool("isSpeaking",false);
			actorAnimator.SetBool("isUpperGrabbing", false);
			actorAnimator.SetBool("isBottomGrabbing", true);
			break;

		case actorStates.leftBottomGrabbing:
			actorAnimator.SetBool("isLeftWalking", false);
			actorAnimator.SetBool("isRightWalking", false);
			actorAnimator.SetBool("isSpeaking",false);
			actorAnimator.SetBool("isUpperGrabbing", false);
			actorAnimator.SetBool("isBottomGrabbing", true);
			break;
		}
		
		previousState = currentState;
		currentState = newState;
	}
	
}
