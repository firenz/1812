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

[RequireComponent (typeof (Animator))]
public class ActorStateController : MonoBehaviour {
	protected Actor actor;

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

	protected Animator actorAnimator = null;
	protected Vector2 actorMoveDirection;
	protected bool isDirectionToGoReached = true;
	protected bool isSpeaking = false;
	protected bool hasEndedSpeaking = true;

	// Use this for initialization
	protected virtual void Start () {
		actor = this.GetComponent<Actor>();
		actorAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected void LateUpdate(){
		actorMoveDirection = actor.moveDirection;
		isDirectionToGoReached = actor.isDirectionToGoReached;
		
		if(!isDirectionToGoReached){
			if(actorMoveDirection.x > 0f){
				//if(OnStateChange != null){
					OnStateChange(actorStates.rightWalking);
					currentState = actorStates.rightWalking;
				//} 
			}
			else if(actorMoveDirection.x < 0f){
				//if(OnStateChange != null){
					OnStateChange(actorStates.leftWalking);
					currentState = actorStates.leftWalking;
				//} 
			}
			
		}
		else if(isDirectionToGoReached){
			this.ActionStatesHandler();
        }
        
        
    }

	protected virtual void ActionStatesHandler(){
		isSpeaking = actor.isSpeaking;
		hasEndedSpeaking = actor.hasEndedSpeaking;
		
		if(!actor.isSpeaking){
			
			switch(currentState){
			case actorStates.leftWalking:
				//if(OnStateChange != null){
				OnStateChange(actorStates.leftIdle);
				//}
				break;
				
			case actorStates.rightWalking:
				//if(OnStateChange != null){
				OnStateChange(actorStates.rightIdle);
				//}
				break;
				
			case actorStates.leftIdle:
				break;
				
			case actorStates.rightIdle:
				break;
				
			case actorStates.leftSpeaking:
				//if(OnStateChange != null){
				OnStateChange(actorStates.leftIdle);
				//}
				break;
				
			case actorStates.rightSpeaking:
				//if(OnStateChange != null){
				OnStateChange(actorStates.rightIdle);
				//}
				break;
			}
			
		}
		else if(isSpeaking){
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
				}
				
			}
			
		}

	}

	protected virtual void OnStateChange(actorStates newState){
		if(newState == currentState){
			return;
		}
		
		switch(newState){
			case actorStates.leftIdle:
				actorAnimator.SetBool("isLeftWalking", false);
				actorAnimator.SetBool("isRightWalking", false);
				actorAnimator.SetBool("isSpeaking",false);
				break;
				
			case actorStates.rightIdle:
				actorAnimator.SetBool("isLeftWalking", false);
				actorAnimator.SetBool("isRightWalking", false);
				actorAnimator.SetBool("isSpeaking",false);
				break;
				
			case actorStates.leftWalking:
				actorAnimator.SetBool("isLeftWalking", true);
				actorAnimator.SetBool("isRightWalking", false);
				actorAnimator.SetBool("isSpeaking",false);
				break;
				
			case actorStates.rightWalking:
				actorAnimator.SetBool("isLeftWalking", false);
				actorAnimator.SetBool("isRightWalking", true);
				actorAnimator.SetBool("isSpeaking",false);
				break;
				
			case actorStates.leftSpeaking:
				actorAnimator.SetBool("isLeftWalking", false);
				actorAnimator.SetBool("isRightWalking", false);
	            actorAnimator.SetBool("isSpeaking",true);
	            break;
                
            case actorStates.rightSpeaking:
               	actorAnimator.SetBool("isLeftWalking", false);
                actorAnimator.SetBool("isRightWalking", false);
                actorAnimator.SetBool("isSpeaking",true);
                break;

			case actorStates.rightUpperGrabbing:
               	actorAnimator.SetBool("isLeftWalking", false);
                actorAnimator.SetBool("isRightWalking", false);
                actorAnimator.SetBool("isSpeaking",false);
                break;

			case actorStates.leftUpperGrabbing:
               	actorAnimator.SetBool("isLeftWalking", false);
                actorAnimator.SetBool("isRightWalking", false);
                actorAnimator.SetBool("isSpeaking",false);
                break;
        }
        
        previousState = currentState;
        currentState = newState;
	}

	protected IEnumerator WaitForAnimation(Animation animation){
		/*
		do{
			yield return null;
		}while(animation.isPlaying);
		*/
		yield return new WaitForSeconds(2.5f);
	}

}
