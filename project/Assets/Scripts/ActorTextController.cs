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

[RequireComponent (typeof (GUIText))]
public class ActorTextController : MonoBehaviour {
	//public LanguageTextLocalization localizationScript; referencia para el futuro
	private Actor actor;
	public float secondsDisplayingText = 1.5f;
	private float spriteWidth = 40f;
	private float spriteHeight = 62f;
	private float halfSpriteWidth;
	//private int maxCharactersPerLine = 15;
	//private int maxWordsPerLine = 3;
	
	// Use this for initialization
	void Start () {
		actor = this.GetComponentInParent<Actor>();
		this.GetComponent<GUIText>().text = "";
		halfSpriteWidth = 18f;
	}
	
	// Update is called once per frame
	void Update () {
		FollowActor();
	}

	void FollowActor(){
		this.transform.position = Camera.main.WorldToViewportPoint(new Vector3(actor.currentPosition.x + halfSpriteWidth, actor.currentPosition.y + spriteHeight, 0.0f));
	}

	protected IEnumerator WaitForDialogueToBeEnded(List<string> conversation){
		actor.hasEndedSpeaking = false;
		foreach(string lineDialogue in conversation){
			this.GetComponent<GUIText>().text = lineDialogue;
			yield return new WaitForSeconds(secondsDisplayingText);
		}
		this.GetComponent<GUIText>().text = "";
		actor.hasEndedSpeaking = true;
		actor.isSpeaking = false;
	}

	public void UpdateTextDialogue(List<string> conversation){
		StartCoroutine(WaitForDialogueToBeEnded(conversation));
	}
}
