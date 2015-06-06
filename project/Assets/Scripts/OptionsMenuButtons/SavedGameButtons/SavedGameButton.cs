using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SavedGameButton :OptionsMenuButton {
	private GameObject savedGamesMenu;

	protected override void Start (){
		buttonID = "SAVED_GAMES";
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		buttonName = LocalizedTextManager.GetLocalizedText(groupID, buttonID, stringID)[0];
		buttonText.text = buttonName;
		savedGamesMenu = this.transform.parent.parent.FindChild("SaveGameMenu").gameObject;
		savedGamesMenu.transform.SetAsFirstSibling();
	}
	
	public override void OnClick (){
		EnableMenus();
	}
	
	public override void EnableMenus(){
		base.EnableMenus();
		savedGamesMenu.transform.SetAsLastSibling();
	}
	
	public override void DisableMenus(){
		base.DisableMenus();
		savedGamesMenu.transform.SetAsFirstSibling();
		savedGamesMenu.transform.FindChild("SaveGamePanel").GetComponent<SavesGameSlotsManager>().Disable();
	}
}
