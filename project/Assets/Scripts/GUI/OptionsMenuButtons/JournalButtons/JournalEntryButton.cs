using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JournalEntryButton : UIGenericButton {
	private string localizedTextEntryID;
	private int numberID;

	public delegate void ShowJournalEntry(int numberID);
	public static event ShowJournalEntry showJournalEntry;

	protected override void Start (){
		thisButton = this.GetComponent<Button>();
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
	}

	public void Initialize(int number){
		Start();

		localizedTextGroupID = JournalEntriesManager.localizedTextGroupID;
		localizedTextElementID = JournalEntriesManager.localizedTextButtonID;
		numberID = number;
		localizedTextEntryID = JournalEntriesManager.prefixJournalEntry + numberID;
		buttonText.text = LocatedTextManager.GetLocatedText(JournalEntriesManager.localizedTextGroupID, JournalEntriesManager.localizedTextButtonID, localizedTextEntryID)[0];
	}

	protected override void ActionOnClick (){
		showJournalEntry(numberID);
	}

	protected override void ChangeButtonTextLanguage (){
		buttonText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextEntryID)[0];
	}
}
