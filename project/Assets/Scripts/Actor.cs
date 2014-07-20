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
using System.Collections.Generic;

[RequireComponent (typeof (SpriteRenderer))]
public class Actor : MonoBehaviour {
	//public Inventary inventary; To be implemented
	protected ActorTextController actorTextScript;
	public Vector2 currentPosition;
	public Vector2 directionToGo;
	public Vector2 moveDirection;
	public bool isDirectionToGoReached = true;
	public bool isSpeaking = false;
	public bool hasEndedSpeaking = true;

	protected float distanceToDirectionToGo = 0f;
	protected float spriteWidth = 40f;
	protected float permisiveErrorDistanceInReachingDirectionToGo = 2f;
	protected Transform thisTransform;

	// Use this for initialization
	protected void Start () {
		actorTextScript = this.GetComponentInChildren<ActorTextController>();
		thisTransform = this.transform;
		currentPosition = thisTransform.position;
		moveDirection = Vector2.zero;
		directionToGo = this.transform.position;
		//spriteWidth = this.calculateSpriteWidth();
	}
	
	// Update is called once per frame
	protected void Update () {
		if(!isDirectionToGoReached){
			distanceToDirectionToGo = Vector2.Distance(currentPosition, directionToGo);
			
			if(distanceToDirectionToGo > permisiveErrorDistanceInReachingDirectionToGo){
				currentPosition = thisTransform.position;
				thisTransform.position = new Vector3(thisTransform.position.x + moveDirection.x, thisTransform.position.y + moveDirection.y, 0.0f);
				
			}
			else{
				moveDirection = Vector2.zero;
				isDirectionToGoReached = true;
			}
		}
		else if(isDirectionToGoReached){
			this.Action();
		}
	}

	protected virtual void Action(){
		//Depending of the type of the actor the disponible actions are different
	}

	public void Speak(List<string> conversation){
		isSpeaking = true;
		actorTextScript.UpdateTextDialogue(conversation);
	}



	public void GoTo(Vector2 direction){
		if(hasEndedSpeaking){
			isDirectionToGoReached = false;
			directionToGo = direction;
			
			//Navigation calculation should be here
			
			if(directionToGo.x > this.transform.position.x){
				directionToGo.x -= spriteWidth;
			}
			
			moveDirection = directionToGo - currentPosition;
			moveDirection.Normalize();
		}
	}

	protected float calculateSpriteWidth(){
		SpriteRenderer thisSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		return thisSpriteRenderer.sprite.border.z - thisSpriteRenderer.sprite.border.x;
	}
}
