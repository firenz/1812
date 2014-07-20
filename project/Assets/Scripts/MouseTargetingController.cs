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

public class MouseTargetingController : MonoBehaviour {
	public Player player;

	private Vector3 mousePosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePosition.x,mousePosition.y), Vector2.zero, 0f);

			if(hit.collider != null){
				if(hit.collider.transform.tag == "InteractivePoint"){
					player.Describe(hit.collider.gameObject.GetComponent<InteractiveObject>());
				}
				else if(hit.collider.transform.tag == "NavigationPolygon"){
					player.GoTo(mousePosition);
				}
			}
		}
		else if(Input.GetMouseButtonDown(1)){
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePosition.x,mousePosition.y), Vector2.zero, 0f);

			if(hit.collider != null){
				if(hit.collider.transform.tag == "InteractivePoint"){
					player.Interact(hit.collider.gameObject.GetComponent<InteractiveObject>());
				}
				else if(hit.collider.transform.tag == "NavigationPolygon"){
					List<string> textToDisplayIfNothingIsInteresting = new List<string>();
					textToDisplayIfNothingIsInteresting.Add("Ahi no hay");
					textToDisplayIfNothingIsInteresting.Add("nada que me");
					textToDisplayIfNothingIsInteresting.Add("interese");
					player.Speak(textToDisplayIfNothingIsInteresting);
				}
			}
		}
	}

}
