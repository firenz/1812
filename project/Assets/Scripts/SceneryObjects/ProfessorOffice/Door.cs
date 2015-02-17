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

public class Door : WarperElement {

	protected override void InitializeInformation(){
		//Write here the info for your interactive object
		nameSceneDestination = "DemoScene_01";

		groupID = "SCENE_PROFESSOROFFICE";
		nameID = "OBJECT_DOOR";
	}

	protected override IEnumerator WaitForLeftClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.CurrentPosition(), interactivePosition));
		if(_distanceBetweenActorAndInteractivePosition >= permisiveErrorBetweenPlayerPositionAndInteractivePosition){
			Player.Instance.GoTo(interactivePosition);
			
			do{
				yield return null;
			}while(Player.Instance.IsWalking());
			
		}
		
		if(Player.Instance.LastTargetedPosition() == interactivePosition){
			Player.Instance.SetInteractionActive();

			yield return new WaitForSeconds(0.1f);
			Player.Instance.LookToTheRight();
			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			
			do{
				yield return null;
			}while(Player.Instance.IsSpeaking());
			
			Player.Instance.SetInteractionInactive();
			
			GameController.WarpToLevel(nameSceneDestination);
		}
	}
}
