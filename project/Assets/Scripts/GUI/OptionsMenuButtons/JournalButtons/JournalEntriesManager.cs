using UnityEngine;
using System.Collections;

public class JournalEntriesManager : MonoBehaviour {
	public static string localizedTextGroupID { get; private set;}
	public static string localizedTextButtonID { get; private set;}
	public static string prefixJournalEntry { get; private set;}
	public static string sufixJournalEntryDesc { get; private set;}

	[SerializeField]
	private string pathPrefabs = "Prefabs/GUI/";

	private GameObject scrollBar;
	private Transform contentPanel;
	
	private void Start () {
		localizedTextGroupID = "OPTIONS_MENU";
		localizedTextButtonID = "JOURNAL";
		prefixJournalEntry = "ENTRY_";
		sufixJournalEntryDesc = "_DESC";
		scrollBar = this.transform.FindChild("Scrollbar").gameObject;
		contentPanel = this.transform.FindChild("ScrollView/ContentPanel");

		CheckGameStateForAddingJournalEntries();

		int _numberEntriesInitialized = contentPanel.childCount;

		if(_numberEntriesInitialized < 4){
			scrollBar.SetActive(false);
		}
	}

	private void CheckGameStateForAddingJournalEntries(){
		//Depending of the played cutscenes and other game events, journal entries are added

		AddJournalEntry(1);
		AddJournalEntry(2);

		if(GameState.CutSceneData.isPlayedIntroProfessorOfficePart){
			AddJournalEntry(3);
		}

		//...
	}

	public void AddJournalEntry(int numberID){
		GameObject _newEntry = Instantiate(Resources.Load<GameObject>(pathPrefabs + "JournalEntryButton"));
		_newEntry.name = "JournalEntryButton_" + numberID;
		_newEntry.transform.SetParent(contentPanel, false);
		_newEntry.GetComponent<JournalEntryButton>().Initialize(numberID);
	}
}
