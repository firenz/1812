﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JournalEntryButton : UIGenericButton {
	private int entryID;

	public delegate void OnClickJournalEntry(int entryID);
	public static event OnClickJournalEntry onClick;

	private void OnEnable(){
		LocalizedTextManager.currentLanguageChanged += ChangeLanguage;
	}
	
	private void OnDisable(){
		LocalizedTextManager.currentLanguageChanged -= ChangeLanguage;
	}

	private void ChangeLanguage(){
		string strindID = "ENTRY_" + entryID;
		this.transform.FindChild("Text").GetComponent<Text>().text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "JOURNAL", strindID)[0];
	}

	public void Initialize(int id){
		entryID = id;
		string strindID = "ENTRY_" + entryID;
		this.transform.FindChild("Text").GetComponent<Text>().text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "JOURNAL", strindID)[0];
	}

	public void EntryJournalOnClick(){
		onClick(entryID);
	}
}