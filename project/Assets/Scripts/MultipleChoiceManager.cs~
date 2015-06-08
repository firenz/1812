using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

public class MultipleChoiceManager : Singleton<MultipleChoiceManager> {
	private bool isSelectionEnded = true;
	private int choiceResult = -1;
	private GameObject MultipleChoicePanel = null;

	private void OnEnable(){
		DisplayUITextMultipleChoice.selectedChoiceResult += SelectedChoiceResult;
	}

	private void OnDisable(){
		DisplayUITextMultipleChoice.selectedChoiceResult -= SelectedChoiceResult;
	}

	// Use this for initialization
	private void Start () {
		MultipleChoicePanel = this.gameObject;
		MultipleChoicePanel.GetComponent<VerticalLayoutGroup>().enabled = false;
		//CreateMultipleSelection("LIBRARIAN_CONVERSATION_CHOICE");

	}

	public void CreateMultipleSelection(string choiceNameID){
		choiceResult = -1;
		isSelectionEnded = false;
		int _totalChoices = LocalizedTextManager.GetTextNodesCount("CHOICES", choiceNameID);
		Debug.Log("totalChoices: " + _totalChoices.ToString());
		MultipleChoicePanel.GetComponent<VerticalLayoutGroup>().enabled = true;
		for(int i = 0; i < _totalChoices; i++){
			GameObject _newMultipleChoiceElement = Instantiate(Resources.Load<GameObject>("Prefabs/Other/MultipleChoiceText"));
			_newMultipleChoiceElement.name = "CHOICE_" + i;
			_newMultipleChoiceElement.transform.SetParent(MultipleChoicePanel.transform, false);
			_newMultipleChoiceElement.GetComponent<DisplayUITextMultipleChoice>().Initialize(choiceNameID, i);
		}
	}

	public void SelectedChoiceResult(int choiceID){
		Debug.Log("SelectedChoice Result: " + choiceID);

		choiceResult = choiceID;
		EraseMultipleSelection();
	}

	public void EraseMultipleSelection(){
		foreach(Transform multipleChoiceElement in MultipleChoicePanel.transform){
			Destroy(multipleChoiceElement.gameObject);
		}
		MultipleChoicePanel.GetComponent<VerticalLayoutGroup>().enabled = false;
		isSelectionEnded = true;
	}

	public int GetSelectionResult(){
		if(isSelectionEnded){
			return choiceResult;
		}
		else{
			return -1;
		}
	}

	public bool IsSelectionEnded(){
		return isSelectionEnded;
	}
}
