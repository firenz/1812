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

public class Window : PickableElement {	
	public bool isOpened;

	protected override void InitializePickableInformation(){
		groupID = "SCENE_PROFESSOROFFICE";
		nameID = "OBJECT_WINDOW";

		currentPositionType = positionTypes.upper;

		isOpened = GameState.LevelProfessorOfficeData.isWindowOpened;

		if(isOpened){
			this.Open();
		}
		else{
			this.Close();
		}
	}

	protected override IEnumerator ManipulatingObject(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());
		}

        if(Player.Instance.LastTargetedPosition() == interactivePosition){
			Player.Instance.isDoingAction = true;

            if(isOpened){
                Player.Instance.Speak(groupID, nameID, "INTERACTION_OPENED");
			}
			else{
				Player.Instance.Speak(groupID, nameID, "INTERACTION_CLOSED");
			}

			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());

			//Player.Instance.SetUpperInteractionActive();

			Player.Instance.UpperInteraction(this);

            //Player.Instance.SetInteractionInactive();
			Player.Instance.isDoingAction = false;
        }
    }

	public override void OnPlayerTouchingAction(){
		if(isOpened){
			this.Close();
		}
		else{
			this.Open();
		}
	}
    
    public override void ActionOnItemInventoryUsed(string nameItemInventory){
		Debug.Log("ActionOnItemInventaryUsed: " + nameItemInventory);
		switch(nameItemInventory){
		case "FailedTestInventory":
			StartCoroutine(FailedTestOnWindow());
			break;
		default:
			break;
		}
	}
	
	private IEnumerator FailedTestOnWindow(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());
		}
		if(Player.Instance.LastTargetedPosition() == interactivePosition){
			Player.Instance.isDoingAction = true;
			Player.Instance.SetInteractionActive();
			if(isOpened){
				Player.Instance.Speak(groupID, nameID, "INTERACTION_OPENED_FAILEDTEST");

				do{
					yield return null;
				}while(Player.Instance.IsSpeaking());

				Inventory.Instance.RemoveItem("FailedTestInventory");
			}
			else{
				Player.Instance.Speak(groupID, nameID, "INTERACTION_CLOSED_FAILEDTEST");

				do{
					yield return null;
				}while(Player.Instance.IsSpeaking());
			}
			Player.Instance.SetInteractionInactive();
			Player.Instance.isDoingAction = false;
		}
	}

	private void Close(){
		isOpened = false;
		this.gameObject.GetComponent<Renderer>().enabled = true;
	}

	private void Open(){
		isOpened = true;
		this.gameObject.GetComponent<Renderer>().enabled = false;
	}

	public bool IsOpened(){
		return isOpened;
	}
}
