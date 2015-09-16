using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Transform))]
public class JournalEntryDescriptionButton : UIGenericButton {
	private Transform thisTransform;
	private UIGenericScrollbar thisScrollbar;

	protected override void OnEnable(){
		base.OnEnable();
		ReturnToButton.resetUIPositions += ResetPosition;
		JournalEntryButton.showJournalEntry += ShowDescription;
	}

	protected override void OnDisable(){
		base.OnDisable();
		ReturnToButton.resetUIPositions -= ResetPosition;
		JournalEntryButton.showJournalEntry -= ShowDescription;
	}

	protected override void Start(){
		thisButton = this.GetComponent<Button>();
		buttonText = this.transform.FindChild("ScrollView/Text").GetComponent<Text>();
		thisScrollbar = this.transform.FindChild("Scrollbar").GetComponent<UIGenericScrollbar>();
		thisTransform = this.GetComponent<Transform>();
		ResetPosition();
	}

	protected override void ActionOnClick (){
		ResetPosition();
	}

	private void ShowDescription(int number){
		List<string> _descriptionLocalized = new List<string>();
		string _description = "";

		localizedTextStringID = JournalEntriesManager.prefixJournalEntry + number + JournalEntriesManager.sufixJournalEntryDesc;
		_descriptionLocalized = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID);

		foreach(string line in _descriptionLocalized){
			_description += line + "\n";
		}

		if(_description.Length <= 252){
			thisScrollbar.gameObject.SetActive(false);
		}

		buttonText.text = _description;

		thisTransform.SetAsLastSibling();
	}

	private void ResetPosition(){
		thisTransform.SetAsFirstSibling();
		buttonText.text = "";
		thisScrollbar.gameObject.SetActive(true);
		thisScrollbar.ResetPosition();
	}

	protected override void ChangeButtonTextLanguage(){}	
}
