using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JournalButton : OptionsMenuButton {
	private GameObject journalMenu;

	protected override void Start(){
		buttonID = "JOURNAL";
		journalMenu = this.transform.parent.parent.Find("JournalMenu").gameObject;
		journalMenu.transform.SetAsFirstSibling();
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		buttonName = LocalizedTextManager.GetLocalizedText(groupID, buttonID, stringID)[0];
		buttonText.text = buttonName;
	}

	public override void OnClick (){
		JournalMenuOnClick();
	}
	
	public void JournalMenuOnClick(){
		EnableMenus();
	}

	public void Enable(){
		isMenuActive = true;
		MenuActivated();
		this.GetComponent<Button>().interactable = false;
		journalMenu.transform.SetAsLastSibling();
	}

	public void Disable(){
		isMenuActive = false;
		EnableButton();
		journalMenu.transform.SetAsFirstSibling();
		journalMenu.transform.FindChild("JournalPanel/Scrollbar").GetComponent<Scrollbar>().value = 1f;
		journalMenu.transform.FindChild("JournalPanel").GetComponent<JournalEntriesManager>().DisableJournalEntryDescription();
	}	

	public override void EnableMenus(){
		DisableButton();
		isMenuActive = true;
		MenuActivated();
		journalMenu.transform.SetAsLastSibling();
	}
	
	public override void DisableMenus(){
		isMenuActive = false;
		EnableButton();
		journalMenu.transform.SetAsFirstSibling();
		journalMenu.transform.FindChild("JournalPanel/Scrollbar").GetComponent<Scrollbar>().value = 1f;
		journalMenu.transform.FindChild("JournalPanel").GetComponent<JournalEntriesManager>().DisableJournalEntryDescription();
	}
}
