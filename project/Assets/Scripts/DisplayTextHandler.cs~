using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayTextHandler : MonoBehaviour {
	public int maxCharactersPerDisplayingTextLine = 20;
	public float secondsDisplayingText = 1.5f;

	public GUIStyle defaultDisplayingTextStyle;

	private float secondsTimerStarted;
	private bool hasEndedDisplayingText = true;
	private bool skipDisplayingTextLine = false;
	private bool ableToSkip = true;

	private string displayingText = "";
	private Vector2 positionDisplayingText;
	private Rect rectangleOfDisplayingText;
	
	private GUIStyle currentTextStyle;


	public void DisplayText(string groupID, string nameID, string stringID, Vector2 position, bool skipTextByClick = true){
		StartCoroutine(WaitForDisplayTextToFinish(groupID, nameID, stringID, position, skipTextByClick, defaultDisplayingTextStyle));
	}

	public void DisplayText(string groupID, string nameID, string stringID, Vector2 position, GUIStyle textStyle, bool skipTextByClick = true){
		StartCoroutine(WaitForDisplayTextToFinish(groupID, nameID, stringID, position, skipTextByClick, textStyle));
	}

	public void DisplayText(List<string> dialogueData, Vector2 position, bool skipTextByClick = true){
		StartCoroutine(WaitForDisplayTextToFinish(dialogueData[0], dialogueData[1], dialogueData[2], position, skipTextByClick, defaultDisplayingTextStyle));
	}

	public void DisplayText(List<string> dialogueData, Vector2 position, GUIStyle textStyle, bool skipTextByClick = true){
		StartCoroutine(WaitForDisplayTextToFinish(dialogueData[0], dialogueData[1], dialogueData[2], position, skipTextByClick, textStyle));
	}

	private IEnumerator WaitForDisplayTextToFinish(string groupID, string nameID, string stringID, Vector2 position, bool skipTextByClick, GUIStyle textStyle){
		positionDisplayingText = Camera.main.WorldToScreenPoint(new Vector2(position.x, position.y));
		positionDisplayingText.y = Screen.height - positionDisplayingText.y; //Needs to be inverted because Unity handles that way
		ableToSkip = skipTextByClick;

		hasEndedDisplayingText = false;

		currentTextStyle = textStyle;
		//currentTextStyle.fontSize = ScaleAllGUI.ScalateFontSize(textStyle.fontSize); To be fixed in the future
		List<string> _localizedAndFormatedDialogue = new List<string>();
		_localizedAndFormatedDialogue = LocalizedTextManager.GetLocalizedText(groupID, nameID, stringID);
		_localizedAndFormatedDialogue = ReestructureDialogInSmallerTextLines(_localizedAndFormatedDialogue);

		foreach(string textLineInDialogue in _localizedAndFormatedDialogue){
			skipDisplayingTextLine = false;
			displayingText = textLineInDialogue;
			secondsTimerStarted = Time.time;

			do{
				yield return null;
			}while((Time.time - secondsTimerStarted) <= secondsDisplayingText && !skipDisplayingTextLine);
		}

		displayingText = "";
		hasEndedDisplayingText = true;
		ableToSkip = true;
	}

	private List<string> ReestructureDialogInSmallerTextLines(List<string> dialogue){
		List<string> _reestructuredDialogue = new List<string>();
		foreach(string textline in dialogue){
			string[] _words = textline.Split(" "[0]);
			string _resultedLine = "";
			string _word = "";

			for(int index = 0; index < _words.Length; index++){
				_word = _words[index].Trim();

				if(index == 0){
					_resultedLine += _words[0];
				}

				if(index > 0){
					_resultedLine += " " + _word;
				}

				if(_resultedLine.Length > maxCharactersPerDisplayingTextLine){
					_resultedLine = _resultedLine.Substring(0, _resultedLine.Length - (_word.Length));
					_reestructuredDialogue.Add(_resultedLine);
					_resultedLine = _word;
				}
			}

			_reestructuredDialogue.Add(_resultedLine);
		}

		return _reestructuredDialogue;
	}

	public bool HasEndedDisplayingText(){
		return hasEndedDisplayingText;
	}

	private void OnGUI(){
		if(displayingText != ""){
			Event mouseEvent = Event.current;

			if(ableToSkip && mouseEvent.isMouse && mouseEvent.type == EventType.MouseDown){
				skipDisplayingTextLine = true;
			}

			rectangleOfDisplayingText = GUILayoutUtility.GetRect(new GUIContent(displayingText), currentTextStyle);
			rectangleOfDisplayingText.center = positionDisplayingText;
			GUI.Label(new Rect(rectangleOfDisplayingText.x, rectangleOfDisplayingText.y, rectangleOfDisplayingText.width + Screen.width * 0.01f * 2f, rectangleOfDisplayingText.height + Screen.height * 0.01f), displayingText, currentTextStyle);
		}
	}

}
