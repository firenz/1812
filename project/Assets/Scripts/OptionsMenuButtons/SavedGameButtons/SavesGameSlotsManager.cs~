using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class SavesGameSlotsManager : MonoBehaviour {
	public GameObject gameSlotPrefab;
	public Scrollbar scrollbar;

	private int currentGameSlotSelected = -1;
	private int filledSlots = 0;
	private int maxSlots = 6;
	private GameObject gameSlotContentPanel;
	private string directoryGameFiles = "Data/Saves/";

	public delegate void UnSelectAllGameSlotButtons();
	public static event UnSelectAllGameSlotButtons unSelectAllGameSlots;

	private void OnEnable(){
		GameSlotButton.onSavedGameSlotClick += OnGameSlotClicked;
	}

	private void OnDisable(){
		GameSlotButton.onSavedGameSlotClick -= OnGameSlotClicked;
	}

	private void Start () {
		gameSlotContentPanel = this.transform.FindChild("ScrollView/ContentPanel").gameObject;

		//Verifies save files and load them in slots in content panel
		if(FileManager.CheckDirectory(directoryGameFiles) == false){
			FileManager.CreateDirectory(directoryGameFiles);
		}

		for(int i = 0; i < maxSlots; i++){
			//GameObject _newSlot = Instantiate(Resources.Load<GameObject>("Prefabs/GUI/SavedGameFileSlot"));
			GameObject _newSlot = Instantiate(gameSlotPrefab);
			_newSlot.name = "GameSlot_" + i;
			_newSlot.tag = "GameSlots";
			_newSlot.transform.SetParent(gameSlotContentPanel.transform, false);

			string _filePattern = "save_" + i + ".xml";
			string[] _gameFilesList = FileManager.FindFiles(directoryGameFiles, _filePattern);

			if(_gameFilesList.Length == 0){
				_newSlot.GetComponent<GameSlotButton>().Initialize(i);
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
				_newSlot.GetComponent<GameSlotButton>().Initialize(i, _lastPlayedLevel, _lastTimePlayed);

				filledSlots++;
			}
		}

	}

	public void OnGameSlotClicked(int numberSlot){
		currentGameSlotSelected = numberSlot;
	}

	public void Save(){
		if(currentGameSlotSelected != -1){
			StartCoroutine(SaveGameSlot(currentGameSlotSelected));
		}
	}

	public void Load(){
		if(currentGameSlotSelected != -1){
			StartCoroutine(LoadGameSlot(currentGameSlotSelected));
		}
	}

	public void Delete(){
		if(currentGameSlotSelected != -1){
			StartCoroutine(DeleteGameSlot(currentGameSlotSelected));
		}
	}

	private IEnumerator SaveGameSlot(int numberSlot){
		bool _isGameSlotEmpty = gameSlotContentPanel.transform.FindChild("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().IsSlotEmpty();
		
		if(!_isGameSlotEmpty){
			ModalWindowHandler.Instance.Initialize("OVERWRITE_GAME");
		}
		else{
			ModalWindowHandler.Instance.Initialize("SAVE_GAME");
		}

		do{
			yield return null;
		}while(!ModalWindowHandler.Instance.IsSelectionEnded());
		
		if(ModalWindowHandler.Instance.IsYesClicked()){
			GameFileManager.Instance.SaveGameFile(numberSlot);
			string _lastPlayedLevel = GameFileManager.Instance.GetLastPlayedLevel(directoryGameFiles, "save", numberSlot, "xml", "plaintext");
			DateTime _lastTimePlayed = GameFileManager.Instance.GetLastTimePlayed(directoryGameFiles, "save", numberSlot, "xml", "plaintext");
			gameSlotContentPanel.transform.Find("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().Initialize(numberSlot, _lastPlayedLevel, _lastTimePlayed);
		}

		ModalWindowHandler.Instance.Disable();
		unSelectAllGameSlots();
		currentGameSlotSelected = -1;
	}

	private IEnumerator LoadGameSlot(int numberSlot){
		bool _isGameSlotEmpty = gameSlotContentPanel.transform.Find("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().IsSlotEmpty();

		if(Application.loadedLevelName != "MainMenu"){
			if(!_isGameSlotEmpty){
				ModalWindowHandler.Instance.Initialize("LOAD_GAME");
				
				do{
					yield return null;
				}while(!ModalWindowHandler.Instance.IsSelectionEnded());
				
				if(ModalWindowHandler.Instance.IsYesClicked()){
					GameFileManager.Instance.LoadGameFile(numberSlot);
					GameController.WarpToLevel(GameState.lastPlayableLevel);
                }
                
                ModalWindowHandler.Instance.Disable();
                unSelectAllGameSlots();
                currentGameSlotSelected = -1;
			}
		}
		else{
			GameFileManager.Instance.LoadGameFile(numberSlot);
			GameController.WarpToLevel(GameState.lastPlayableLevel);
        }

	}

	private IEnumerator DeleteGameSlot(int numberSlot){
		bool _isGameSlotEmpty = gameSlotContentPanel.transform.Find("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().IsSlotEmpty();
		
		if(!_isGameSlotEmpty){
			ModalWindowHandler.Instance.Initialize("DELETE_GAME");
			
			do{
				yield return null;
			}while(!ModalWindowHandler.Instance.IsSelectionEnded());
			
			if(ModalWindowHandler.Instance.IsYesClicked()){
				GameFileManager.Instance.DeleteGameFile(numberSlot);
				gameSlotContentPanel.transform.Find("GameSlot_" + numberSlot).GetComponent<GameSlotButton>().ClearSlot();
			}

			ModalWindowHandler.Instance.Disable();
			unSelectAllGameSlots();
			currentGameSlotSelected = -1;
		}
	}

	public int GetCurrentSelectedGameSlot(){
		return currentGameSlotSelected;
	}

	public void Disable(){
		scrollbar.value = 1f;
		currentGameSlotSelected = -1;
		unSelectAllGameSlots();
	}

	public int SlotsCount(){
		return filledSlots;
	}
}
