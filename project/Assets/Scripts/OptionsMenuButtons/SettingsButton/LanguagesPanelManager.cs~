using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class LanguagesPanelManager : MonoBehaviour {
	private string[] directoriesPath;
	private List<string> languagesID;
	private int countLanguages = 0;
	private GameObject contentPanel;

	private void OnEnable(){
		LanguageButton.disableLanguagePanel += DisableLanguagesPanel;
	}

	private void OnDisable(){
		LanguageButton.disableLanguagePanel -= DisableLanguagesPanel;
	}

	// Use this for initialization
	private void Start() {
		languagesID = new List<string>();
		contentPanel = this.transform.FindChild("ScrollView/ContentPanel").gameObject;
		//directoriesPath = FileManager.FindSubDirectories("Resources/Data/Localization");
		directoriesPath = FileManager.FindSubDirectories("Data/Localization");

		foreach(string directory in directoriesPath){
			string _tempLanguageID = directory.Replace("\\", "/");
			//_tempLanguageID = _tempLanguageID.Replace(FileManager.path + "/Resources/Data/Localization/" , "");
			_tempLanguageID = _tempLanguageID.Replace(FileManager.path + "/Data/Localization/" , "");
			//string _localizedTextPath = "Resources/Data/Localization/" + _tempLanguageID + "/LocalizedTexts.xml";
			string _localizedTextPath = "Data/Localization/" + _tempLanguageID + "/LocalizedTexts.xml";
			_localizedTextPath = _localizedTextPath.Replace("\\", "/");

			if(FileManager.CheckFile(_localizedTextPath)){
				languagesID.Add(_tempLanguageID);


				//Until XMLFileReader is improved for different XmlDocument files not just one...
				//////////////////////////////////////
				XmlDocument _localizedTextDoc;
				_localizedTextPath = _localizedTextPath.Replace(".xml", "");
				_localizedTextPath = _localizedTextPath.Replace("Resources/", "");

				TextAsset _newTextAsset = Resources.Load<TextAsset>(_localizedTextPath);
				_localizedTextDoc = new XmlDocument();
				_localizedTextDoc.LoadXml(_newTextAsset.text);

				string _nodeText = "text";
				
				try{
					_nodeText = _localizedTextDoc.SelectSingleNode("/localizedtexts/meta/Language").InnerText;
				} catch (NullReferenceException exception){
					_nodeText = "[This string has either not been implemented or needs to be translated.]";
                    Debug.LogError("Text string not found.\nException: " + exception.ToString());
				}

				_localizedTextDoc = null;
				GC.Collect();
				GC.WaitForPendingFinalizers();
				//////////////////////////////////////


				CreateLanguageButton(_nodeText, languagesID[countLanguages]);
				countLanguages++;
			}

		}

		if(countLanguages > 4){
			contentPanel.transform.parent.parent.FindChild("Scrollbar").gameObject.SetActive(true);
		}

		this.transform.SetAsFirstSibling();
	}

	public void CreateLanguageButton(string languageName, string languageID){
		GameObject _newEntry = Instantiate(Resources.Load<GameObject>("Prefabs/GUI/LanguageButton"));
		_newEntry.name = "LanguageButton";
		_newEntry.transform.SetParent(contentPanel.transform, false);
		_newEntry.GetComponent<LanguageButton>().Initialize(languageName, languageID);
	}

	public void DisableLanguagesPanel(){
		this.transform.SetAsFirstSibling();
		this.transform.FindChild("Scrollbar").GetComponent<Scrollbar>().value = 1f;
	}

	public int TotalLanguages(){
		return countLanguages;
	}
}
