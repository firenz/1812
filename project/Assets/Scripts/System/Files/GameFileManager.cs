using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

public class GameFileManager : Singleton<GameFileManager> {
	private string path;

	protected override void InitializeOnAwake (){
		path = Application.dataPath;
	}

	private void Start () {
		if(FileManager.CheckDirectory("Data/Saves/") == false){
			FileManager.CreateDirectory("Data/Saves/");
		}
	}
	
	public void SaveGameFile(int gameFileSlot){
		if(FileManager.CheckDirectory("Data/Saves/") == false){
			FileManager.CreateDirectory("Data/Saves/");
		}

		if(FileManager.CheckFile("Data/Saves/save_" + gameFileSlot.ToString()) == false){
			FileManager.CreateXMLFile("Data/Saves", "save_" + gameFileSlot.ToString(), "xml", BuildXMLData(), "plaintext");
		}
		else{
			FileManager.UpdateFile("Data/Saves", "save_" + gameFileSlot.ToString(), "xml", BuildXMLData(), "replace");
		}
	}

	public void LoadGameFile(int gameFileSlot){
		if(FileManager.CheckDirectory("Data/Saves/") == true){
			if(FileManager.CheckFile("Data/Saves/save_" + gameFileSlot + ".xml") == true){
				ParseXMLFile("Data/Saves", "save_" + gameFileSlot, "xml", "plaintext");
			}
			else{
				Debug.LogError("Unable to load game file as the file save_" + gameFileSlot + " does not exist");
			}
		}
		else{
			Debug.LogError("Unable to load game file as the directory does not exist");
		}
	}

	public void DeleteGameFile(int gameFileSlot){
		if(FileManager.CheckDirectory("Data/Saves/") == true){
			string _filePath = "Data/Saves/save_" + gameFileSlot + ".xml";
			if(FileManager.CheckFile(_filePath) == true){
				FileManager.DeleteFile(_filePath);
			}
			else{
				Debug.LogError("Unable to delete game file as the file save_" + gameFileSlot + " does not exist");
			}
		}
		else{
			Debug.LogError("Unable to delete game file as the directory does not exist");
		}
	}

	public string GetLastPlayedLevel(string directory, string filename, int gameFileSlot, string filetype, string mode){
		string _newFilename = filename + "_" + gameFileSlot;
		XmlDocument _xmlDoc = new XmlDocument();
        _xmlDoc.Load(path + "/" + directory + "/" + _newFilename + "." + filetype);

		if(mode == "plaintext"){
			_xmlDoc.Load(path + "/" + directory + "/" + _newFilename + "." + filetype);
		}
		else if(mode == "encrypt"){
			string _filedata = FileManager.ReadFile(directory, _newFilename, filetype);
			_filedata = FileManager.DecryptData(_filedata);
			FileManager.CreateFile(directory + "/", "/tmp_" + _newFilename, filetype, _filedata);
			_xmlDoc.Load(path + "/" + directory + "/tmp_" + _newFilename + "." + filetype);
        }

		return _xmlDoc.SelectSingleNode("//LastSessionData").Attributes["lastPlayableLevel"].Value;
	}

