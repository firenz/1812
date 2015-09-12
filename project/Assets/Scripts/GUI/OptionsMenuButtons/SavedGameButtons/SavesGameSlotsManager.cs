using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SavesGameSlotsManager : MonoBehaviour {
	public int currentGameSlotSelected { get; private set;}
	public int totalUsedSlots { get; private set;}
	
	[SerializeField]
	private string pathSlotPrefab = "Prefabs/GUI/";
	[SerializeField]
	private string directoryGameFiles = "Data/Saves/";
	[SerializeField]
	private styleSlotInformation currentSlotStyle = styleSlotInformation.InGameMenuStyle;

	private const int maxSlots = 6;
	private GameObject gameSlotContentPanel;
	private List<GameSlotButton> gameSlots;

	public enum styleSlotInformation{
		MainMenuStyle,
		InGameMenuStyle
	}

	public delegate void UnSelectAllGameSlotButtons();
	public static event UnSelectAllGameSlotButtons unSelectAllGameSlots;

	private void OnEnable(){
		ReturnToButton.resetUIPositions += ResetGameSlotsPanel;
		GameSlotButton.onSavedGameSlotClick += OnGameSlotClicked;
		SaveButton.saveSlot += Save;
		LoadButton.loadSlot += Load;
		DeleteButton.deleteSlot += Delete;
	}

	private void OnDisable(){
		ReturnToButton.resetUIPositions -= ResetGameSlotsPanel;
		GameSlotButton.onSavedGameSlotClick -= OnGameSlotClicked;
		SaveButton.saveSlot -= Save;
		LoadButton.loadSlot -= Load;
		DeleteButton.deleteSlot -= Delete;
	}

	private void Start () {
		gameSlots = new List<GameSlotButton>();
		totalUsedSlots = 0;
		currentGameSlotSelected = -1;
		gameSlotContentPanel = this.transform.FindChild("ScrollView/ContentPanel").gameObject;

		//Verifies save files and load them in slots in content panel
		if(FileManager.CheckDirectory(directoryGameFiles) == false){
			FileManager.CreateDirectory(directoryGameFiles);
		}

		for(int i = 0; i < maxSlots; i++){
			GameObject _newSlot;
			if(currentSlotStyle == styleSlotInformation.InGameMenuStyle){
				_newSlot = Instantiate(Resources.Load<GameObject>(pathSlotPrefab + "GameSlotIngameMenu"));
			}
			else{
				_newSlot = Instantiate(Resources.Load<GameObject>(pathSlotPrefab + "GameSlotMainMenu"));
			}

			_newSlot.name = "GameSlot_" + i;
			_newSlot.tag = "GameSlots";
			_newSlot.transform.SetParent(gameSlotContentPanel.transform, false);

			string _filePattern = "save_" + i + ".xml";
			string[] _gameFilesList = FileManager.FindFiles(directoryGameFiles, _filePattern);

			if(_gameFilesList.Length == 0){
				_newSlot.GetComponent<GameSlotButton>().Initialize(i);
				GameSlotButton _gameSlotButtonScript = _newSlot.GetComponent<GameSlotButton>();
				gameSlots.Add(_gameSlotButtonScript);
			}
			else if (_gameFilesList.Length == 1){
				string _patternFileExtension = @"\b.xml\b";
				string _replace = "";
				string _gameFile = _gameFilesList[0];

				_gameFile = Regex.Replace(_gameFile, _patternFileExtension, _replace, RegexOptions.IgnoreCase);

				string _filename = _gameFile.Substring(_gameFile.Length - 6, 4);
				int _number =  int.Parse(_gameFile.Substring(_gameFile.Length - 1));

				string _lastPlayedLevel = GameFileManager.Instance.GetLastPlayedLevel(directoryGameFiles,_filename, _number, "xml", "plaintext");
				DateTime _lastTimePlayed = GameFileManager.Instance.GetLastTimePlayed(directoryGameFiles,_filename, _number, "xml", "plaintext");
				_newSlot.GetComponent<GameSlotButton>().Initialize(i, _lastPlayedLevel, _lastTimePlayed, currentSlotStyle);

				try{
					GameSlotButton _gameSlotButtonScript = _newSlot.GetComponent<GameSlotButton>();
					gameSlots.Add(_gameSlotButtonScript);
				}catch(NullReferenceException){
					Debug.LogError("GameButtonSlot is null");
				}

				totalUsedSlots++;
			}
		}


	}

	public void OnGameSlotClicked(int number){
		currentGameSlotSelected = number;
	}

	public void Save(){
		if(currentGameSlotSelected != -1){
			StartCoroutine(SaveGameSlot(currentGameSlotSelected));
		}
		unSelectAllGameSlots();
	}

	public void Load(){
		if(currentGameSlotSelected != -1){
			StartCoroutine(LoadGameSlot(currentGameSlotSelected));
		}
		unSelectAllGameSlots();
	}

	public void Delete(){
		if(currentGameSlotSelected != -1){
			StartCoroutine(DeleteGameSlot(currentGameSlotSelected));
		}
		unSelectAllGameSlots();
	}

	private IEnumerator SaveGameSlot(int numberSlot){
		bool _isGameSlotEmpty = gameSlotContentPanel.transform.FindChild("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().isSlotEmpty;
		
		if(!_isGameSlotEmpty){
			ModalWindowHandler.Instance.Initialize("OVERWRITE_GAME");
		}
		else{
			ModalWindowHandler.Instance.Initialize("SAVE_GAME");
		}

		do{
			yield return null;
		}while(!ModalWindowHandler.Instance.isAnswerSelected);
		
		if(ModalWindowHandler.Instance.isYesClicked){
			GameFileManager.Instance.SaveGameFile(numberSlot);
			string _lastPlayedLevel = GameFileManager.Instance.GetLastPlayedLevel(directoryGameFiles, "save", numberSlot, "xml", "plaintext");
			DateTime _lastTimePlayed = GameFileManager.Instance.GetLastTimePlayed(directoryGameFiles, "save", numberSlot, "xml", "plaintext");
			gameSlotContentPanel.transform.Find("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().Initialize(numberSlot, _lastPlayedLevel, _lastTimePlayed, currentSlotStyle);
		}

		SettingsFileManager.Instance.SaveSettingsFile();
		ModalWindowHandler.Instance.Disable();
		currentGameSlotSelected = -1;
	}

	private IEnumerator LoadGameSlot(int numberSlot){
		bool _isGameSlotEmpty = gameSlotContentPanel.transform.Find("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().isSlotEmpty;

		if(!_isGameSlotEmpty){
			ModalWindowHandler.Instance.Initialize("LOAD_GAME");
			
			do{
				yield return null;
			}while(!ModalWindowHandler.Instance.isAnswerSelected);
			
			if(ModalWindowHandler.Instance.isYesClicked){
				ResetGameSlotsPanel();
				ReturnToButton.isAnyMenuActive = false;
				GameFileManager.Instance.LoadGameFile(numberSlot);
				GameController.WarpToLevel(GameState.lastPlayedLevel);
			}

			SettingsFileManager.Instance.SaveSettingsFile();
			ModalWindowHandler.Instance.Disable();
			currentGameSlotSelected = -1;
		}
	}

	private IEnumerator DeleteGameSlot(int numberSlot){
		bool _isGameSlotEmpty = gameSlotContentPanel.transform.Find("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().isSlotEmpty;
		
		if(!_isGameSlotEmpty){
			ModalWindowHandler.Instance.Initialize("DELETE_GAME");
			
			do{
				yield return null;
			}while(!ModalWindowHandler.Instance.isAnswerSelected);
			
			if(ModalWindowHandler.Instance.isYesClicked){
				GameFileManager.Instance.DeleteGameFile(numberSlot);
				gameSlotContentPanel.transform.Find("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().ClearSlot();
			}

			ModalWindowHandler.Instance.Disable();
			currentGameSlotSelected = -1;
		}
	}

	public GameSlotButton GetGameSlot(int number){
		try{
			return gameSlots[number];
		}catch(IndexOutOfRangeException){
			return null;
		}
	}

	public void ResetGameSlotsPanel(){
		currentGameSlotSelected = -1;
		unSelectAllGameSlots();
	}
}
