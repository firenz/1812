using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameSlotButton : UIGenericButton {
	public int numberSlot { get; private set;}
	public bool isSlotEmpty { get; private set;}

	private Color defaultNormalBtnColor;
	private string defaultSlotText = "";
	private string lastScenePlayedName = "";
	private string lastScenePlayedID = "";
	private DateTime lastTime;
	private SavesGameSlotsManager.styleSlotInformation slotStyle;

	public delegate void UnSelectAllGameSlotButtons();
	public static event UnSelectAllGameSlotButtons unSelectAllGameSlots;
	public delegate void OnSavedGameSlotClick(int numberSlot);
	public static event OnSavedGameSlotClick onSavedGameSlotClick;

	protected override void OnEnable(){
		base.OnEnable();
		SavesGameSlotsManager.unSelectAllGameSlots += UnSelected;
		GameSlotButton.unSelectAllGameSlots += UnSelected;
	}
	
	protected override void OnDisable(){
		base.OnDisable();
		SavesGameSlotsManager.unSelectAllGameSlots -= UnSelected;
		GameSlotButton.unSelectAllGameSlots -= UnSelected;
	}
	
	protected override void Start () {
		thisButton = this.GetComponent<Button>();
		buttonText = this.transform.FindChild("Text").GetComponent<Text>();
		defaultNormalBtnColor = thisButton.image.color;
	}

	public void Initialize(int number){
		Start();

		numberSlot = number;
		defaultSlotText = LocatedTextManager.GetLocatedText("OPTIONS_MENU", "SAVED_GAMES", "EMPTY_SLOT")[0];
		buttonText.text = "Slot " + numberSlot + "\n" + defaultSlotText;
		isSlotEmpty = true;
	}

	public void Initialize(int number, string playableSceneName, DateTime datetime, SavesGameSlotsManager.styleSlotInformation mode){
		Start();

		numberSlot = number;
		lastScenePlayedName = playableSceneName;
		lastTime = datetime;
		lastScenePlayedID = GameController.UnitySceneNameToSceneNameID(lastScenePlayedName);
		slotStyle = mode;

		string _localizedLastTimeIntroText = LocatedTextManager.GetLocatedText("OPTIONS_MENU", "SAVED_GAMES", "LAST_TIME")[0]; 
		string _localizedLastPlaceIntroText = LocatedTextManager.GetLocatedText("OPTIONS_MENU", "SAVED_GAMES", "LAST_PLACE")[0];
		string _lastPlace = LocatedTextManager.GetLocatedText("LIST_PLAYABLE_SCENES", lastScenePlayedID, "NAME")[0];
		string _lastTimePlayed = lastTime.ToString("dd/MM/yyyy HH:mm");


		if(slotStyle == SavesGameSlotsManager.styleSlotInformation.InGameMenuStyle){
			buttonText.text = "Slot " + numberSlot + " \n";
			buttonText.text += _localizedLastPlaceIntroText + ":\n" + _lastPlace + "\n" + _localizedLastTimeIntroText + ": " + _lastTimePlayed;
		}
		else{
			buttonText.text = "Slot " + numberSlot + " \n";
			buttonText.text += _localizedLastPlaceIntroText + ": " + _lastPlace + "\n" + _localizedLastTimeIntroText + ": " + _lastTimePlayed;
		}

		isSlotEmpty = false;
	}

	protected override void ActionOnClick (){
		unSelectAllGameSlots();
		Selected();
		onSavedGameSlotClick(numberSlot);
	}

	protected override void ChangeButtonTextLanguage(){
		if(isSlotEmpty){
			defaultSlotText = LocatedTextManager.GetLocatedText("OPTIONS_MENU", "SAVED_GAMES", "EMPTY_SLOT")[0];
			buttonText.text = "Slot " + numberSlot + ":\n" + defaultSlotText;
		}
		else{
			string _localizedLastTimeIntroText = LocatedTextManager.GetLocatedText("OPTIONS_MENU", "SAVED_GAMES", "LAST_TIME")[0]; 
			string _localizedLastPlaceIntroText = LocatedTextManager.GetLocatedText("OPTIONS_MENU", "SAVED_GAMES", "LAST_PLACE")[0];
			string _lastPlace = LocatedTextManager.GetLocatedText("LIST_PLAYABLE_SCENES", lastScenePlayedID, "NAME")[0];
			string _lastTimePlayed = lastTime.ToString("dd/MM/yyyy HH:mm");

			buttonText.text = "Slot " + numberSlot + " \n";

			if(slotStyle == SavesGameSlotsManager.styleSlotInformation.InGameMenuStyle){
				buttonText.text += _localizedLastPlaceIntroText + ":\n" + _lastPlace + "\n" + _localizedLastTimeIntroText + ": " + _lastTimePlayed;
			}
			else{
				buttonText.text += _localizedLastPlaceIntroText + ": " + _lastPlace + "\n" + _localizedLastTimeIntroText + ": " + _lastTimePlayed;
			}
        }
    }

	public void Selected(){
		thisButton.image.color = thisButton.colors.highlightedColor;
	}

	public void UnSelected(){
		thisButton.image.color = defaultNormalBtnColor;
	}

	public void ClearSlot(){
		isSlotEmpty = true;
		Initialize(numberSlot);
	}
}