	public DateTime GetLastTimePlayed(string directory, string filename, int gameFileSlot, string filetype, string mode){
		filename = filename + "_" + gameFileSlot;
		XmlDocument _xmlDoc = new XmlDocument();
		_xmlDoc.Load(path + "/" + directory + "/" + filename + "." + filetype);
		
		if(mode == "plaintext"){
			_xmlDoc.Load(path + "/" + directory + "/" + filename + "." + filetype);
		}
		else if(mode == "encrypt"){
			string _filedata = FileManager.ReadFile(directory, filename, filetype);
			_filedata = FileManager.DecryptData(_filedata);
			FileManager.CreateFile(directory + "/", "/tmp_" + filename, filetype, _filedata);
			_xmlDoc.Load(path + "/" + directory + "/tmp_" + filename + "." + filetype);
		}

		string _stringDateTime = _xmlDoc.SelectSingleNode("//LastSessionData").Attributes["lastTimePlayed"].Value;
		DateTime _datetime = DateTime.ParseExact(_stringDateTime, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

		return _datetime;
    }
    
    private string BuildXMLData(){
		XmlDocument _xmlDoc = new XmlDocument();
		XmlElement _rootElement = _xmlDoc.CreateElement("GameFile");

		_xmlDoc.AppendChild(_rootElement);

		XmlElement _lastSessionData = _xmlDoc.CreateElement("LastSessionData");
		_lastSessionData.SetAttribute("lastPlayableLevel", GameState.lastPlayedLevel);

		string _date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
		_lastSessionData.SetAttribute("lastTimePlayed", _date);
		_rootElement.AppendChild(_lastSessionData);

		XmlElement _cutScenesData = _xmlDoc.CreateElement("CutScenesData");
		_cutScenesData.SetAttribute("isPlayedIntroCorridorPart", GameState.CutSceneData.isPlayedIntroCorridorPart.ToString());
		_cutScenesData.SetAttribute("isPlayedIntroProfessorOfficePart", GameState.CutSceneData.isPlayedIntroProfessorOfficePart.ToString());
		_cutScenesData.SetAttribute("isPlayedCutsceneAfterPickedFlagsFromFloor", GameState.CutSceneData.isPlayedCutsceneAfterPickedFlagsFromFloor.ToString());
		_cutScenesData.SetAttribute("isPlayedCutsceneAfterMapPuzzleIsSolved", GameState.CutSceneData.isPlayedCutsceneAfterMapPuzzleIsSolved.ToString());
		_cutScenesData.SetAttribute("isPlayedCutsceneLibrarianMetAfterPuzzleIsSolved", GameState.CutSceneData.isPlayedCutsceneLibrarianMetAfterPuzzleIsSolved.ToString());
		//...More to come
		_rootElement.AppendChild(_cutScenesData);

		XmlElement _levelCorridorData = _xmlDoc.CreateElement("LevelCorridorData");
		XmlElement _playerPositionInCorridor = _xmlDoc.CreateElement("playerPosition");
		_playerPositionInCorridor.SetAttribute("X", GameState.LevelCorridorData.playerPosition.x.ToString());
		_playerPositionInCorridor.SetAttribute("Y", GameState.LevelCorridorData.playerPosition.y.ToString());
		_levelCorridorData.AppendChild(_playerPositionInCorridor);
		_levelCorridorData.SetAttribute("timesTrashBinWasExaminated", GameState.LevelCorridorData.timesTrashBinWasExaminated.ToString());
		_levelCorridorData.SetAttribute("isDialogueChoiceMadeWithLibrarianFinished", GameState.LevelCorridorData.isDialogueChoiceMadeWithLibrarianFinished.ToString());
		//...More to come
		_rootElement.AppendChild(_levelCorridorData);

		XmlElement _levelProfessorOfficeData = _xmlDoc.CreateElement("LevelProfessorOfficeData");
		XmlElement _playerPositionInProfessorOffice = _xmlDoc.CreateElement("playerPosition");
		_playerPositionInProfessorOffice.SetAttribute("X", GameState.LevelProfessorOfficeData.playerPosition.x.ToString());
		_playerPositionInProfessorOffice.SetAttribute("Y", GameState.LevelProfessorOfficeData.playerPosition.y.ToString());
		_levelProfessorOfficeData.AppendChild(_playerPositionInProfessorOffice);
		_levelProfessorOfficeData.SetAttribute("isWindowOpened", GameState.LevelProfessorOfficeData.isWindowOpened.ToString());
		_levelProfessorOfficeData.SetAttribute("isLittleFlagsFellIntoFloor", GameState.LevelProfessorOfficeData.isLittleFlagsFellIntoFloor.ToString());
		_levelProfessorOfficeData.SetAttribute("isLittleFlagsPickedFromFloor", GameState.LevelProfessorOfficeData.isLittleFlagsPickedFromFloor.ToString());
		//...More to come
		_rootElement.AppendChild(_levelProfessorOfficeData);

		XmlElement _levelMapPuzzleData = _xmlDoc.CreateElement("LevelMapPuzzleData");
		_levelMapPuzzleData.SetAttribute("isMapPuzzleSolved", GameState.MapPuzzleData.isMapPuzzleSolved.ToString());
		_rootElement.AppendChild(_levelMapPuzzleData);

		XmlElement _inventory = _xmlDoc.CreateElement("Inventory");
		List<string> itemsInInventory = Inventory.Instance.GetItemsName();
		foreach(string itemName in itemsInInventory){
			XmlElement _itemInventory = _xmlDoc.CreateElement("item");
			_itemInventory.SetAttribute("Name", itemName);
			_inventory.AppendChild(_itemInventory);
		}
		_rootElement.AppendChild(_inventory);

		return FileManager.XmlDocToString(_xmlDoc);
	}

	private void ParseXMLFile(string directory, string filename, string filetype, string mode){
		XmlDocument _xmlDoc = new XmlDocument();

		if(mode == "plaintext"){
			_xmlDoc.Load(path + "/" + directory + "/" + filename + "." + filetype);
		}
		else if(mode == "encrypt"){
			string _filedata = FileManager.ReadFile(directory, filename, filetype);
			_filedata = FileManager.DecryptData(_filedata);
			FileManager.CreateFile(directory + "/", "/tmp_" + filename, filetype, _filedata);
			_xmlDoc.Load(path + "/" + directory + "/tmp_" + filename + "." + filetype);
		}

		XmlNode _lastSessionData = _xmlDoc.SelectSingleNode("//LastSessionData");
		GameState.lastPlayedLevel = _lastSessionData.Attributes["lastPlayableLevel"].Value;

		XmlNode _cutScenesData = _xmlDoc.SelectSingleNode("//CutScenesData");
		GameState.CutSceneData.isPlayedIntroCorridorPart = bool.Parse(_cutScenesData.Attributes["isPlayedIntroCorridorPart"].Value);
		GameState.CutSceneData.isPlayedIntroProfessorOfficePart = bool.Parse(_cutScenesData.Attributes["isPlayedIntroProfessorOfficePart"].Value);
		GameState.CutSceneData.isPlayedCutsceneAfterPickedFlagsFromFloor = bool.Parse(_cutScenesData.Attributes["isPlayedCutsceneAfterPickedFlagsFromFloor"].Value);
		GameState.CutSceneData.isPlayedCutsceneAfterMapPuzzleIsSolved = bool.Parse(_cutScenesData.Attributes["isPlayedCutsceneAfterMapPuzzleIsSolved"].Value);
		GameState.CutSceneData.isPlayedCutsceneLibrarianMetAfterPuzzleIsSolved = bool.Parse(_cutScenesData.Attributes["isPlayedCutsceneLibrarianMetAfterPuzzleIsSolved"].Value);
		//...More to come

		XmlNode _levelCorridorData = _xmlDoc.SelectSingleNode("//LevelCorridorData");
		GameState.LevelCorridorData.playerPosition.x = float.Parse(_levelCorridorData.FirstChild.Attributes["X"].Value);
		GameState.LevelCorridorData.playerPosition.y = float.Parse(_levelCorridorData.FirstChild.Attributes["Y"].Value);
		GameState.LevelCorridorData.timesTrashBinWasExaminated = int.Parse(_levelCorridorData.Attributes["timesTrashBinWasExaminated"].Value);
		GameState.LevelCorridorData.isDialogueChoiceMadeWithLibrarianFinished = bool.Parse(_levelCorridorData.Attributes["isDialogueChoiceMadeWithLibrarianFinished"].Value);
		//...More to come

		XmlNode _levelProfessorOfficeData = _xmlDoc.SelectSingleNode("//LevelProfessorOfficeData");
		GameState.LevelProfessorOfficeData.playerPosition.x = float.Parse(_levelProfessorOfficeData.FirstChild.Attributes["X"].Value);
		GameState.LevelProfessorOfficeData.playerPosition.y = float.Parse(_levelProfessorOfficeData.FirstChild.Attributes["Y"].Value);
		GameState.LevelProfessorOfficeData.isWindowOpened = bool.Parse(_levelProfessorOfficeData.Attributes["isWindowOpened"].Value);
		GameState.LevelProfessorOfficeData.isLittleFlagsFellIntoFloor = bool.Parse(_levelProfessorOfficeData.Attributes["isLittleFlagsFellIntoFloor"].Value);
		GameState.LevelProfessorOfficeData.isLittleFlagsPickedFromFloor = bool.Parse(_levelProfessorOfficeData.Attributes["isLittleFlagsPickedFromFloor"].Value);
		//...More to come

		XmlNode _levelMapPuzzleData = _xmlDoc.SelectSingleNode("//LevelMapPuzzleData");
		GameState.MapPuzzleData.isMapPuzzleSolved = bool.Parse(_levelMapPuzzleData.Attributes["isMapPuzzleSolved"].Value);

		//More nodes here when new levels are implemented

		XmlNodeList _items = _xmlDoc.GetElementsByTagName("item");
		Inventory.Instance.DeleteAllItems();
		foreach(XmlNode item in _items){
			Inventory.Instance.AddItem(item.Attributes["Name"].Value);
		}
	}
	
}
