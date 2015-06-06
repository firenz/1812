using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text.RegularExpressions;

public static class LocalizedTextManager {
	private static string currentLanguage;
	private static char[] charsToBeRemoved;
	private static XmlDocument localizedTextDoc;


	public delegate void CurrentLanguageChanged();
	public static event CurrentLanguageChanged currentLanguageChanged;

	private static void OnApplicationQuit(){
		string _xmlPath = "Data/Localization/" + currentLanguage + "/LocalizedTexts";
		XMLFileReader.CloseXMLFile(out localizedTextDoc,_xmlPath);
	}

	// Use this for initialization
	public static void Initialize () {
		//Get current language from the saved state of the game preferences
		currentLanguage = GameState.SystemData.currentLanguage;

		charsToBeRemoved = new char[] {'\t', '\n', ' ', '\r'};

		//This line is for loading the localized XMLs from resources folder (used now for testing purposes)
		//string _xmlPath = "Data/Localization/" + currentLanguage + "/LocalizedTexts";

		//This line is for loading the localized XMLs from an external folder
		//(after creating a build and adding the folder with XMLs manually)
		//(used when project is finished for easing work to the people who wants to make his owns localized texts)
		string _xmlPath = Application.dataPath + "/Data/Localization/" + currentLanguage + "/LocalizedTexts.xml";

		//Then xml in path is loaded
		localizedTextDoc = new XmlDocument();
		XMLFileReader.OpenXMLFile(out localizedTextDoc, _xmlPath);

	}
	
	public static List<string> GetLocalizedText(string groupId , string elementId, string textId){
		string _xmlPathToNode = "/localizedtexts/group[@id='" + groupId + "']/element[@id='" + elementId + "']/string[@id='" + textId + "']";
		List<string> _localizedText = new List<string>();
		string _rawText = XMLFileReader.GetNodeInfo(localizedTextDoc, _xmlPathToNode);

		_rawText = _rawText.Trim(charsToBeRemoved);
		string[] _textLines = _rawText.Split('\n');

		for(int index = 0; index < _textLines.Length; index++){
			_localizedText.Add(_textLines[index].Trim(charsToBeRemoved));
		}
		return _localizedText;
	}

	public static int GetTextNodesCount(string groupId, string elementId){
		string _xmlPathToNode = "/localizedtexts/group[@id='" + groupId + "']/element[@id='" + elementId + "']";

		return XMLFileReader.GetChildNodeCount(localizedTextDoc, _xmlPathToNode);
	}

	public static void ChangeCurrentLanguage(string newLanguage){
		Debug.Log("Language changed!");
		string _xmlPath = "Data/Localization/" + currentLanguage + "/LocalizedTexts";
		XMLFileReader.CloseXMLFile(out localizedTextDoc, _xmlPath);
		currentLanguage = newLanguage;
		GameState.SystemData.currentLanguage = newLanguage;
		SettingsFileManager.Instance.SaveSettingsFile();
		Initialize();
		currentLanguageChanged();
	}

	public static string GetCurrentLanguage(){
		return currentLanguage;
	}
}
