﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public abstract class ActorAnimStateMachine : MonoBehaviour {

	protected Actor actor;
	protected ActorStates currentAnimationState;

	protected Animator animator;
	
	protected void Start () {
		actor = this.gameObject.GetComponent<Actor>();
		animator = GetComponent<Animator>();
		currentAnimationState = ActorStates.leftIdle;
		OnAnimationStateChange(currentAnimationState);
		InitializeAnimData();
	}

	protected abstract void InitializeAnimData();

	protected virtual void Update () {
		if(actor.IsWalking()){
			if(actor.IsFacingLeft()){
				OnAnimationStateChange(ActorStates.leftWalking);
				currentAnimationState = ActorStates.leftWalking;
			}
			else if(actor.IsFacingRight()){
				OnAnimationStateChange(ActorStates.rightWalking);
				currentAnimationState = ActorStates.rightWalking;
			}
		}
		else if(actor.IsSpeaking()){
			if(actor.IsFacingLeft()){
				OnAnimationStateChange(ActorStates.leftSpeaking);
				currentAnimationState = ActorStates.leftSpeaking;
			}
			else if(actor.IsFacingRight()){
				OnAnimationStateChange(ActorStates.rightSpeaking);
				currentAnimationState = ActorStates.rightSpeaking;
			}
		}
		else /*if(actor.IsIdle())*/{
			if(actor.IsFacingLeft()){
				OnAnimationStateChange(ActorStates.leftIdle);
				currentAnimationState = ActorStates.leftIdle;
			}
			else if(actor.IsFacingRight()){
				OnAnimationStateChange(ActorStates.rightIdle);
				currentAnimationState = ActorStates.rightIdle;
			}
		}
	}

	protected virtual void OnAnimationStateChange(ActorStates newState){
		//This method needs to be overrided if we use additional animation states
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

/*
public class ActorStates{
	public int Value {get; set;}
	
	public int leftIdle = 0;
	public int rightIdle = 1;
	public int leftWalking = 2;
	public int rightWalking = 3;
	public int leftSpeaking = 4;
	public int rightSpeaking = 5;


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
*/


public class ActorStates{
	public int Value {get; set;}
	
	public const int leftIdle = 0;
	public const int rightIdle = 1;
	public const int leftWalking = 2;
	public const int rightWalking = 3;
	public const int leftSpeaking = 4;
	public const int rightSpeaking = 5;
	
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