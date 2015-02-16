using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

public class GameFileManager : MonoBehaviour {
	private static GameFileManager instance;
	public static GameFileManager Instance{
		get{
			if(instance == null){
				instance = new GameObject("FileGameManager").AddComponent<GameFileManager>();
				DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

	private string path; //Holds the application path

	private void OnDestroy() {
		if (instance == this) {
			instance = null;
		}
	}
	
	private void OnApplicationQuit(){
        instance = null;
    }

	private void Awake() {
		if(this != instance){
			Destroy(this.gameObject);
		}
		else{
			FileManager.InitializeDataPath();
			path = Application.dataPath;
        }

	}

	// Use this for initialization
	void Start () {
		if(FileManager.CheckDirectory("Data/Saves/") == false){
			FileManager.CreateDirectory("Data/Saves/");
			FileManager.CreateXMLFile("Data/Saves","save_0","xml", BuildXMLData(),"plaintext");
		}
	}
	
	public void SaveGameFile(int gameFileSlot){
		if(FileManager.CheckDirectory("Data/Saves/") == false){
			FileManager.CreateDirectory("Data/Saves/");
		}

		if(FileManager.CheckFile("Data/Saves/save_" + gameFileSlot.ToString()) == false){
			FileManager.CreateXMLFile("Data/Saves", "save_" + gameFileSlot.ToString(), "xml", BuildXMLData(), "plaintext");
		}
	}

	public void LoadGameFile(int gameFileSlot){
		if(FileManager.CheckDirectory("Data/Saves/") == true){
			if(FileManager.CheckFile("Data/Saves/save_" + gameFileSlot.ToString()) == true){
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

	private string BuildXMLData(){
		Debug.Log("Creating default XML");
		XmlDocument _xmlDoc = new XmlDocument();

		XmlElement _rootElement = _xmlDoc.CreateElement("GameFile");
		_xmlDoc.AppendChild(_rootElement);

		XmlElement _lastSessionData = _xmlDoc.CreateElement("LastSessionData");
		_lastSessionData.SetAttribute("lastPlayableLevel", GameState.lastPlayableLevel);
		_rootElement.AppendChild(_lastSessionData);

		XmlElement _systemSettingsData = _xmlDoc.CreateElement("SystemSettings");
		XmlElement _screenSettings = _xmlDoc.CreateElement("Screen");
		_screenSettings.SetAttribute("Width", GameState.SystemData.ScreenSettings.width.ToString());
		_screenSettings.SetAttribute("Height", GameState.SystemData.ScreenSettings.height.ToString());
		_screenSettings.SetAttribute("FullScreen", GameState.SystemData.ScreenSettings.fullscreen.ToString());
		_systemSettingsData.AppendChild(_screenSettings);
		_systemSettingsData.SetAttribute("currentLanguage", GameState.SystemData.currentLanguage);
		_rootElement.AppendChild(_systemSettingsData);

		XmlElement _cutScenesData = _xmlDoc.CreateElement("CutScenesData");
		_cutScenesData.SetAttribute("isPlayedIntro", GameState.CutSceneData.isPlayedIntro.ToString());
		//...More to come
		_rootElement.AppendChild(_cutScenesData);

		XmlElement _levelCorridorData = _xmlDoc.CreateElement("LevelCorridorData");
		XmlElement _playerPositionInCorridor = _xmlDoc.CreateElement("playerPosition");
		_playerPositionInCorridor.SetAttribute("X", GameState.LevelCorridorData.playerPosition.x.ToString());
		_playerPositionInCorridor.SetAttribute("Y", GameState.LevelCorridorData.playerPosition.y.ToString());
		_levelCorridorData.AppendChild(_playerPositionInCorridor);
		_levelCorridorData.SetAttribute("timesTrashBinWasExaminated", GameState.LevelCorridorData.timesTrashBinWasExaminated.ToString());
		//...More to come
		_rootElement.AppendChild(_levelCorridorData);

		XmlElement _levelProfessorOfficeData = _xmlDoc.CreateElement("LevelProfessorOfficeData");
		XmlElement _playerPositionInProfessorOffice = _xmlDoc.CreateElement("playerPosition");
		_playerPositionInProfessorOffice.SetAttribute("X", GameState.LevelProfessorOfficeData.playerPosition.x.ToString());
		_playerPositionInProfessorOffice.SetAttribute("Y", GameState.LevelProfessorOfficeData.playerPosition.y.ToString());
		_levelProfessorOfficeData.AppendChild(_playerPositionInProfessorOffice);
		_levelProfessorOfficeData.SetAttribute("isWindowOpened", GameState.LevelProfessorOfficeData.isWindowOpened.ToString());
		_levelProfessorOfficeData.SetAttribute("isLittleFlagsPickedFromFloor", GameState.LevelProfessorOfficeData.isLittleFlagsPickedFromFloor.ToString());
		//...More to come
		_rootElement.AppendChild(_levelProfessorOfficeData);

		XmlElement _inventory = _xmlDoc.CreateElement("Inventory");
		List<string> itemsInInventory = Inventory.Instance.GetItemsName();
		foreach(string itemName in itemsInInventory){
			XmlElement _itemInventory = _xmlDoc.CreateElement("item");
			_itemInventory.SetAttribute("Name", itemName);
			_inventory.AppendChild(_itemInventory);
		}
		_rootElement.AppendChild(_inventory);

		return _xmlDoc.OuterXml;
	}

	private void ParseXMLFile(string directory, string filename, string filetype, string mode){
		Debug.Log("Reading XML File in " + directory);
		XmlDocument _xmlDoc = new XmlDocument();

		if(mode == "plaintext"){
			_xmlDoc.Load(path + "/" + directory + "/" + filename + "." + filetype);
		}
		else if(mode == "encrypt"){
			string _filedata = FileManager.ReadFile(directory, filename, filetype);
			_filedata = FileManager.DecryptData(_filedata);
			FileManager.CreateFile(directory + "/", "/tml_" + filename, filetype, _filedata);
			_xmlDoc.Load(path + "/" + directory + "/tmp_" + filename + "." + filetype);
		}

		XmlNode _lastSessionData = _xmlDoc.SelectSingleNode("//LastSessionData");
		GameState.lastPlayableLevel = _lastSessionData.Attributes["lastPlayableLevel"].Value;

		XmlNode _systemSettings = _xmlDoc.SelectSingleNode("//SystemSettings");
		GameState.SystemData.ScreenSettings.width = int.Parse(_systemSettings.FirstChild.Attributes["Width"].Value);
		GameState.SystemData.ScreenSettings.height = int.Parse(_systemSettings.FirstChild.Attributes["Height"].Value);
		GameState.SystemData.ScreenSettings.fullscreen = bool.Parse(_systemSettings.FirstChild.Attributes["FullScreen"].Value);
		GameState.SystemData.currentLanguage = _systemSettings.Attributes["currentLanguage"].Value;

		XmlNode _cutScenesData = _xmlDoc.SelectSingleNode("//CutScenesData");
		GameState.CutSceneData.isPlayedIntro = bool.Parse(_cutScenesData.Attributes["isPlayedIntro"].Value);
		//...More to come

		XmlNode _levelCorridorData = _xmlDoc.SelectSingleNode("//LevelCorridorData");
		GameState.LevelCorridorData.playerPosition.x = int.Parse(_levelCorridorData.FirstChild.Attributes["X"].Value);
		GameState.LevelCorridorData.playerPosition.y = int.Parse(_levelCorridorData.FirstChild.Attributes["Y"].Value);
		GameState.LevelCorridorData.timesTrashBinWasExaminated = int.Parse(_levelCorridorData.FirstChild.Attributes["timesTrashBinWasExaminated"].Value);
		//...More to come

		XmlNode _levelProfessorOfficeData = _xmlDoc.SelectSingleNode("//LevelProfessorOfficeData");
		GameState.LevelProfessorOfficeData.playerPosition.x = int.Parse(_levelProfessorOfficeData.FirstChild.Attributes["X"].Value);
		GameState.LevelProfessorOfficeData.playerPosition.y = int.Parse(_levelProfessorOfficeData.FirstChild.Attributes["Y"].Value);
		GameState.LevelProfessorOfficeData.isWindowOpened = bool.Parse(_levelProfessorOfficeData.Attributes["isWindowOpened"].Value);
		GameState.LevelProfessorOfficeData.isLittleFlagsPickedFromFloor = bool.Parse(_levelProfessorOfficeData.Attributes["isLittleFlagsPickedFromFloor"].Value);
		//...More to come

		//More nodes here when new levels are implemented

		XmlNodeList _items = _xmlDoc.GetElementsByTagName("item");
		foreach(XmlNode item in _items){
			Inventory.Instance.AddItem(item.Attributes["Name"].Value);
		}
	}
}
