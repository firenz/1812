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

public class Player : Actor {	
	public bool isInteracting = false;
	//public bool hasEndedManipulatingMechanism = true;
	public bool isGrabbingUpperObject = false;
	public bool isGrabbingBottomObject = false;
	public bool hasEndedGrabbing = true;

	public void Action(InteractiveObject interactiveObject){
		Debug.Log("Action");
		interactiveObject.isInteractiveObjectMechanismActivated = true;
		interactiveObject.Mechanism();
	}

	void GrabBottomObject(InteractiveObject interactiveObject){
		isGrabbingBottomObject = true;
		hasEndedGrabbing = false;
		//To be implemented
	}
	
	void GrabUpperObject(InteractiveObject interactiveObject){
		isGrabbingUpperObject = true;
		//To be implemented
	}
	
	public void Describe(InteractiveObject interactiveObject){
		Debug.Log("Describe");
		StartCoroutine(WaitForDescribeCompleted(interactiveObject));
	}

	public void Interact(InteractiveObject interactiveObject){
		Debug.Log("Interact");
		StartCoroutine(WaitForInteractionCompleted(interactiveObject));
	}

	private IEnumerator WaitForInteractionCompleted(InteractiveObject interactiveObject){
		Debug.Log("WaitForInteractionCompleted");
		yield return StartCoroutine(WaitForGoToDirectionCompleted(interactiveObject.position));
		
		InteractiveObject.interactiveTypes typeInteractiveObject = interactiveObject.currentType;

		switch(typeInteractiveObject){
		case InteractiveObject.interactiveTypes.examinable:
			Debug.Log("WaitForInteractionCompleted:examinable");
			yield return StartCoroutine(WaitForSpeakCompleted(interactiveObject.Examination()));
			break;
		case InteractiveObject.interactiveTypes.interactiveButNotPickable:
			Debug.Log("WaitForInteractionCompleted:interactiveButNotPickable");
			yield return StartCoroutine(WaitForActionCompleted(interactiveObject));
			break;
		case InteractiveObject.interactiveTypes.bottomPickable:
			Debug.Log("WaitForInteractionCompleted:bottomPickable");
			//this.GrabBottomObject(interactiveObject); To be implemented
			break;
		case InteractiveObject.interactiveTypes.upperPickable:
			Debug.Log("WaitForInteractionCompleted:upperPickable");
			//this.GrabUpperObject(interactiveObject); To be implemented
			break;
		}
		isInteracting = false;
	}

	private IEnumerator WaitForDescribeCompleted(InteractiveObject interactiveObject){
		Debug.Log("WaitForDescribe");
		yield return StartCoroutine(WaitForGoToDirectionCompleted(interactiveObject.position));

		yield return StartCoroutine(WaitForSpeakCompleted(interactiveObject.Description()));
	}

	private IEnumerator WaitForGoToDirectionCompleted(Vector2 directionToGo){
		Debug.Log("WaitForGoToDirection");
		this.GoTo(directionToGo);
		do{
			yield return null;
		}while(!isDirectionToGoReached);
	}

	private IEnumerator WaitForSpeakCompleted(List<string> conversation){
		Debug.Log("WaitForSpeak");
		this.Speak(conversation);
		do{
			yield return null;
		}while(!hasEndedSpeaking);
	}

	private IEnumerator WaitForActivationObjectMechanismCompleted(InteractiveObject interactiveObject){
		isGrabbingUpperObject = true; //For testing purposes, the name will change later to isManipulatingUpperObject
		hasEndedGrabbing = false; //For testing purposes, the name will change later to hasEndedManipulatingUpperObject
		yield return new WaitForSeconds(0.7f);
		Debug.Log("WaitForActivationObjectMechanismCompleted");
		isGrabbingUpperObject = false;
		hasEndedGrabbing = true;
		this.Action(interactiveObject);
		do{
			yield return null;
		}while(interactiveObject.isInteractiveObjectMechanismActivated);
		Debug.Log("WaitForActivationObjectMechanismCompleted:afterIsMechanismActivated");

	}

	private IEnumerator WaitForActionCompleted(InteractiveObject interactiveObject){
		Debug.Log("WaitForActionCompleted");
		yield return StartCoroutine(WaitForSpeakCompleted(interactiveObject.Examination()));
		isInteracting = true;
		Debug.Log("WaitForActionCompleted:afterExamination");
		yield return StartCoroutine(WaitForActivationObjectMechanismCompleted(interactiveObject));
		Debug.Log("WaitForActionCompleted:afterActivation");
		isInteracting = false;
		}


}
