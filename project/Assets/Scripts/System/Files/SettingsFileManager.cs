using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Xml;

public class SettingsFileManager : Singleton<SettingsFileManager> {
	private string path;
	
	protected override void InitializeOnAwake (){}

	public void Initialize () {
		path = Application.dataPath;

		if(FileManager.CheckDirectory("Data/") == false){
			FileManager.CreateDirectory("Data/");
		}
		
		if(FileManager.CheckFile("Data/settings.xml") == false){
			GameState.InitializeSettingsState();
			SaveSettingsFile();
		}
		else{
			LoadSettingsFile();
		}

		Screen.SetResolution(GameState.SystemData.ScreenSettings.width, GameState.SystemData.ScreenSettings.height, GameState.SystemData.ScreenSettings.fullscreen);
		LocatedTextManager.InitializeLanguageSettings(GameState.SystemData.currentLanguage);
	}
	
	public void SaveSettingsFile(){
		if(FileManager.CheckDirectory("Data/") == false){
			FileManager.CreateDirectory("Data/");
		}
		
		if(FileManager.CheckFile("Data/settings.xml") == false){
			FileManager.CreateXMLFile("Data/", "settings", "xml", BuildXMLData(), "plaintext");
		}
		else{
			FileManager.UpdateFile("Data/", "settings", "xml", BuildXMLData(), "replace");
		}
	}

	public void LoadSettingsFile(){
		if(FileManager.CheckDirectory("Data/") == false){
			FileManager.CreateDirectory("Data/");
		}

		if(FileManager.CheckFile("Data/settings.xml") == true){
			ParseXMLFile("Data/", "settings", "xml", "plaintext");
		}
		else{
			FileManager.CreateXMLFile("Data/", "settings", "xml", BuildXMLData(), "plaintext");
		}

	}

	private string BuildXMLData(){
		XmlDocument _xmlDoc = new XmlDocument();

		XmlElement _rootElement = _xmlDoc.CreateElement("SettingsFile");
		_xmlDoc.AppendChild(_rootElement);
		
		XmlElement _systemSettingsData = _xmlDoc.CreateElement("SystemSettings");

		XmlElement _firstSetup = _xmlDoc.CreateElement("FirstSetup");
		_firstSetup.SetAttribute("firstTimeLaunched", GameState.SystemData.isFirstTimeGameLaunched.ToString());
		_systemSettingsData.AppendChild(_firstSetup);

		XmlElement _screenSettings = _xmlDoc.CreateElement("Screen");
		_screenSettings.SetAttribute("Width", GameState.SystemData.ScreenSettings.width.ToString());
		_screenSettings.SetAttribute("Height", GameState.SystemData.ScreenSettings.height.ToString());
		_screenSettings.SetAttribute("FullScreen", GameState.SystemData.ScreenSettings.fullscreen.ToString());
		_systemSettingsData.AppendChild(_screenSettings);

		XmlElement _languageSettings = _xmlDoc.CreateElement("Language");
		_languageSettings.SetAttribute("currentLanguage", GameState.SystemData.currentLanguage);
		_systemSettingsData.AppendChild(_languageSettings);

		XmlElement _soundSettings = _xmlDoc.CreateElement("SoundSettings");
		_soundSettings.SetAttribute("musicVolume", GameState.SystemData.AudioVolumeSettings.music.ToString());
		_soundSettings.SetAttribute("SFXVolume", GameState.SystemData.AudioVolumeSettings.sfx.ToString());
		_systemSettingsData.AppendChild(_soundSettings);

		_rootElement.AppendChild(_systemSettingsData);

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
		
		XmlNode _systemSettings = _xmlDoc.SelectSingleNode("//SystemSettings");

		GameState.SystemData.isFirstTimeGameLaunched = bool.Parse(_systemSettings.SelectSingleNode("//FirstSetup").Attributes["firstTimeLaunched"].Value);
		GameState.SystemData.ScreenSettings.width = int.Parse(_systemSettings.SelectSingleNode("//Screen").Attributes["Width"].Value);
		GameState.SystemData.ScreenSettings.height = int.Parse(_systemSettings.SelectSingleNode("//Screen").Attributes["Height"].Value);
		GameState.SystemData.ScreenSettings.fullscreen = bool.Parse(_systemSettings.SelectSingleNode("//Screen").Attributes["FullScreen"].Value);
		GameState.SystemData.currentLanguage = _systemSettings.SelectSingleNode("//Language").Attributes["currentLanguage"].Value;
		GameState.SystemData.AudioVolumeSettings.music = int.Parse(_systemSettings.SelectSingleNode("//SoundSettings").Attributes["musicVolume"].Value);
		GameState.SystemData.AudioVolumeSettings.sfx = int.Parse(_systemSettings.SelectSingleNode("//SoundSettings").Attributes["SFXVolume"].Value);
	}
}
