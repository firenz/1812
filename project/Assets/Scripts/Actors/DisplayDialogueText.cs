using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DisplayDialogueText : MonoBehaviour {
	public const float secondsDisplayingText = 2.5f;
	public bool hasEndedDisplayingText{ get; private set;}

	private float timeDisplayingDialogueText;
	private bool skipDisplayingTextLine = false;
	private bool ableToSkip = true;

	private Image actorDialoguePanelImage;
	private Text actorDialogueText = null;
	private string displayingText = "";
	private Color defaultDialoguePanelColor;
	private Color defaultTextColor;

	private void Start () {
		Canvas _canvas = this.transform.FindChild("Canvas").GetComponent<Canvas>();
		_canvas.worldCamera = Camera.main;
		GameObject _actorDialoguePanel = _canvas.transform.FindChild("Panel").gameObject;
		actorDialoguePanelImage = _actorDialoguePanel.GetComponent<Image>();
		defaultDialoguePanelColor = actorDialoguePanelImage.color;
		actorDialoguePanelImage.color = Color.clear;
		actorDialogueText = _actorDialoguePanel.transform.FindChild("ActorUIText").GetComponent<Text>();
		actorDialogueText.text = "";
		defaultTextColor = actorDialogueText.color;
	}

	private void Update(){
		if(displayingText == ""){
			actorDialogueText.text = "";
			actorDialoguePanelImage.color = Color.clear;
		}
		else{
			actorDialoguePanelImage.color = defaultDialoguePanelColor;

			if(actorDialogueText.text != displayingText){
				actorDialogueText.text = displayingText;
			}

			if(Input.GetMouseButtonDown(0) && ableToSkip){
				skipDisplayingTextLine = true;
			}
		}
	}

	public void DisplayText(string groupID, string nameID, string stringID, Color textColor, bool skipTextByClick = true, int maxCharPerLine = 22, int maxNewLinesPerDialogue = 1){
		StartCoroutine(WaitForDisplayTextToFinish(groupID, nameID, stringID, textColor, skipTextByClick, maxCharPerLine, maxNewLinesPerDialogue));
	}

	public void DisplayText(string groupID, string nameID, string stringID, bool skipTextByClick = true, int maxCharPerLine = 22, int maxNewLinesPerDialogue = 1){
		StartCoroutine(WaitForDisplayTextToFinish(groupID, nameID, stringID, defaultTextColor, skipTextByClick, maxCharPerLine, maxNewLinesPerDialogue));
	}
	
	public void DisplayText(List<string> dialogueData, Color textColor, bool skipTextByClick = true, int maxCharPerLine = 22, int maxNewLinesPerDialogue = 1){
		StartCoroutine(WaitForDisplayTextToFinish(dialogueData[0], dialogueData[1], dialogueData[2], textColor, skipTextByClick, maxCharPerLine, maxNewLinesPerDialogue));
	}

	public void DisplayText(List<string> dialogueData, bool skipTextByClick = true, int maxCharPerLine = 22, int maxNewLinesPerDialogue = 1){
		StartCoroutine(WaitForDisplayTextToFinish(dialogueData[0], dialogueData[1], dialogueData[2], defaultTextColor, skipTextByClick, maxCharPerLine, maxNewLinesPerDialogue));
	}

	private IEnumerator WaitForDisplayTextToFinish(string groupID, string nameID, string stringID, Color textColor, bool skipTextByClick, int maxCharPerLine, int maxNewLinesPerDialogue){
		ableToSkip = skipTextByClick;
		actorDialogueText.color = textColor;
		hasEndedDisplayingText = false;

		List<string> _localizedAndFormatedDialogue = new List<string>();
		_localizedAndFormatedDialogue = LocatedTextManager.GetLocatedTextFormatted(groupID, nameID, stringID, maxCharPerLine, maxNewLinesPerDialogue);

		foreach(string textLineInDialogue in _localizedAndFormatedDialogue){
			timeDisplayingDialogueText = Time.time;
			skipDisplayingTextLine = false;
			displayingText = textLineInDialogue;
		
			do{
				yield return null;
			}while((Time.time - timeDisplayingDialogueText) <= secondsDisplayingText && !skipDisplayingTextLine);
		}
		displayingText = "";
		hasEndedDisplayingText = true;
		ableToSkip = true;
	}

	public bool HasEndedDisplayingText(){
		return hasEndedDisplayingText;
	}
}
