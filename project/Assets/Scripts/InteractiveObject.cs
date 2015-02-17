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

public class InteractiveObject : MonoBehaviour {
	public Vector2 position;

	public enum interactiveTypes{
		examinable = 0,
		interactiveButNotPickable,
		upperPickable,
        bottomPickable,
    }
	public interactiveTypes currentType = interactiveTypes.examinable;

	protected List<string> definitionText = null;
	protected List<string> interactionText = null;
	protected List<string> givableObjectNameForPlayer = null; //For pickable object class
	protected bool isGrabbableObjectActive = true; //For pickable object class
	public bool isInteractiveObjectMechanismActivated = false; //For interactive object with mechanism class

	// Use this for initialization
	protected void Start () {
		position = new Vector2(this.transform.GetChild(0).position.x, this.transform.GetChild(0).position.y);
		definitionText = new List<string>();
		interactionText = new List<string>();
		givableObjectNameForPlayer = new List<string>();
		InitializeInformation();
	}

	protected virtual void InitializeInformation(){
		//Write here the info for your interactive object
	}

	public List<string> Description(){
		return definitionText;
	}

	public List<string> Examination(){
		return interactionText;
	}

	public List<string> Grab(){
		if(isGrabbableObjectActive){
			//this.GetComponent<SpriteRenderer>().enabled = false;
			isGrabbableObjectActive  = false;
		}
		return givableObjectNameForPlayer;
	}

	public virtual void Mechanism(){
		// Interaction depends of object
	}

}
