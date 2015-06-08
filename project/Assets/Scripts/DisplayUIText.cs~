using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DisplayUIText : MonoBehaviour {
	public int maxCharactersPerDisplayingTextLine = 20;
	public float secondsDisplayingText = 1.5f;

	private float secondsTimerStarted;
	private bool hasEndedDisplayingText = true;
	private bool skipDisplayingTextLine = false;
	private bool ableToSkip = true;
	private Color defaultColor;
	private Vector2 positionDisplayingText;
	private RectTransform panelRectTransform;
	private Transform dialogText;
	private GameObject actorDialoguePanel = null;
	private Text actorDialogueText = null;
	private string displayingText = "";

	// Use this for initialization
	void Start () {
		Canvas _canvas = this.transform.FindChild("Canvas").GetComponent<Canvas>();
		_canvas.worldCamera = Camera.main;
		actorDialoguePanel = _canvas.transform.FindChild("Panel").gameObject;
		actorDialogueText = actorDialoguePanel.transform.FindChild("ActorUIText").GetComponent<Text>();
		Vector2 _initialDialogPosition = this.transform.FindChild("dialogText").transform.position;
		dialogText = this.transform.FindChild("dialogText");
		panelRectTransform = actorDialoguePanel.GetComponent<RectTransform>();

		panelRectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(_initialDialogPosition);
		defaultColor = actorDialogueText.color;
		actorDialogueText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(displayingText != ""){
			if(!actorDialoguePanel.gameObject.activeSelf){
				actorDialoguePanel.SetActive(true);
				actorDialogueText.text = displayingText;
			}

			if(actorDialogueText.text != displayingText){
				actorDialogueText.text = displayingText;
			}

			if(Input.GetMouseButtonDown(0) && ableToSkip){
				skipDisplayingTextLine = true;
			}
		}
		else{
			if(actorDialoguePanel.gameObject.activeSelf){
				actorDialoguePanel.SetActive(false);
				actorDialogueText.text = "";
				skipDisplayingTextLine = false;
			}
		}
	}

	public void DisplayText(string groupID, string nameID, string stringID, Vector2 position, Color textColor, bool skipTextByClick = true){
		StartCoroutine(WaitForDisplayTextToFinish(groupID, nameID, stringID, position, textColor, skipTextByClick));
	}

	public void DisplayText(string groupID, string nameID, string stringID, Vector2 position, bool skipTextByClick = true){
		StartCoroutine(WaitForDisplayTextToFinish(groupID, nameID, stringID, position, defaultColor, skipTextByClick));
	}
	
	public void DisplayText(List<string> dialogueData, Vector2 position, Color textColor, bool skipTextByClick = true){
		StartCoroutine(WaitForDisplayTextToFinish(dialogueData[0], dialogueData[1], dialogueData[2], position, textColor, skipTextByClick));
	}

	public void DisplayText(List<string> dialogueData, Vector2 position, bool skipTextByClick = true){
		StartCoroutine(WaitForDisplayTextToFinish(dialogueData[0], dialogueData[1], dialogueData[2], position, defaultColor, skipTextByClick));
	}

	private IEnumerator WaitForDisplayTextToFinish(string groupID, string nameID, string stringID, Vector2 position, Color textColor, bool skipTextByClick){
		positionDisplayingText = Camera.main.WorldToScreenPoint(new Vector2(position.x, position.y));
		//positionDisplayingText = RelocateTextCenterIfNearBorderScreen(positionDisplayingText);
		ableToSkip = skipTextByClick;
		actorDialogueText.color = textColor;
		hasEndedDisplayingText = false;

		List<string> _localizedAndFormatedDialogue = new List<string>();
		_localizedAndFormatedDialogue = LocalizedTextManager.GetLocalizedText(groupID, nameID, stringID);
		_localizedAndFormatedDialogue = ReestructureDialogInSmallerTextLines(_localizedAndFormatedDialogue);
		
		foreach(string textLineInDialogue in _localizedAndFormatedDialogue){
			skipDisplayingTextLine = false;
			displayingText = textLineInDialogue;
			secondsTimerStarted = Time.time;
			//positionDisplayingText = RelocateTextCenterIfNearBorderScreen(positionDisplayingText);

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
}
