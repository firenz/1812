using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JournalEntriesManager : MonoBehaviour {
	private float heightJournalPanel = 0f;
	private float heightJournalButton = 0f;
	private int numberJournalButtons = 0;
	private GameObject scrollBar;
	private GameObject contentPanel;
	private GameObject journalEntryDescription;

	private void OnEnable(){
		JournalEntryButton.onClick += ShowJournalEntryDescription;
	}

	private void OnDisable(){
		JournalEntryButton.onClick -= ShowJournalEntryDescription;
	}

	// Use this for initialization
	private void Start () {
		scrollBar = this.transform.FindChild("Scrollbar").gameObject;
		contentPanel = this.transform.FindChild("ScrollView/ContentPanel").gameObject;
		journalEntryDescription = this.transform.parent.FindChild("JournalEntryDescription").gameObject;
		journalEntryDescription.transform.SetAsFirstSibling();

		CheckGameStateForAddingJournalEntries();

		heightJournalButton = this.transform.FindChild("ScrollView/ContentPanel/JournalEntryButton").GetComponent<LayoutElement>().minHeight;
		heightJournalPanel = this.transform.FindChild("ScrollView").GetComponent<LayoutElement>().minHeight;
		numberJournalButtons = contentPanel.transform.childCount;

		if((numberJournalButtons * heightJournalButton) > heightJournalPanel){
			scrollBar.SetActive(true);
		}

	}

	public void CheckGameStateForAddingJournalEntries(){
		//Depending of the played cutscenes and other game events, journal entries are added

		//if(GameState.CutSceneData.isPlayedIntro){
			AddJournalEntry(1);
		//}
	}

	public void AddJournalEntry(int id){
		GameObject _newEntry = Instantiate(Resources.Load<GameObject>("Prefabs/GUI/JournalEntryButton"));
		_newEntry.name = "JournalEntryButton";
		_newEntry.transform.SetParent(contentPanel.transform, false);
		_newEntry.GetComponent<JournalEntryButton>().Initialize(id);
	}

	public void ShowJournalEntryDescription(int entryID){
		string _entryJournalDescID = "ENTRY_" + entryID + "_DESC";
		journalEntryDescription.transform.FindChild("Text").GetComponent<Text>().text = LocalizedTextManager.GetLocalizedText("OPTIONS_MENU", "JOURNAL", _entryJournalDescID)[0];
		journalEntryDescription.transform.SetAsLastSibling();
	}

	public void DisableJournalEntryDescription(){
		journalEntryDescription.transform.SetAsFirstSibling();
	}
}
