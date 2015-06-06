using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DisplayUITextMultipleChoice : UIGenericButton {
	public Color mouseOverColor;
	private Color defaultColor;
	private Text multipleChoiceText = null;
	private int choiceID = 0;

	public delegate void SelectedChoiceResultEvent(int choiceID);
	public static event SelectedChoiceResultEvent selectedChoiceResult;

	// Use this for initialization
	private void Awake () {
		multipleChoiceText = this.GetComponent<Text>();
		//multipleChoiceText.text = "";
		defaultColor = multipleChoiceText.color;
		//Initialize("LIBRARIAN_CONVERSATION_CHOICE", 0);
	}

	protected override void Update (){
		if(isMouseOver){
			CustomCursorController.Instance.ChangeCursorOverInteractiveElement();
		}
	}

	public void Initialize(string choiceNameID, int numberID){
		string stringID = "CHOICE_" + numberID;
		Debug.Log("Initialize: choiceNameID: " + choiceNameID + " stringID: " + stringID);
		multipleChoiceText.text = LocalizedTextManager.GetLocalizedText("CHOICES", choiceNameID, stringID)[0];
        choiceID = numberID;
	}

	public override void OnMouseOver(){
		base.OnMouseOver();
		multipleChoiceText.color = mouseOverColor;
	}

	public override void OnMouseExit(){
		base.OnMouseExit();
		multipleChoiceText.color = defaultColor;
	}

	public void SelectedChoiceOnMouseDown(){
		selectedChoiceResult(choiceID);
	}
}
