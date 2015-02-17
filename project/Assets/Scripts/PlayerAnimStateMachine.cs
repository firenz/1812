﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public sealed class PlayerAnimStateMachine : ActorAnimStateMachine {
	private PlayerStates currentState;

	protected override void InitializeAnimData(){
	}

	protected override void Update () {
		if(Player.Instance.IsWalking()){
			if(Player.Instance.IsFacingLeft()){
				OnAnimationStateChange(PlayerStates.leftWalking);
				currentState = PlayerStates.leftWalking;
			}
			else if(Player.Instance.IsFacingRight()){
				OnAnimationStateChange(PlayerStates.rightWalking);
				currentState = PlayerStates.rightWalking;
			}
		}
		else if(Player.Instance.IsSpeaking()){
			if(Player.Instance.IsFacingLeft()){
				OnAnimationStateChange(PlayerStates.leftSpeaking);
				currentState = PlayerStates.leftSpeaking;
			}
			else if(Player.Instance.IsFacingRight()){
				OnAnimationStateChange(PlayerStates.rightSpeaking);
				currentState = PlayerStates.rightSpeaking;
			}
		}
		else if(Player.Instance.IsGrabbingUpperItem()){
			if(Player.Instance.IsFacingLeft()){
				OnAnimationStateChange(PlayerStates.leftUpperGrabbing);
				currentState = PlayerStates.leftUpperGrabbing;
			}
			else if(Player.Instance.IsFacingRight()){
				OnAnimationStateChange(PlayerStates.rightUpperGrabbing);
				currentState = PlayerStates.rightUpperGrabbing;
			}
		}
		else if(Player.Instance.IsGrabbingBottomItem()){
			if(Player.Instance.IsFacingLeft()){
				OnAnimationStateChange(PlayerStates.leftBottomGrabbing);
				currentState = PlayerStates.leftBottomGrabbing;
			}
			else if(Player.Instance.IsFacingRight()){
				OnAnimationStateChange(PlayerStates.rightBottomGrabbing);
				currentState = PlayerStates.rightBottomGrabbing;
			}
		}
		else if(Player.Instance.IsWaiting()){
			if(Player.Instance.IsFacingLeft()){
				OnAnimationStateChange(PlayerStates.leftUsingPhone);
				currentState = PlayerStates.leftUsingPhone;
			}
			else if(Player.Instance.IsFacingRight()){
				OnAnimationStateChange(PlayerStates.rightUsingPhone);
				currentState = PlayerStates.rightUsingPhone;
			}
		}
		else if(Player.Instance.IsIdle()){
			if(Player.Instance.IsFacingLeft()){
				OnAnimationStateChange(PlayerStates.leftIdle);
				currentState = PlayerStates.leftIdle;
			}
			else if(Player.Instance.IsFacingRight()){
				OnAnimationStateChange(PlayerStates.rightIdle);
				currentState = PlayerStates.rightIdle;
			}
		}
	}

	private void OnAnimationStateChange(PlayerStates newState){
		if(newState == currentState){
			return;
		}
		
		switch(newState){
		case PlayerStates.leftIdle:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", false);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
			
		case PlayerStates.rightIdle:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", false);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case PlayerStates.leftWalking:
			animator.SetBool("isWalking", true);
			animator.SetBool("isSpeaking", false);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
			
		case PlayerStates.rightWalking:
			animator.SetBool("isWalking", true);
			animator.SetBool("isSpeaking",false);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case PlayerStates.leftSpeaking:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", true);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
			
		case PlayerStates.rightSpeaking:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", true);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case PlayerStates.rightUpperGrabbing:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", false);
			animator.SetBool("isUpperGrabbing", true);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case PlayerStates.leftUpperGrabbing:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", false);
			animator.SetBool("isUpperGrabbing", true);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
			
		case PlayerStates.rightBottomGrabbing:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", false);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", true);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case PlayerStates.leftBottomGrabbing:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", false);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", true);
			animator.SetBool("isUsingPhone", false);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
			
		case PlayerStates.rightUsingPhone:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", false);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", true);
			animator.SetBool("isFacingLeft", false);
			animator.SetBool("isFacingRight", true);
			break;
			
		case PlayerStates.leftUsingPhone:
			animator.SetBool("isWalking", false);
			animator.SetBool("isSpeaking", false);
			animator.SetBool("isUpperGrabbing", false);
			animator.SetBool("isBottomGrabbing", false);
			animator.SetBool("isUsingPhone", true);
			animator.SetBool("isFacingLeft", true);
			animator.SetBool("isFacingRight", false);
			break;
		}
		
		currentState = newState;
	}
}

public class PlayerStates : ActorStates{
	public const int leftUpperGrabbing = 6;
	public const int rightUpperGrabbing = 7;
	public const int leftBottomGrabbing = 8;
	public const int rightBottomGrabbing = 9;
	public const int leftUsingPhone = 10;
	public const int rightUsingPhone = 11;
	
	public PlayerStates(){}
	
	public PlayerStates(int value){
		Value = value;
	}
	
	public static implicit operator int(PlayerStates type){
		return type.Value;
	}
	
	public static implicit operator PlayerStates(int value){
		return new PlayerStates(value);
	}
}
