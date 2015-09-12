﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

public static class LocatedTextManager {
	private static string currentLanguage;
	private static XmlDocument locatedTextDoc;
	
	public delegate void CurrentLanguageChanged();
	public static event CurrentLanguageChanged currentLanguageChanged;

	private static void OnApplicationQuit(){
		XMLFileReader.CloseXMLFile(out locatedTextDoc);
	}
	
	public static void Initialize() {
		//Get current language from the saved state of the game preferences
		currentLanguage = GameState.SystemData.currentLanguage;

		//This line is for loading the localized XMLs from an external folder
		//(after creating a build and adding the folder with XMLs manually)
		//(used when project is finished for easing work to the people who wants to make his owns localized texts)
		string _xmlPath = Application.dataPath + "/Data/Localization/" + currentLanguage + "/LocatedTexts.xml";

		//Then xml in path is loaded
		locatedTextDoc = new XmlDocument();
		XMLFileReader.OpenXMLFile(out locatedTextDoc, _xmlPath);

	}
	
	public static List<string> GetLocatedText(string groupId , string elementId, string textId){
		string _xmlPathToNode = "/locatedtexts/group[@id='" + groupId + "']/element[@id='" + elementId + "']/string[@id='" + textId + "']";
		string _rawText = XMLFileReader.GetNodeInfo(locatedTextDoc, _xmlPathToNode);

		_rawText = _rawText.Replace("\t", "");
		_rawText = _rawText.Replace("\r", "");
		_rawText = _rawText.TrimStart(' ', '\n');
		_rawText = _rawText.TrimEnd(' ', '\n');
		List<string> _locatedText = _rawText.Split('\n').Select(p => p.Trim()).ToList();

		return _locatedText;
	}

	public static List<string> GetLocatedTextFormatted(string groupID, string elementID, string textID, int maxCharsPerLine = 0, int maxDisplayingLines = 0){
		List<string> _locatedText = GetLocatedText(groupID, elementID, textID);
		List<string> _resultingText = new List<string>();
		string _resultingString = "";

		if(maxCharsPerLine == 0 && maxDisplayingLines == 0){
			_resultingText = _locatedText;
			return _resultingText;
		}

		for(int i = 0; i < _locatedText.Count; i++){
			string _currentLine = _locatedText[i];
			_currentLine = _currentLine.Trim(' ', '\r','\t', '\n');
			List<string> _wordsInLine = _currentLine.Split(' ').ToList();
			int _currentWordsLength = 0;
			int _nextWordLength = 0;
			int _currentNewLine = 0;
			int _lastIndexInLine = _wordsInLine.Count -1;
			_resultingString = "";
            
			for(int j = 0; j < _wordsInLine.Count; j++){
				string _nextWord = _wordsInLine[j];
				_nextWordLength = _nextWord.Length;

				if(_currentNewLine < maxDisplayingLines){
					if((_currentWordsLength + _nextWordLength) <= maxCharsPerLine){
						if(j != _lastIndexInLine){
							_resultingString += _nextWord + " ";	
							_currentWordsLength += _nextWordLength + 1;
						}
						else{
							_resultingString += _nextWord;
                            _resultingText.Add(_resultingString);
                            _currentNewLine = 0;
                            _currentWordsLength = 0;
                            _resultingString = "";
                        }
					}
					else{
						if(j != _lastIndexInLine){
							_resultingString += "\n" + _nextWord + " ";
							_currentWordsLength = _nextWordLength + 1;
                            _currentNewLine++;
						}
						else{
							_resultingString += "\n" + _nextWord;
							_resultingText.Add(_resultingString);
							_currentWordsLength = 0;
                            _currentNewLine = 0;
							_resultingString = "";
                        }
                    }
                }
				else{
					if((_currentWordsLength + _nextWordLength) <= maxCharsPerLine){
						if(j != _lastIndexInLine){
							_resultingString += _nextWord + " ";	
							_currentWordsLength += _nextWordLength + 1;
						}
						else{
							_resultingString += _nextWord;
							_resultingText.Add(_resultingString);
							_currentNewLine = 0;
							_currentWordsLength = 0;
							_resultingString = "";
						}
					}
					else{
						if(j != _lastIndexInLine){
							_resultingText.Add(_resultingString);
							_resultingString = _nextWord + " ";
							_currentWordsLength = _nextWordLength + 1;
							_currentNewLine = 0;
						}
						else{
							_resultingText.Add(_resultingString);
							_resultingText.Add(_nextWord);
							_resultingString = "";
							_currentWordsLength = 0;
                            _currentNewLine = 0;
						}
                    }
                }
            }

            _wordsInLine.Clear();
        }

        return _resultingText;
    }
    
    
    public static int GetTextNodesCount(string groupId, string elementId){
        string _xmlPathToNode = "/locatedtexts/group[@id='" + groupId + "']/element[@id='" + elementId + "']";
        return XMLFileReader.GetChildNodeCount(locatedTextDoc, _xmlPathToNode);
    }
    
    public static void InitializeLanguageSettings(string language){
		XMLFileReader.CloseXMLFile(out locatedTextDoc);
		currentLanguage = language;
		Initialize();
	}

	public static void ChangeCurrentLanguage(string newLanguage){
		XMLFileReader.CloseXMLFile(out locatedTextDoc);
		currentLanguage = newLanguage;
		GameState.SystemData.currentLanguage = newLanguage;
		SettingsFileManager.Instance.SaveSettingsFile();
		Initialize();
		currentLanguageChanged();
	}

	public static string GetGadipediaKeywordID(string valueKeyword){
		string _xmlPathToNode = "/locatedtexts/group[@id='GADIPEDIA_ARTICLES']/element[@id='KEYWORDS']";
		string _keywordID = "";

		XmlNodeList _keywords = XMLFileReader.GetChildNodes(locatedTextDoc, _xmlPathToNode);
		int _indexNodes = 0;

		while((_indexNodes < _keywords.Count) && (_keywords[_indexNodes].InnerText != valueKeyword)){
			_indexNodes++;
		}

		if(_indexNodes < _keywords.Count){
			_keywordID = _keywords[_indexNodes].Attributes["id"].Value;
		}

		return _keywordID;
	}

	public static string GetCurrentLanguage(){
		return currentLanguage;
	}
}