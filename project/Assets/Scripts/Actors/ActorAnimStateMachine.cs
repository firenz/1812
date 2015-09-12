using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public abstract class ActorAnimStateMachine : MonoBehaviour {
	protected Animator animator;
	protected Actor actor;
	protected ActorStates currentAnimationState;

	protected void Start () {
		actor = this.GetComponent<Actor>();
		animator = this.GetComponent<Animator>();
		currentAnimationState = ActorStates.leftIdle;

		OnAnimationStateChange(currentAnimationState);
		InitializeAnimData();
	}

	protected virtual void InitializeAnimData(){}

	protected virtual void Update () {
		if(actor.isWalking){
			if(actor.isFacingLeft){
				OnAnimationStateChange(ActorStates.leftWalking);
				currentAnimationState = ActorStates.leftWalking;
			}
			else if(actor.isFacingRight){
				OnAnimationStateChange(ActorStates.rightWalking);
				currentAnimationState = ActorStates.rightWalking;
			}
		}
		else if(actor.isSpeaking){
			if(actor.isFacingLeft){
				OnAnimationStateChange(ActorStates.leftSpeaking);
				currentAnimationState = ActorStates.leftSpeaking;
			}
			else if(actor.isFacingRight){
				OnAnimationStateChange(ActorStates.rightSpeaking);
				currentAnimationState = ActorStates.rightSpeaking;
			}
		}
		else{
			if(actor.isFacingLeft){
				OnAnimationStateChange(ActorStates.leftIdle);
				currentAnimationState = ActorStates.leftIdle;
			}
			else if(actor.isFacingRight){
				OnAnimationStateChange(ActorStates.rightIdle);
				currentAnimationState = ActorStates.rightIdle;
			}
		}
	}

	//This method needs to be overrided if we use additional animation states
	protected virtual void OnAnimationStateChange(ActorStates newState){
		/*
		if(newState == currentAnimationState){
			return;
		}
		else{
			switch(newState){
			case ActorStates.leftIdle:
				animator.SetBool("isWalking", false);
				animator.SetBool("isSpeaking", false);
				animator.SetBool("isFacingLeft", true);
				animator.SetBool("isFacingRight", false);
				break;
			case ActorStates.rightIdle:
				animator.SetBool("isWalking", false);
				animator.SetBool("isSpeaking", false);
				animator.SetBool("isFacingLeft", false);
				animator.SetBool("isFacingRight", true);
				break;
			case ActorStates.leftWalking:
				animator.SetBool("isWalking", true);
				animator.SetBool("isSpeaking", false);
				animator.SetBool("isFacingLeft", true);
				animator.SetBool("isFacingRight", false);
				break;
			case ActorStates.rightWalking:
				animator.SetBool("isWalking", true);
				animator.SetBool("isSpeaking", false);
				animator.SetBool("isFacingLeft", false);
				animator.SetBool("isFacingRight", true);
				break;
			case ActorStates.leftSpeaking:
				animator.SetBool("isWalking", false);
				animator.SetBool("isSpeaking", true);
				animator.SetBool("isFacingLeft", true);
				animator.SetBool("isFacingRight", false);
				break;
			case ActorStates.rightSpeaking:
				animator.SetBool("isWalking", true);
				animator.SetBool("isSpeaking", false);
				animator.SetBool("isFacingLeft", false);
				animator.SetBool("isFacingRight", true);
				break;
			//More additional states...
			default:
				break;
			}
		}
		*/
	}


}

public class ActorStates{
	public const int leftIdle = 0;
	public const int rightIdle = 1;
	public const int leftWalking = 2;
	public const int rightWalking = 3;
	public const int leftSpeaking = 4;
	public const int rightSpeaking = 5;

	public int Value {get; set;}
	
	public ActorStates(){}
	
	public ActorStates(int value){
		Value = value;
	}
	
	public static implicit operator int(ActorStates type){
		return type.Value;
	}
	
	public static implicit operator ActorStates(int value){
		return new ActorStates(value);
	}
}
