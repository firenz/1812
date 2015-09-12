using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Transform))]
public class LanguagesPanelManager : MonoBehaviour {
	public int countLanguages { get; private set;}

	[SerializeField]
	private string pathLocalizationDirectories = "Data/Localization";
	[SerializeField]
	private string pathPrefabs = "Prefabs/UIElements/";

	private string xmlLanguageMetaInfoIDPath = "/locatedtexts/meta/Language";
	private string[] directoriesPath;
	private List<string> languagesID;
	private Transform contentPanel;
	private UIGenericScrollbar scrollbar;

	private void OnEnable(){
		LanguagePanelButton.showLanguageMenuOnClick += ShowLanguagePanel;
		ChangeCurrentLanguageButton.disableLanguagePanel += ResetPosition;
		ReturnToButton.resetUIPositions += ResetPosition;
		ReturnToSettingsMenuButton.returnToSettings += ResetPosition;
	}

	private void OnDisable(){
		LanguagePanelButton.showLanguageMenuOnClick -= ShowLanguagePanel;
		ChangeCurrentLanguageButton.disableLanguagePanel -= ResetPosition;
		ReturnToButton.resetUIPositions -= ResetPosition;
		ReturnToSettingsMenuButton.returnToSettings -= ResetPosition;
	}
	
	private void Start() {
		countLanguages = 0;
		languagesID = new List<string>();
		contentPanel = this.transform.FindChild("Panel/ScrollView/ContentPanel");
		scrollbar = this.transform.FindChild("Panel/Scrollbar").GetComponent<UIGenericScrollbar>();
		directoriesPath = FileManager.FindSubDirectories(pathLocalizationDirectories);

		//When not using Resources.Load use this line instead
		//directoriesPath = FileManager.FindSubDirectories("Resources/Data/Localization");

		FindLocalizedLanguages();
	}

	private void FindLocalizedLanguages(){
		foreach(string directory in directoriesPath){
			string _tmpLanguageID = directory.Replace("\\", "/");
			_tmpLanguageID = _tmpLanguageID.Replace(FileManager.path + "/Data/Localization/" , "");
			string _localizedTextPath = "Data/Localization/" +  _tmpLanguageID + "/LocatedTexts.xml";
			_localizedTextPath = _localizedTextPath.Replace("\\", "/");
			
			if(FileManager.CheckFile(_localizedTextPath)){
				languagesID.Add(_tmpLanguageID);
				
				XmlDocument _localizedTextDoc;
				XMLFileReader.OpenXMLFile(out _localizedTextDoc, Application.dataPath + "/" + _localizedTextPath);
				string _nodeText = XMLFileReader.GetNodeInfo(_localizedTextDoc, xmlLanguageMetaInfoIDPath);
				XMLFileReader.CloseXMLFile(out _localizedTextDoc);
				
				CreateLanguageButton(_nodeText, languagesID[countLanguages]);
				countLanguages++;
			}
		}
        
        if(countLanguages < 3){
            scrollbar.gameObject.SetActive(false);
		}
	}

	private void CreateLanguageButton(string languageName, string languageID){
		GameObject _newEntry = Instantiate(Resources.Load<GameObject>(pathPrefabs + "ChangeLanguageButton"));
		_newEntry.name = "LanguageButton_"+ languageID;
		_newEntry.tag = "LanguageButton";
		_newEntry.transform.SetParent(contentPanel, false);
		_newEntry.GetComponent<ChangeCurrentLanguageButton>().Initialize(languageName, languageID);
	}

	public void ShowLanguagePanel(){
		if(scrollbar.gameObject.activeSelf){
			scrollbar.ResetPosition();
		}
		this.transform.SetAsLastSibling();
	}

	private void ResetPosition(){
		this.transform.SetAsFirstSibling();
		if(scrollbar.gameObject.activeSelf){
			scrollbar.ResetPosition();
		}
	}
}
