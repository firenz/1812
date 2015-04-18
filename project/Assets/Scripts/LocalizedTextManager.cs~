using UnityEngine;
using UnityEditor;
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

	// Use this for initialization
	public static void Initialize () {
		//Get current language from the saved state of the game preferences
		currentLanguage = GameState.SystemData.currentLanguage;

		charsToBeRemoved = new char[] {'\t', '\n', ' ', '\r'};

		//This line is for loading the localized XMLs from resources folder (used now for testing purposes)
		string xmlPath = "Data/Localization/" + currentLanguage + "/LocalizedTexts";

		//This line is for loading the localized XMLs from an external folder
		//(after creating a build and adding the folder with XMLs manually)
		//(used when project is finished for easing work to the people who wants to make his owns localized texts)
		//string xmlPath = Application.dataPath + "/Resources/Data/Localization/" + currentLanguage + "/LocalizedTexts.xml";

		//Then xml in path is loaded
		XMLFileReader.OpenXMLFile(xmlPath);
	}
	
	public static List<string> GetLocalizedText(string groupId , string elementId, string textId){
		string _xmlPathToNode = "/localizedtexts/group[@id='" + groupId + "']/element[@id='" + elementId + "']/string[@id='" + textId + "']";
		List<string> _localizedText = new List<string>();
		string _rawText = XMLFileReader.GetNodeInfo(_xmlPathToNode);

		_rawText = _rawText.Trim(charsToBeRemoved);
		string[] _textLines = _rawText.Split('\n');

		for(int index = 0; index < _textLines.Length; index++){
			_localizedText.Add(_textLines[index].Trim(charsToBeRemoved));
		}
		return _localizedText;
	}

	public static void ChangeCurrentLanguage(string newLanguage){
		currentLanguage = newLanguage;
	}

	public static string GetCurrentLanguage(){
		return currentLanguage;
	}
}
